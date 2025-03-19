using Chess.Interfaces;
using Chess.Enums;

namespace Chess.Models;
public class Player : IPlayer
{
    public string PlayerName { get; }
    public PieceColor Color { get; }
    public PlayerStatus Status { get; set; }

    public Player(string name, PieceColor color)
    {
        PlayerName = name;
        Color = color;
        Status = PlayerStatus.Normal;
    }

    public bool HasValidMove(Board board)
    {
        List<Movement> validMoves = [];
        foreach (var piece in board.GetBoard())
        {
            if (piece?.Color == Color)
            {
                foreach (var destination in piece.GetValidMoves(board))
                {
                    validMoves.Add(new Movement(piece.CurrentPosition, destination));
                }
            }
        }
        if (validMoves != null) return true;
        else return false;
    }
}

// moved IsChecked() to the controller for easier reach