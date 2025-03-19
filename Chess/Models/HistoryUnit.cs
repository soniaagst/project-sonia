namespace Chess.Models;
public class HistoryUnit
{
    public Piece? Piece;
    public Position Destination;
    public bool IsKill = false;
    public bool IsCheck = false;
    public bool IsShortCastle = false;
    public bool IsLongCastle = false;
    public bool IsPromotion = false;
    public Piece? PromotedPiece;
}