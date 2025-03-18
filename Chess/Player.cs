public class Player : IPlayer {
    public string PlayerName {get;}
    public Colors Color {get;}
    public PlayerStatus Status {get; set;}

    public Player(string name, Colors color) {
        PlayerName = name;
        Color = color;
        Status = PlayerStatus.Normal;
    }

    public bool HasValidMove(Board board) {
        List<Move> validMoves = [];
        foreach (var piece in board.GetBoard()) {
            if (piece?.Color == Color) {
                foreach (var destination in piece.GetValidMoves(board)) {
                    validMoves.Add(new Move(piece.CurrentPosition, destination));
                }
            }
        }
        if (validMoves != null) return true;
        else return false;
    }
}

public interface IPlayer {
    public string PlayerName {get;}
    public Colors Color {get;}
    public PlayerStatus Status {get; set;}
    public bool HasValidMove(Board board);
}