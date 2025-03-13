
public class King : Piece {
    public bool IsMoved {get; set;}
    public bool IsChecked {get; set;}
    public bool CanShortCastle {get; set;}
    public bool CanLongCastle {get; set;}
    public King(PieceColor color, Position position) : base(color, position) {
        IsMoved = false;
        IsChecked = false;
        CanShortCastle = false;
        CanLongCastle = false;
    }

    public override List<Position> GetValidMoves(Board board)
    {
        throw new NotImplementedException();
    }
}