public class Player {
    public string Name {get;}
    public PieceColor Color {get;}
    public PlayerStatus Status {get; set;}

    public Player(string name, PieceColor color) {
        Name = name;
        Color = color;
        Status = PlayerStatus.Normal;
    }

    public bool HasValidMove(Board board) {
        List<Movement> validMoves = [];
        foreach (var piece in board.GetBoard()) {
            if (piece?.Color == Color) {
                foreach (var destination in piece.GetValidMoves(board)) {
                    validMoves.Add(new Movement(piece.CurrentPosition, destination));
                }
            }
        }
        if (validMoves != null) return true;
        else return false;
    }
}