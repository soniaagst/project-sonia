using Chess.Models;
using Chess.Enums;

namespace Chess.Interfaces;
public interface IPlayer
{
    public string PlayerName { get; }
    public Colors Color { get; }
    public PlayerStatus Status { get; set; }
    public bool HasValidMove(Board board);
}