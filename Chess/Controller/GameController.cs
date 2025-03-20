using Chess.Models;
using Chess.Models.Pieces;
using Chess.Enums;
using Chess.Interfaces;

namespace Chess.Controller;
public class GameController
{
    private IDisplay _display;
    private GameStatus _gameStatus;
    private Board _board;
    private List<IPlayer> _players = [];
    private int _currentTurnIndex;
    private IPlayer _currentTurn;
    public event Action<IPlayer>? OnTurnSwitched;
    public event Action<Position?>? OnGameUpdated; // is a rename of OnMovesPlayedHistory, it shows history
    private List<HistoryUnit> _moveHistory = []; // is rename of MovesPlayer. it lists moves details.

    public GameController(IDisplay display, IPlayer whitePlayer, IPlayer blackPlayer, Action<Board>? initializeBoard = null)
    {
        _display = display;

        _board = new();
        initializeBoard?.Invoke(_board); // custom initial board
        if (initializeBoard is null) _board.InitializeBoard();

        _players.Add(whitePlayer);
        _players.Add(blackPlayer);

        _currentTurnIndex = 0;
        _currentTurn = _players[_currentTurnIndex];
        
        _gameStatus = GameStatus.Running;

        SubscribeEvents();
    }

    private void SubscribeEvents() {
        OnGameUpdated += lastMove => {
            _display.DisplayBoard(_board, lastMove);
            _display.DisplayHistory(_moveHistory);
        };

        OnTurnSwitched += player => {
            _display.DisplayMessage("\nEnter 'exit' to quit the game. Enter 'draw' to end the game in a tie.");
            _display.DisplayMessage($"{player.PlayerName}'s ({player.Color}) turn.");
        };
    }
    
    public void Play()
    {
        Position? lastMoveOrigin = null;

        OnGameUpdated?.Invoke(lastMoveOrigin);
        OnTurnSwitched?.Invoke(_currentTurn);

        while (_gameStatus == GameStatus.Running)
        {
            if (_currentTurn.Status == PlayerStatus.Checked)
            {
                _display.DisplayMessage("You're in CHECK! Save your King.");
            }

            string input = _display.AskNonNullInput("Enter your move (ex.: b1 c3) ");

            if (input == "EXIT")
            {
                _display.DisplayMessage("Game exited.");
                _currentTurn.Status = PlayerStatus.Resigned;
                _players[1 - _currentTurnIndex].Status = PlayerStatus.Won;
                _gameStatus = GameStatus.Finished;
                break;
            }

            if (input == "DRAW")
            {
                _display.DisplayMessage("Players agreed to draw.");
                _currentTurn.Status = PlayerStatus.Draw;
                _players[1 - _currentTurnIndex].Status = PlayerStatus.Draw;
                _gameStatus = GameStatus.Finished;
                break;
            }

            if (!_display.TryParseMove(input, out Movement? movement))
            {
                _display.DisplayMessage("Invalid input or move. Try again.");
                continue;
            }
            if (MakeMove(movement!.Value) == true)
            {
                lastMoveOrigin = movement!.Value.From;
                
                UpdateGameStatus();
                OnGameUpdated?.Invoke(lastMoveOrigin);

                if (_gameStatus == GameStatus.Finished)
                    break;

                SwitchTurn();
            }
            else
            {
                _display.DisplayMessage("Invalid move. Try again.");
                continue;
            }
        }

        if (_gameStatus == GameStatus.Finished)
        {
            IPlayer opponent = _players[1 - _currentTurnIndex];

            if (opponent.Status == PlayerStatus.Checkmate)
            {
                _display.DisplayMessage($"\nCHECKMATE! {_currentTurn.PlayerName} wins!");
            }
            else
            {
                if (_currentTurn.Status == PlayerStatus.Resigned)
                {
                    _display.DisplayMessage($"{_currentTurn.PlayerName} has resigned. {opponent.PlayerName} wins!");
                }
                else if (_currentTurn.Status == PlayerStatus.Stalemate)
                {
                    _display.DisplayMessage("\nStalemate! The game is draw.");
                }
                else _display.DisplayMessage("\nGame Over. It's a tie.");
            }
        }

    }

