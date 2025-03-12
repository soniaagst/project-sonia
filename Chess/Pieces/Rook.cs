public class Rook : Piece {
    public bool IsMoved {get; set;}
    public Rook(PieceColor color, int row, int col) : base(color, row, col) {
        IsMoved = false;
    }

    public override bool Move(Position newPosition, out Position? lastMovementOrigin)
    {
        throw new NotImplementedException();
    }
}