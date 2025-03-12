public class King : Piece {
    public bool IsMoved {get; set;}
    public bool IsChecked {get; set;}
    public bool CanShortCastle {get; set;}
    public bool CanLongCastle {get; set;}
    public King(PieceColor color, int row, int col) : base(color, row, col) {
        IsMoved = false;
        IsChecked = false;
        CanShortCastle = false;
        CanLongCastle = false;
    }

    public override bool Move(Position newPosition, out Position? lastMovementOrigin)
    {
        // if the king move more than 1 step, it's a castle, then move the rook automatically
        throw new NotImplementedException();
    }
}