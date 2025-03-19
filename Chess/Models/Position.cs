namespace Chess.Models;
public struct Position(int row, int col)
{
    public int Row = row;
    public int Col = col;

    public override readonly string ToString()
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