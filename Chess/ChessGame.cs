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
        _players.Add(new Player(whiteName, PieceColor.White));
        _players.Add(new Player(blackName, PieceColor.Black));
        _currentTurnIndex = 0;
        _currentPlayer = _players[_currentTurnIndex];
        _display = display;
        switchPlayer = () => { _currentTurnIndex = 1 - _currentTurnIndex; _currentPlayer = _players[_currentTurnIndex];};
    }

    public void Play() {
        Position? lastMovementOrigin = null;
        while (_gameStatus == GameStatus.Running) {
            _display.DisplayBoard(_board, lastMovementOrigin);
            _display.DisplayMessage("Enter 'exit' to quit the game.");
            if (_currentPlayer.Status == PlayerStatus.Checked) _display.DisplayMessage("CHECK!");
            string input = _display.AskNonNullInput($"{_currentPlayer.Name}'s ({_currentPlayer.Color}) turn, enter your move (ex.: b1 c3) ");

            if (input == "EXIT") {
                _display.DisplayMessage("Game exited.");
                _currentPlayer.Status = PlayerStatus.Resigned; switchPlayer(); _currentPlayer.Status = PlayerStatus.Won;
                _gameStatus= GameStatus.Finished;
                break;
            }

            Movement? movement;
            if (!_display.TryParseMove(input, out movement)) {
                _display.DisplayMessage("Invalid input or move. Try again.");
                continue;
            }
            if (Move(movement!) == true) {
                UpdateGameStatus();
                switchPlayer();
            }
            else {
                _display.DisplayMessage("Invalid move. Try again.");
                continue;
            }
        }

        if (_gameStatus == GameStatus.Finished) {
            _display.DisplayBoard(_board, lastMovementOrigin);
            _display.DisplayMessage("Game Over.");
            Player? winner = _players.Find(player => player.Status == PlayerStatus.Won);
            Player? resignedPlayer = _players.Find(player => player.Status == PlayerStatus.Resigned);
            if (resignedPlayer is not null) {
                _display.DisplayMessage($"{resignedPlayer.Name} has resigned.");
            }
            if (winner is not null) {
                _display.DisplayMessage($"{winner.Name} won!");
            }
            else _display.DisplayMessage("It's a draw.");
        }
        
    }

    private bool IsValidMove(Movement move) { // add: if someone is checked, the only valid move is save the king
        Piece? piece = _board.GetPieceAt(move.From);
        if (piece is not null && piece.Color == _currentPlayer.Color) {
            List<Position> validMoves = piece.GetValidMoves(_board);
            if (validMoves.Contains(move.To)) return true;
        }
        _display.DisplayMessage("Invalid move!");
        return false;
    }

    public bool Move(Movement movement) {
        if (!IsValidMove(movement)) return false;

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

    private Piece CreatePromotedPiece(PromoteOption choice, PieceColor color, Position position) {
        if (choice is PromoteOption.Rook) return new Rook(color, position);
        if (choice is PromoteOption.Bishop) return new Bishop(color, position);
        if (choice is PromoteOption.Knight) return new Knight(color, position);
        else return new Queen(color, position);
    }

    private void UpdateGameStatus() {
        Player opponent = _players[1 - _currentTurnIndex];
        // if in check and don't have valid move, it's checkmate
        if (IsInCheck(opponent)) {
            opponent.Status = PlayerStatus.Checked;
            if (opponent.HasValidMove(_board) == false) {
                opponent.Status = PlayerStatus.Checkmate;
                _currentPlayer.Status = PlayerStatus.Won;
                _gameStatus = GameStatus.Finished;
                return;
            }
        }
        // if not in check but don't have valid move, it's stalemate
        if (opponent.HasValidMove(_board) == false) {
            opponent.Status = PlayerStatus.Stalemate;
            _currentPlayer.Status = PlayerStatus.Stalemate;
            _gameStatus = GameStatus.Finished;
            return;
        }
    }

    public bool IsInCheck(Player player) {
        King king = _board.FindKing(player.Color)!;
        bool result = _board.IsUnderAttack(king.CurrentPosition, king.Color);
        if (result) king.IsChecked = true;
        return result;
    }

}