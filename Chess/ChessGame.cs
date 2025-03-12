public class GameController {
    private GameStatus _gameStatus;
    private Board _board;
    private List<Player> _players = new();
    private int _currentTurn;
    private Display _display;

    public GameController(Display display) {
        _gameStatus = GameStatus.Running;
        _board = new();
        _players.Add(new Player("WhitePlayer", PieceColor.White));
        _players.Add(new Player("BlackPlayer", PieceColor.Black));
        _currentTurn = 0;
        _display = display;
    }

    public void Play() {
        // while the game is running:
            // check if the current player has valid moves
            // show the board
            // show message "Enter 'exit' to quit the game. White turn, enter your move (ex.: b1 c3) "
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

    public void Move(Movement movement) {
        // check validity for specific piece type
        // check for special movements for specific piece type (castling, en passant)
        // if it kills, Kill
        // move (swap the piece location, clear the old location)
    }

    public void Kill() {}

    public bool IsValidMove(Position currentPosition, Position newPosition) {
        Piece? piece = _board.GetPieceAt(currentPosition);
        if (piece is not null && piece.IsValidMove(newPosition)) {
            return true;
        }
        return false;
    }
}