    private List<Movement> GetLegalMoves(IPlayer player)
    // is needed for filtering valid moves into 
    // legal moves (moves that won't make king in danger)
    // this is basic and can't be omitted
    {
        List<Movement> legalMoves = [];
        foreach (var piece in _board.GetBoard())
        {
            if (piece?.Color == player.Color)
            {
                List<Position> validMoves = piece.GetValidMoves(_board);
                foreach (var simulatedNewPos in validMoves)
                {
                    Position originalPos = piece.CurrentPosition;

                    Piece? killedPiece = _board.SimulateMove(originalPos, simulatedNewPos);
                    if (!IsInCheck(player))
                    {
                        Movement newMove = new(originalPos, simulatedNewPos);
                        legalMoves.Add(newMove);
                    }
                    _board.UndoSimulation(originalPos, simulatedNewPos, killedPiece);
                    IsInCheck(player);
                }
            }
        }
        return legalMoves;
    }

    private bool IsLegalMove(Movement move)
    // is a rename for ValidateMove()
    // IsLegalMove() is more appropriate because
    // valid move doesn't necessarily legal
    {
        Piece? piece = _board.GetPieceAt(move.From);
        if (piece is not null && piece.Color == _currentTurn.Color)
        {
            List<Movement> legalMoves = GetLegalMoves(_currentTurn);
            if (legalMoves.Contains(move)) return true;
        }
        return false;
    }

    private bool MakeMove(Movement move)
    // MakeMove() and PlayerMove() doesn't seem to have different purpose, so
    // MakeMove() alone is enough
    {
        if (!IsLegalMove(move)) return false;

        bool result = _board.MovePiece(move.From, move.To, out Piece? movingPiece, out Piece? killedPiece, out Pawn? promotedPawn);

        if (result)
        {
            if (killedPiece is not null)
            {
                _board.KillPiece(killedPiece);
            }

            Piece? promotedPiece = null;
            if (promotedPawn is not null)
            // checking and executing promotion handled here, 
            // CheckPromotion() would only make things complicated
            {
                PromoteOption choice = _display.AskPromotionChoice();

                // create promotion piece
                if (choice is PromoteOption.Rook) promotedPiece =  new Rook(promotedPawn.Color, promotedPawn.CurrentPosition);
                else if (choice is PromoteOption.Bishop) promotedPiece = new Bishop(promotedPawn.Color, promotedPawn.CurrentPosition);
                else if (choice is PromoteOption.Knight) promotedPiece = new Knight(promotedPawn.Color, promotedPawn.CurrentPosition);
                else promotedPiece = new Queen(promotedPawn.Color, promotedPawn.CurrentPosition);

                // replace pawn
                Position position = promotedPawn.CurrentPosition;
                _board.KillPiece(promotedPawn);
                _board.GetBoard()[position.Row, position.Col] = promotedPiece;
            }

            _moveHistory.Add(new HistoryUnit
            {
                Piece = movingPiece,
                StartingPosition = move.From,
                EndingPosition = move.To,
                IsKill = killedPiece != null,
                IsCheck = IsInCheck(_players[1 - _currentTurnIndex]),
                IsShortCastle = movingPiece is King && move.To.Col - move.From.Col == 2,
                IsLongCastle = movingPiece is King && move.To.Col - move.From.Col == -2,
                IsPromotion = promotedPawn != null,
                PromotedPiece = promotedPiece,
                KilledPiece = killedPiece
            });
            TrimMoveHistory();
        }

        return result;
    }

    private void TrimMoveHistory()
    {
        if (_moveHistory.Count > 4)
        {
            _moveHistory.RemoveAt(0); _moveHistory.RemoveAt(0);
        }
    }

    private void SwitchTurn()
    {
        _currentTurnIndex = 1 - _currentTurnIndex;
        _currentTurn = _players[_currentTurnIndex];
        OnTurnSwitched?.Invoke(_currentTurn);
    }

