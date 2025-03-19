namespace Chess.Models;
public class HistoryUnit
// is a rename from Move, since it's purpose is to build history
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