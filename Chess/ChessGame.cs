public enum PieceColor {
    White, Black
}

public enum GameState {
    Running, Finished
}

public struct Position {
    int Row;
    int Col;
    public Position(int row, int col) {
        Row = row; Col = col;
    }
}

public class Player {
    public string Name {get;}
    public PieceColor Color {get;}

    public Player(string name, PieceColor color) {
        Name = name;
        Color = color;
    }
}

public class GameController {
    public GameState GameState {get; private set;}
    public PieceColor CurrentPlayer {get; private set;}

    public GameController() {
        GameState = GameState.Running;
        CurrentPlayer = PieceColor.White;
    }
}