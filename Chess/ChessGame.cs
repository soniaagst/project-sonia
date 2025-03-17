public class GameController {
    private GameStatus _gameStatus;
    private Board _board;
    private List<Player> _players = new();
    private int _currentTurnIndex;
    private Player _currentPlayer;
    private Display _display;
    private Action switchPlayer;

    public GameController(Display display, string whiteName = "WhitePlayer", string blackName = "BlackPlayer") {
        _gameStatus = GameStatus.Running;
        _board = new();
        _players.Add(new Player(whiteName, Colors.White));
        _players.Add(new Player(blackName, Colors.Black));
        _currentTurnIndex = 0;
        _currentPlayer = _players[_currentTurnIndex];
        _display = display;
        switchPlayer = () => { _currentTurnIndex = 1 - _currentTurnIndex; _currentPlayer = _players[_currentTurnIndex];};
    }

    public void Play() {
        Position? lastMoveOrigin = null;

        while (_gameStatus == GameStatus.Running) {
            _display.DisplayBoard(_board, lastMoveOrigin);
            _display.DisplayMessage("Enter 'exit' to quit the game. Enter 'draw' to end the game in a tie.");

            if (_currentPlayer.Status == PlayerStatus.Checked) {
                _display.DisplayMessage("You're in CHECK! Save your King.");
            }

            string input = _display.AskNonNullInput($"{_currentPlayer.Name}'s ({_currentPlayer.Color}) turn, enter your move (ex.: b1 c3) ");

            if (input == "EXIT") {
                _display.DisplayMessage("Game exited.");
                _currentPlayer.Status = PlayerStatus.Resigned;
                _players[1-_currentTurnIndex].Status = PlayerStatus.Won;
                _gameStatus= GameStatus.Finished;
                break;
            }

            if (input == "DRAW") {
                _display.DisplayMessage("Player agreed to draw.");
                _currentPlayer.Status = PlayerStatus.Draw;
                _players[1-_currentTurnIndex].Status = PlayerStatus.Draw;
                _gameStatus = GameStatus.Finished;
                break;
            }

            Movement? movement;
            if (!_display.TryParseMove(input, out movement)) {
                _display.DisplayMessage("Invalid input or move. Try again.");
                continue;
            }
            if (Move(movement!.Value) == true) {
                lastMoveOrigin = movement!.Value.From;
                UpdateGameStatus();
                switchPlayer();
            }
            else {
                _display.DisplayMessage("Invalid move. Try again.");
                continue;
            }
        }

        if (_gameStatus == GameStatus.Finished) {
            _display.DisplayBoard(_board, lastMoveOrigin);

            Player opponent = _players[1-_currentTurnIndex];

            if (_currentPlayer.Status == PlayerStatus.Checkmate) {
                _display.DisplayMessage($"CHECKMATE! {opponent.Name} wins!");
            }
            else {
                if (_currentPlayer.Status == PlayerStatus.Resigned) {
                    _display.DisplayMessage($"{_currentPlayer.Name} has resigned. {opponent.Name} wins!");
                }
                else if (_currentPlayer.Status == PlayerStatus.Stalemate) {
                    _display.DisplayMessage("Stalemate! The game is draw.");
                }
                else _display.DisplayMessage("Game Over. It's a tie.");
            }
        }
        
    }

    private List<Movement> GetLegalMoves(Player player) {
        List<Movement> legalMoves = [];
        foreach (var piece in _board.GetBoard()) {
            if (piece?.Color == player.Color) {
                List<Position> validMoves = piece.GetValidMoves(_board);
                foreach (var simulatedNewPos in validMoves) {
                    Position originalPos = piece.CurrentPosition;

                    Piece? killedPiece = _board.SimulateMove(originalPos, simulatedNewPos);
                    if (!IsInCheck(player)) {
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

    private bool IsLegalMove(Movement move) {
        Piece? piece = _board.GetPieceAt(move.From);
        if (piece is not null && piece.Color == _currentPlayer.Color) {
            List<Movement> legalMoves = GetLegalMoves(_currentPlayer);
            if (legalMoves.Contains(move)) return true;
        }
        return false;
    }

    private bool Move(Movement movement) {
        if (!IsLegalMove(movement)) return false;

        else if (_board.MovePiece(movement.From, movement.To, out Piece? killedPiece, out Pawn? promotedPawn)) {
            if (killedPiece is not null) {
                Kill(killedPiece);
            }
            if (promotedPawn is not null) {
                HandlePromotion(promotedPawn);
            }
            return true;
        }
        
        else return false;
    }

    private void Kill(Piece targetPiece) {
        _board.KillPiece(targetPiece);
        targetPiece.IsKilled = true;
    }

    private void HandlePromotion(Pawn pawn) {
        PromoteOption choice = _display.AskPromotionChoice();
        Piece promotedPiece = CreatePromotedPiece(choice, pawn.Color, pawn.CurrentPosition);
        _board.ReplacePiece(pawn, promotedPiece);
    }

    private Piece CreatePromotedPiece(PromoteOption choice, Colors color, Position position) {
        if (choice is PromoteOption.Rook) return new Rook(color, position);
        if (choice is PromoteOption.Bishop) return new Bishop(color, position);
        if (choice is PromoteOption.Knight) return new Knight(color, position);
        else return new Queen(color, position);
    }

    private void UpdateGameStatus() {
        Player opponent = _players[1 - _currentTurnIndex];

        if (!IsInCheck(_currentPlayer)) _currentPlayer.Status = PlayerStatus.Normal;
        if (!IsInCheck(opponent)) opponent.Status = PlayerStatus.Normal;

        if (GetLegalMoves(opponent).Count == 0) {
            if (IsInCheck(opponent)) {
                opponent.Status = PlayerStatus.Checkmate;
                _currentPlayer.Status = PlayerStatus.Won;
                _gameStatus = GameStatus.Finished;
                return;
            }
            else {
                opponent.Status = PlayerStatus.Stalemate;
                _currentPlayer.Status = PlayerStatus.Stalemate;
                _gameStatus = GameStatus.Finished;
                return;
            }
        }
        else if (IsInCheck(opponent)) {
            opponent.Status = PlayerStatus.Checked;
            return;
        }

        if (IsInsufficientMaterial()) {
            opponent.Status = PlayerStatus.Draw;
            _currentPlayer.Status = PlayerStatus.Draw;
            _gameStatus = GameStatus.Finished;
            return;
        }
    }

    private bool IsInCheck(Player player) {
        King king = _board.FindKing(player.Color)!;
        bool result = _board.IsUnderAttack(king.CurrentPosition, king.Color);
        if (result) king.IsChecked = true;
        return result;
    }

    private bool IsInsufficientMaterial() {
        List<Piece> remainingPieces = [];
        foreach(var piece in _board.GetBoard()) if (piece != null) remainingPieces.Add(piece);

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
                    if (bishops[0].CurrentPosition.GetSquareColor() == bishops[1].CurrentPosition.GetSquareColor())
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