    private void UpdateGameStatus()
    // is a rename for SetPlayerGameStatus().
    {
        IPlayer opponent = _players[1 - _currentTurnIndex];

        if (!IsInCheck(_currentTurn)) _currentTurn.Status = PlayerStatus.Normal;
        if (!IsInCheck(opponent)) opponent.Status = PlayerStatus.Normal;

        if (GetLegalMoves(opponent).Count == 0)
        {
            if (IsInCheck(opponent))
            {
                opponent.Status = PlayerStatus.Checkmate;
                _currentTurn.Status = PlayerStatus.Won;
                _gameStatus = GameStatus.Finished;
                return;
            }
            else
            {
                opponent.Status = PlayerStatus.Stalemate;
                _currentTurn.Status = PlayerStatus.Stalemate;
                _gameStatus = GameStatus.Finished;
                return;
            }
        }
        else if (IsInCheck(opponent))
        {
            opponent.Status = PlayerStatus.Checked;
            return;
        }

        if (IsInsufficientMaterial()) {
            opponent.Status = PlayerStatus.Draw;
            _currentTurn.Status = PlayerStatus.Draw;
            _gameStatus = GameStatus.Finished;
            return;
        }
    }

    private bool IsInCheck(IPlayer player)
    // moved from Player
    {
        King king = _board.FindKing(player.Color)!;
        bool result = _board.IsUnderAttack(king.CurrentPosition, king.Color);
        if (result) king.IsChecked = true;
        return result;
    }

    private bool IsInsufficientMaterial() {
        List<Piece> remainingPieces = _board.GetPieces();

        // king vs king
        if (remainingPieces.All(p => p is King)) return true;

        // kn vs k OR kb vs k
        if (remainingPieces.Count == 3 && remainingPieces.Any(p => p is Knight or Bishop) && remainingPieces.Any(p => p is King))
            return true;

        if (remainingPieces.Count == 4) {
            // kb vs kb (same color)
            if (remainingPieces.Count(p => p is Bishop) == 2) {
                List<Piece> bishops = remainingPieces.Where(p => p is Bishop).ToList();
                if (bishops[0].Color != bishops[1].Color) {
                    if (bishops[0].CurrentPosition.GetBoxColor() == bishops[1].CurrentPosition.GetBoxColor())
                        return true;
                }
            }

            // kn vs kn
            if (remainingPieces.Count(piece => piece is Knight) == 2) {
                List<Piece> knights = remainingPieces.Where(piece => piece is Knight).ToList();
                if (knights[0].Color != knights[1].Color) {
                    return true;
                }
            }

            // kb vs kn
            if (remainingPieces.Any(piece => piece is Knight) && remainingPieces.Any(piece => piece is Bishop)) {
                Piece knight = remainingPieces.Find(p => p is Knight)!;
                Piece bishop = remainingPieces.Find(p => p is Bishop)!;
                if (knight.Color != bishop.Color)
                    return true;
            }
        }

        return false;
    }
}

// Board handles the actual execution of movement, and
// en passant is just another movement, it should be handled by the Board, 
// so CheckEnPassant() would be misplaced if put here

// IsOver() has the same outcome as this method, 
// and actually just returns if the game status is finished,
// which is already in Play() and already self explanatory there.

// PlayerPieceDictionary is pointless, unless we implement "draw by insufficient material" rule.
// even if so, it's still redundant since there's Board.GetPieces(), which returns list of remaining pieces on board,
// because we need to call GatPieces() twice and filter based on color twice just to fill the dictionary, and
// further filter again just to see what kind of pieces left onn board.
// And Board.GetPieces() is useless, unless draw by insufficient material is implemented.
// conclusion: adding draw by insufficient material is not violating the class diagram, it's just hidden or implicit.
// conclusion2: PlayerPieceDictionary is still useless anyway.

// IPiece PieceKilled is just doesn't make any sense here. i omitted.