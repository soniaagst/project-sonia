using Chess.Models;
using Chess.Enums;

namespace Chess.Interfaces;
public interface IPlayer
{
    public string PlayerName { get; }
    public PieceColor Color { get; }
    public PlayerStatus Status { get; set; }
    public bool HasValidMove(Board board);
}