public class Player {
    public string Name {get;}
    public PieceColor Color {get;}
    public PlayerStatus Status {get; set;}
    public bool HasValidMoves {get; set;}

    public Player(string name, PieceColor color) {
        Name = name;
        Color = color;
        Status = PlayerStatus.Normal;
        HasValidMoves = true;
    }

    public override string ToString()
    {
        return Color.ToString();
    }
}