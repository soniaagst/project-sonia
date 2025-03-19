namespace Chess.Models;
public struct Movement(Position from, Position to)
// in case if the name Move originally means is the noun (not the verb),
// it's better to call it Movement for clarity
{
    public Position From { get; } = from;
    public Position To { get; } = to;
}