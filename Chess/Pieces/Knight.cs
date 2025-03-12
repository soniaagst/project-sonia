
public class Knight : Piece {
    public Knight(PieceColor color, int row, int col) : base(color, row, col) {}

    public override bool Move(Position newPosition, out Position? lastMovementOrigin)
    {
        throw new NotImplementedException();
    }
}