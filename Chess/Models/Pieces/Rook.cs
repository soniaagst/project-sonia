using Chess.Enums;

namespace Chess.Models.Pieces;
public class Rook : Piece
{
    public Rook(PieceColor color, Position position) : base(color, position) { }

    public override List<Position> GetValidMoves(Board board)
    {
        List<Position> validMoves = [];
        int[][] directions = [[-1, 0], [1, 0], [0, -1], [0, 1]];

        foreach (var dir in directions)
        {
            Position destination = CurrentPosition;

            while (true)
            {
                destination = new Position(destination.Row + dir[0], destination.Col + dir[1]);
                if (!Board.IsInsideBoard(destination)) break;

                Piece? pieceAtDestination = board.GetPieceAt(destination);
                if (pieceAtDestination == null)
                {
                    validMoves.Add(destination);
                }
                else
                {
                    if (pieceAtDestination.Color != Color) validMoves.Add(destination);
                    break;
                }
            }
        }

        return validMoves;
    }
}