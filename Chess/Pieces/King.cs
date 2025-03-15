
public class King : Piece {
    public bool IsChecked {get; set;}
    public King(PieceColor color, Position position) : base(color, position) {
        IsChecked = false;
    }

    public override List<Position> GetValidMoves(Board board) {
        List<Position> validMoves = new();

        List<List<int>> directions = new();
        for (int row = -1; row <= 1; row++) {
            for (int col = -1; col <= 1; col++) {
                if (row != 0 || col != 0) directions.Add([row,col]);
            }
        }

        foreach (var dir in directions) {
            Position oneStep = new Position(CurrentPosition.Row + dir[0], CurrentPosition.Col + dir[1]);

            if (Board.IsInsideBoard(oneStep) && 
                !board.IsFriendlyPieceAt(oneStep, Color) && 
                !board.IsUnderAttack(oneStep, Color))
            {
                validMoves.Add(oneStep);
            }
        }

        // Check for castling
        if (!IsMoved && !IsChecked) {
            TryAddCastlingMove(board, ref validMoves, true);  // Short Castle
            TryAddCastlingMove(board, ref validMoves, false); // Long Castle
        }

        return validMoves;
    }

    private void TryAddCastlingMove(Board board, ref List<Position> castleMoves, bool isShortCastle) {
        int kingColDest = isShortCastle ? 6 : 2;  // Destination column for the king
        int rookColStart = isShortCastle ? 7 : 0;  // Column where the rook starts
        Position rookPos = new Position(CurrentPosition.Row, rookColStart);
        Position kingDestination = new Position(CurrentPosition.Row, kingColDest);

        if (CanCastle(board, rookPos)) {
            castleMoves.Add(kingDestination);
        }
    }

    private bool CanCastle(Board board, Position rookPos) {
        Piece? piece = board.GetPieceAt(rookPos);

        if (piece is Rook rook && rook.IsMoved is false) {
            int step = rookPos.Col > CurrentPosition.Col ? 1 : -1;

            for (int col = CurrentPosition.Col + step; col != rookPos.Col; col += step) {
                Position betweenPos = new Position(CurrentPosition.Row, col);

                if (board.GetPieceAt(betweenPos) is not null || board.IsUnderAttack(betweenPos, Color)) {
                    return false;
                }
            }
            return true;
        }
        return false;
    }
}