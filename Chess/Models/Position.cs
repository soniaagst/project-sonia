using Chess.Enums;

namespace Chess.Models;
public struct Position(int row, int col)
{
    public int Row = row;
    public int Col = col;

    public PieceColor GetBoxColor() {
        return (PieceColor)((Row + Col)%2);
    }

    public override readonly string ToString()
    // for history notation purpose
    {
        char colLetter = Col switch
        {
            0 => 'a',
            1 => 'b',
            2 => 'c',
            3 => 'd',
            4 => 'e',
            5 => 'f',
            6 => 'g',
            7 => 'h',
            _ => '?'
        };
        return $"{colLetter}{8 - Row}";
    }
}

// this is a rename from class Box.
// since Box doesn't have info for occupying piece or is it occupied,
// the name Position is more suitable

// And X and Y is more readable if it's Col and Row.