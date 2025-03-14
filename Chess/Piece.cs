public abstract class Piece {
    public PieceColor Color {get; private set;}
    public bool IsKilled {get; set;}
    public bool IsMoved {get; set;}
    public Position CurrentPosition {get; set;}

    public Piece(PieceColor color, Position position) {
        Color = color;
        IsKilled = false;
        CurrentPosition = position;
    }

    public abstract List<Position> GetValidMoves(Board board);
}