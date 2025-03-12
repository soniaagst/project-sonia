public class Bishop : Piece {
    public Bishop(PieceColor color, int row, int col) : base(color, row, col) {}

    public override bool Move(Position newPosition, out Position? lastMovementOrigin)
    {
        throw new NotImplementedException();
    }
}