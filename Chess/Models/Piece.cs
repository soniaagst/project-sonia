using Chess.Enums;

namespace Chess.Models;
public abstract class Piece
{
    public PieceColor Color { get; private set; }
    public bool IsKilled { get; set; } // is rename for Killed property
    public bool IsMoved { get; set; } // needed for pawn, king, and rook movement rules
    public Position CurrentPosition { get; set; }

    public Piece(PieceColor color, Position position)
    {
        Color = color;
        IsKilled = false;
        CurrentPosition = position;
    }

    public abstract List<Position> GetValidMoves(Board board);
    // is a rename of CanMove(),
    // because the original purpose of CanMove 
    // is to set rules of valid moves for each piece

    public override string ToString()
    // this is for history notation purpose
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

// the usage of abstract class instead of concrete class that inherits an interface here is because
// that is 1 step too much, and interface can't implement method.