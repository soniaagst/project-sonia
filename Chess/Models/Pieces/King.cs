using Chess.Enums;

namespace Chess.Models.Pieces;
public class King : Piece
{
    public bool IsChecked { get; set; }
    public King(PieceColor color, Position position) : base(color, position)
    {
        IsChecked = false;
    }

    public override List<Position> GetValidMoves(Board board)
    {
        List<Position> validMoves = [];

        List<List<int>> directions = [];
        for (int row = -1; row <= 1; row++)
        {
            for (int col = -1; col <= 1; col++)
            {
                if (row != 0 || col != 0) directions.Add([row, col]);
            }
        }

        foreach (var dir in directions)
        {
            Position oneStep = new(CurrentPosition.Row + dir[0], CurrentPosition.Col + dir[1]);

            if (Board.IsInsideBoard(oneStep) &&
                !board.IsFriendlyPieceAt(oneStep, Color) &&
                !board.IsUnderAttack(oneStep, Color))
            {
                validMoves.Add(oneStep);
            }
        }

        if (!IsMoved && !IsChecked)
        {
            TryAddCastlingMove(board, ref validMoves, isShortCastle: true);
            TryAddCastlingMove(board, ref validMoves, isShortCastle: false);
        }

        return validMoves;
    }

    private void TryAddCastlingMove(Board board, ref List<Position> validMoves, bool isShortCastle)
    {
        int kingDestCol = isShortCastle ? 6 : 2;
        int rookStartCol = isShortCastle ? 7 : 0;
        int step = isShortCastle ? 1 : -1;
        int row = CurrentPosition.Row;

        Position rookPos = new(row, rookStartCol);
        Position kingDestination = new(row, kingDestCol);

        Piece? rook = board.GetPieceAt(rookPos);
        if (rook is Rook && rook.IsMoved is false)
        {

            for (int col = CurrentPosition.Col + step; col != rookPos.Col; col += step)
            {
                Position betweenPos = new(row, col);

                if (board.GetPieceAt(betweenPos) is not null || board.IsUnderAttack(betweenPos, Color))
                {
                    return;
                }
            }
            validMoves.Add(kingDestination);
        }
    }
}