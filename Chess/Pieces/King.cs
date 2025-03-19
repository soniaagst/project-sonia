public class King : Piece {
    public bool IsChecked {get; set;}
    public King(Colors color, Box position) : base(color, position) {
        IsChecked = false;
    }

    public override List<Box> GetValidMoves(Board board) {
        List<Box> validMoves = new();

        List<List<int>> directions = new();
        for (int row = -1; row <= 1; row++) {
            for (int col = -1; col <= 1; col++) {
                if (row != 0 || col != 0) directions.Add([row,col]);
            }
        }

        foreach (var dir in directions) {
            Box oneStep = new Box(CurrentPosition.Y + dir[0], CurrentPosition.X + dir[1]);

            if (Board.IsInsideBoard(oneStep) && 
                !board.IsFriendlyPieceAt(oneStep, Color) && 
                !board.IsUnderAttack(oneStep, Color))
            {
                validMoves.Add(oneStep);
            }
        }

        if (!IsMoved && !IsChecked) {
            TryAddCastlingMove(board, ref validMoves, isShortCastle: true);
            TryAddCastlingMove(board, ref validMoves, isShortCastle: false);
        }

        return validMoves;
    }

    private void TryAddCastlingMove(Board board, ref List<Box> validMoves, bool isShortCastle) {
        int kingDestCol = isShortCastle? 6 : 2;
        int rookStartCol = isShortCastle? 7 : 0;
        int step = isShortCastle? 1 : -1;
        int row = CurrentPosition.Y;

        Box rookPos = new Box(row, rookStartCol);
        Box kingDestination = new Box(row, kingDestCol);

        Piece? rook = board.GetPieceAt(rookPos);
        if (rook is Rook && rook.IsMoved is false) {

            for (int col = CurrentPosition.X + step; col != rookPos.X; col += step) {
                Box betweenPos = new Box(row, col);

                if (board.GetPieceAt(betweenPos) is not null || board.IsUnderAttack(betweenPos, Color)) {
                    return;
                }
            }
            validMoves.Add(kingDestination);
        }
    }
}