public enum PieceColor {
    White, Black
}

public enum GameStatus {
    Running, Finished
}

public struct Position {
    public int Row;
    public int Col;
    public Position(int row, int col) {
        Row = row; Col = col;
    }
}

public enum PromoteOption {
    Queen, Rook, Bishop, Knight
}

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

    public bool IsValidMove(Position currentPosition, Position newPosition) {
        Piece? piece = _board.GetPieceAt(currentPosition);
        if (piece is not null && piece.IsValidMove(newPosition)) {
            return true;
        }
        return false;
    }
    public void Move() {}
    public void Kill() {}
}