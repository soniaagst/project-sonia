public class GameController {
    private IDisplay _display;
    private GameStatus _gameStatus;
    private Board _board;
    private List<IPlayer> _players = new();
    private int _currentTurnIndex;
    private IPlayer _currentTurn;
    public event Action<IPlayer>? OnTurnChanged; // for UI
    private int _fiftyMoveCounter = 0; // for 50-move rule
    private Dictionary<string, int> _boardHistory = []; // for threefold repetition
    private List<Move> _moveHistory = []; // for showing history

    public GameController(IDisplay display, IPlayer whitePlayer, IPlayer blackPlayer) {
        _display = display;
        _board = new();
        _players.Add(whitePlayer);
        _players.Add(blackPlayer);
        _currentTurnIndex = 0;
        _currentTurn = _players[_currentTurnIndex];
        _gameStatus = GameStatus.Running;
    }

    private void TrimMoveHistory() {
        if (_moveHistory.Count > 4) {
            _moveHistory.RemoveAt(0); _moveHistory.RemoveAt(0);
        }
    }

    private void SwitchTurn() {
        _currentTurnIndex = 1 - _currentTurnIndex;
        _currentTurn = _players[_currentTurnIndex];
        OnTurnChanged?.Invoke(_currentTurn);
    }

    public void Play() {
        Box? lastMoveOrigin = null;

        OnTurnChanged += currentPlayer => {
            _display.DisplayBoard(_board, lastMoveOrigin);
            _display.DisplayHistory(_moveHistory);
            _display.DisplayMessage("Enter 'exit' to quit the game. Enter 'draw' to end the game in a tie.");
            _display.DisplayMessage($"{currentPlayer.PlayerName}'s ({currentPlayer.Color}) turn.");
        };

        while (_gameStatus == GameStatus.Running) {
            _display.DisplayBoard(_board, lastMoveOrigin);
            _display.DisplayHistory(_moveHistory);
            _display.DisplayMessage("Enter 'exit' to quit the game. Enter 'draw' to end the game in a tie.");

            if (_currentTurn.Status == PlayerStatus.Checked) {
                _display.DisplayMessage("You're in CHECK! Save your King.");
            }

            string input = _display.AskNonNullInput($"{_currentTurn.PlayerName}'s ({_currentTurn.Color}) turn, enter your move (ex.: b1 c3) ");

            if (input == "EXIT") {
                _display.DisplayMessage("Game exited.");
                _currentTurn.Status = PlayerStatus.Resigned;
                _players[1-_currentTurnIndex].Status = PlayerStatus.Won;
                _gameStatus= GameStatus.Finished;
                break;
            }

            if (input == "DRAW") {
                _display.DisplayMessage("Player agreed to draw.");
                _currentTurn.Status = PlayerStatus.Draw;
                _players[1-_currentTurnIndex].Status = PlayerStatus.Draw;
                _gameStatus = GameStatus.Finished;
                break;
            }

            Movement? movement;
            if (!_display.TryParseMove(input, out movement)) {
                _display.DisplayMessage("Invalid input or move. Try again.");
                continue;
            }
            if (PlayerMove(movement!.Value) == true) {
                lastMoveOrigin = movement!.Value.From;
                SaveBoardState();
                SetPlayerGameStatus();
                SwitchTurn();
            }
            else {
                _display.DisplayMessage("Invalid move. Try again.");
                continue;
            }
        }

        if (_gameStatus == GameStatus.Finished) {
            _display.DisplayBoard(_board, lastMoveOrigin);
            _display.DisplayHistory(_moveHistory);

            IPlayer opponent = _players[1-_currentTurnIndex];

            if (_currentTurn.Status == PlayerStatus.Checkmate) {
                _display.DisplayMessage($"CHECKMATE! {opponent.PlayerName} wins!");
            }
            else {
                if (_currentTurn.Status == PlayerStatus.Resigned) {
                    _display.DisplayMessage($"{_currentTurn.PlayerName} has resigned. {opponent.PlayerName} wins!");
                }
                else if (_currentTurn.Status == PlayerStatus.Stalemate) {
                    _display.DisplayMessage("Stalemate! The game is draw.");
                }
                else _display.DisplayMessage("Game Over. It's a tie.");
            }
        }
        
    }

    private List<Movement> GetLegalMoves(IPlayer player) {
        List<Movement> legalMoves = [];
        foreach (var piece in _board.GetBoard()) {
            if (piece?.Color == player.Color) {
                List<Box> validMoves = piece.GetValidMoves(_board);
                foreach (var simulatedNewPos in validMoves) {
                    Box originalPos = piece.CurrentPosition;

                    Piece? killedPiece = _board.SimulateMove(originalPos, simulatedNewPos);
                    if (!IsChecked(player)) {
                        Movement newMove = new(originalPos, simulatedNewPos);
                        legalMoves.Add(newMove);
                    }
                    _board.UndoSimulation(originalPos, simulatedNewPos, killedPiece);
                    IsChecked(player);
                }
            }
        }
        return legalMoves;
    }

    private bool ValidateMove(Movement move) {
        Piece? piece = _board.GetPieceAt(move.From);
        if (piece is not null && piece.Color == _currentTurn.Color) {
            List<Movement> legalMoves = GetLegalMoves(_currentTurn);
            if (legalMoves.Contains(move)) return true;
        }
        return false;
    }

    private bool PlayerMove(Movement move) {
        if (!ValidateMove(move)) return false;

        Action<Piece?, Piece?, Pawn?> onMoveMade = (movingPiece, killedPiece, promotedPawn) => {
            MakeMove(movingPiece!, killedPiece, promotedPawn, move);
        };

        return _board.MovePiece(move.From, move.To, onMoveMade);
    }

    private void MakeMove(Piece movingPiece, Piece? killedPiece, Pawn? promotedPawn, Movement move) {
        if (killedPiece is not null) {
            _board.KillPiece(killedPiece);
        }

        Piece? promotedPiece = null;
        if (promotedPawn is not null) {
            promotedPiece = Promote(promotedPawn);
        }

        _fiftyMoveCounter = (killedPiece is null && movingPiece is not Pawn) ? (_fiftyMoveCounter + 1) : 0;

        _moveHistory.Add(new Move {
            Piece = movingPiece,
            Destination = move.To,
            IsKill = killedPiece != null,
            IsCheck = IsChecked(_players[1 - _currentTurnIndex]),
            IsShortCastle = movingPiece is King && move.To.X - move.From.X == 2,
            IsLongCastle = movingPiece is King && move.To.X - move.From.X == -2,
            IsPromotion = promotedPawn != null,
            PromotedPiece = promotedPiece
        });
        TrimMoveHistory();
    }

    private Piece Promote(Pawn promotedPawn) {
        PromoteOption choice = _display.AskPromotionChoice();
        // create promotion piece
        if (choice is PromoteOption.Rook) return new Rook(promotedPawn.Color, promotedPawn.CurrentPosition);
        if (choice is PromoteOption.Bishop) return new Bishop(promotedPawn.Color, promotedPawn.CurrentPosition);
        if (choice is PromoteOption.Knight) return new Knight(promotedPawn.Color, promotedPawn.CurrentPosition);
        Piece promotedPiece = new Queen(promotedPawn.Color, promotedPawn.CurrentPosition);
        // replace pawn
        Box position = promotedPawn.CurrentPosition;
        _board.KillPiece(promotedPawn);
        _board.GetBoard()[position.Y, position.X] = promotedPiece;
        return promotedPiece;
    }

    private void SaveBoardState() {
        string boardState = _board.GenerateBoardState();
        if (_boardHistory.ContainsKey(boardState)) {
            _boardHistory[boardState]++;
        } else {
            _boardHistory[boardState] = 1;
        }
    }

    private void SetPlayerGameStatus() {
        IPlayer opponent = _players[1 - _currentTurnIndex];

        if (!IsChecked(_currentTurn)) _currentTurn.Status = PlayerStatus.Normal;
        if (!IsChecked(opponent)) opponent.Status = PlayerStatus.Normal;

        if (GetLegalMoves(opponent).Count == 0) {
            if (IsChecked(opponent)) {
                opponent.Status = PlayerStatus.Checkmate;
                _currentTurn.Status = PlayerStatus.Won;
                _gameStatus = GameStatus.Finished;
                return;
            }
            else {
                opponent.Status = PlayerStatus.Stalemate;
                _currentTurn.Status = PlayerStatus.Stalemate;
                _gameStatus = GameStatus.Finished;
                return;
            }
        }
        else if (IsChecked(opponent)) {
            opponent.Status = PlayerStatus.Checked;
            return;
        }

        if (IsInsufficientMaterial() || _fiftyMoveCounter == 100 || IsThreefoldRepetition()) {
            opponent.Status = PlayerStatus.Draw;
            _currentTurn.Status = PlayerStatus.Draw;
            _gameStatus = GameStatus.Finished;
            return;
        }
    }

    private bool IsChecked(IPlayer player) {
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

    private bool IsThreefoldRepetition() {
        return _boardHistory.Values.Any(count => count >= 3);
    }

}