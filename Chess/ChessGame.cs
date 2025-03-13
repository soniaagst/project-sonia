public class GameController {
    private GameStatus _gameStatus;
    private Board _board;
    private List<Player> _players = new();
    private int _currentTurn;
    private Display _display;
    private Action switchPlayer;

    public GameController(Display display) {
        _gameStatus = GameStatus.Running;
        _board = new();
        _players.Add(new Player("WhitePlayer", PieceColor.White));
        _players.Add(new Player("BlackPlayer", PieceColor.Black));
        _currentTurn = 0;
        _display = display;
        switchPlayer = () => { _currentTurn = Math.Abs(_currentTurn - 1); };
    }

    public void Play() {
        Position? lastMovementOrigin = null;
        while (_gameStatus == GameStatus.Running) {
            _display.DisplayBoard(_board, lastMovementOrigin);
            _display.DisplayMessage($"Enter 'exit' to quit the game. \n{_players[_currentTurn].ToString} turn, enter your move: ");
        }
        // while the game is running:
            // check if the current player has valid moves
            // show the board
            // show message "Enter 'exit' to quit the game. White turn, enter your move (ex.: B1 C3) "
            // check if the input is 'exit'
            // try parse the input into a Movement
            // find the piece to move on the board (old position)
            // check if the piece belongs to currentplayer
            // check if there's opponent piece on board (new position)
            // call Move
            // check if a pawn can be promoted --> show promote options --> promote
            // check if the move has checked the opponent --> change opponent status
            // check if the move has release the player from checked status
            // change game status if: checkmate, stalemate, draw
            // switch turn
        // if the game finished:
            // show final board
            // show the winner or player states if there's no winner
    }

    private void Move(Position currentPos, Position newPos, Player player) {
        // check validity for specific piece type
        // check for special movements for specific piece type (castling, en passant)
        // if it kills, Kill
        // move (swap the piece location, clear the old location)
    }

    private bool IsValidMove(Position currentPosition, Position newPosition) {
        Piece? piece = _board.GetPieceAt(currentPosition);
        if (piece is not null) {
            List<Position> validMoves = piece.GetValidMoves(_board);
            if (validMoves.Contains(newPosition)) return true;
        }
        return false;
    }

    public void Move(Position currentPosition, Position newPosition) {
    if (_board.MovePiece(currentPosition, newPosition, out Piece? promotedPawn)) {
        if (promotedPawn is Pawn pawn) {
            HandlePromotion(pawn);
        }
        switchPlayer();
    }
}

private void HandlePromotion(Pawn pawn) {
    PromoteOption choice = _display.AskPromotionChoice();
    Piece promotedPiece = CreatePromotedPiece(choice, pawn.Color, pawn.CurrentPosition);
    _board.ReplacePiece(pawn, promotedPiece);
}

private Piece CreatePromotedPiece(PromoteOption choice, PieceColor color, Position position) {
    return choice switch {
        PromoteOption.Queen => new Queen(color, position),
        PromoteOption.Rook => new Rook(color, position),
        PromoteOption.Bishop => new Bishop(color, position),
        PromoteOption.Knight => new Knight(color, position),
        _ => throw new InvalidOperationException("Invalid promotion choice")
    };
}

}