
public class Queen : Piece {
    public Queen(PieceColor color, Position position) : base(color, position) {}

    public override List<Position> GetValidMoves(Board board)
    {
        throw new NotImplementedException();
    }
}