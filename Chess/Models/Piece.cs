using Chess.Enums;

namespace Chess.Models;
public abstract class Piece
{
    public Colors Color { get; private set; }
    public bool IsKilled { get; set; }
    public bool IsMoved { get; set; }
    public Position CurrentPosition { get; set; }

    public Piece(Colors color, Position position)
    {
        Color = color;
        IsKilled = false;
        CurrentPosition = position;
    }

    public abstract List<Position> GetValidMoves(Board board);

    public override string ToString()
    {
        string pieceChar = GetType().ToString() switch
        {
            "Pawn" => "P",
            "Knight" => "N",
            "Bishop" => "B",
            "Rook" => "R",
            "Queen" => "Q",
            "King" => "K",
            _ => "?"
        };
        return pieceChar;
    }
}