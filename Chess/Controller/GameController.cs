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
    public event Action? OnTurnChanged;
    private List<HistoryUnit> _moveHistory = [];

    public GameController(IDisplay display, IPlayer whitePlayer, IPlayer blackPlayer)
    {
        _display = display;
        _board = new();
        _players.Add(whitePlayer);
        _players.Add(blackPlayer);
        _currentTurnIndex = 0;
        _currentTurn = _players[_currentTurnIndex];
        _gameStatus = GameStatus.Running;

        OnTurnChanged += () => {

        };
    }

    public void Play()
    {
        Position? lastMoveOrigin = null;

        while (_gameStatus == GameStatus.Running)
        {
            _display.DisplayBoard(_board, lastMoveOrigin);
            _display.DisplayHistory(_moveHistory);
            _display.DisplayMessage("\nEnter 'exit' to quit the game. Enter 'draw' to end the game in a tie.");

            if (_currentTurn.Status == PlayerStatus.Checked)
            {
                _display.DisplayMessage("You're in CHECK! Save your King.");
            }

            string input = _display.AskNonNullInput($"{_currentTurn.PlayerName}'s ({_currentTurn.Color}) turn, enter your move (ex.: b1 c3) ");

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
                _display.DisplayMessage("Player agreed to draw.");
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
            _display.DisplayBoard(_board, lastMoveOrigin);
            _display.DisplayHistory(_moveHistory);

            IPlayer opponent = _players[1 - _currentTurnIndex];

            if (_currentTurn.Status == PlayerStatus.Checkmate)
            {
                _display.DisplayMessage($"\nCHECKMATE! {opponent.PlayerName} wins!");
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
    {
        if (!IsLegalMove(move)) return false;

        Action<Piece?, Piece?, Pawn?> onMoveMade = (movingPiece, killedPiece, promotedPawn) =>
        {
            HandleMoveDone(movingPiece!, killedPiece, promotedPawn, move);
        };

        return _board.MovePiece(move.From, move.To, onMoveMade);
    }

    private void HandleMoveDone(Piece movingPiece, Piece? killedPiece, Pawn? promotedPawn, Movement move)
    {
        if (killedPiece is not null)
        {
            _board.KillPiece(killedPiece);
        }

        Piece? promotedPiece = null;
        if (promotedPawn is not null)
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
            Destination = move.To,
            IsKill = killedPiece != null,
            IsCheck = IsInCheck(_players[1 - _currentTurnIndex]),
            IsShortCastle = movingPiece is King && move.To.Col - move.From.Col == 2,
            IsLongCastle = movingPiece is King && move.To.Col - move.From.Col == -2,
            IsPromotion = promotedPawn != null,
            PromotedPiece = promotedPiece
        });
        TrimMoveHistory();
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
        OnTurnChanged?.Invoke();
    }

    private void UpdateGameStatus()
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


    }

    private bool IsInCheck(IPlayer player)
    {
        King king = _board.FindKing(player.Color)!;
        bool result = _board.IsUnderAttack(king.CurrentPosition, king.Color);
        if (result) king.IsChecked = true;
        return result;
    }
}