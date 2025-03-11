public enum PlayerStatus {
    Normal, Checked, Checkmate, Won, Stalemate, Draw, Resigned
}

public class Player {
    public string Name {get;}
    public PieceColor Color {get;}
    public PlayerStatus Status {get; set;}

    public Player(string name, PieceColor color) {
        Name = name;
        Color = color;
        Status = PlayerStatus.Normal;
    }
}