public struct Movement {
    public  Box From {get;}
    public Box To {get;}
    public Movement(Box from, Box to) {
        From = from;
        To = to;
    }
}

public class Move {
    public Piece? Piece;
    public Box Destination;
    public bool IsKill = false;
    public bool IsCheck = false;
    public bool IsShortCastle = false;
    public bool IsLongCastle = false;
    public bool IsPromotion = false;
    public Piece? PromotedPiece;
}