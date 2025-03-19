namespace Chess.Models;
public struct Movement(Position from, Position to)
{
    public Position From { get; } = from;
    public Position To { get; } = to;
}