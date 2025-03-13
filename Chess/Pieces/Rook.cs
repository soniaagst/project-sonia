
public class Rook : Piece {
    public bool IsMoved {get; set;}
    public Rook(PieceColor color, Position position) : base(color, position) {
        IsMoved = false;
    }

    public override List<Position> GetValidMoves(Board board)
    {
        throw new NotImplementedException();
    }
}