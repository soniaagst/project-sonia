
public class King : Piece {
    public bool IsChecked {get; set;}
    public King(PieceColor color, Position position) : base(color, position) {
        IsChecked = false;
    }

    public override List<Position> GetValidMoves(Board board) {
        List<Position> validMoves = new();

        List<Position> directions = new();
        for (int i = -1; i < 2; i++) {
            for (int j = -1; j < 2; j++) {
                if (i != 0 || j != 0) directions.Add(new Position(i,j));
            }
        }

        foreach (var dir in directions) {
            Position newPos = new Position(CurrentPosition.Row + dir.Row, CurrentPosition.Col + dir.Col);

            if (Board.IsInsideBoard(newPos) && 
                !board.IsFriendlyPieceAt(newPos, Color) && 
                !board.IsUnderAttack(newPos, Color)) 
            {
                validMoves.Add(newPos);
            }
        }

        // Check for castling
        if (!IsMoved && !IsChecked) {
            TryAddCastlingMove(board, ref validMoves, true);  // Short Castle
            TryAddCastlingMove(board, ref validMoves, false); // Long Castle
        }

        return validMoves;
    }

    private void TryAddCastlingMove(Board board, ref List<Position> moves, bool isShortCastle) {
        int col = isShortCastle ? 6 : 2;  // Target column for the king
        int rookCol = isShortCastle ? 7 : 0;  // Column where the rook starts
        Position rookPos = new Position(CurrentPosition.Row, rookCol);
        Position kingTarget = new Position(CurrentPosition.Row, col);

        if (CanCastle(board, rookPos, kingTarget)) {
            moves.Add(kingTarget);
        }
    }

    private bool CanCastle(Board board, Position rookPos, Position kingTarget) {
        Piece? piece = board.GetPieceAt(rookPos);
        if (piece is Rook rook && !rook.IsMoved) {
            // Check if the path between King and Rook is clear and not under attack
            int step = rookPos.Col > CurrentPosition.Col ? 1 : -1;
            for (int c = CurrentPosition.Col + step; c != rookPos.Col; c += step) {
                Position checkPos = new Position(CurrentPosition.Row, c);
                if (board.GetPieceAt(checkPos) != null) {
                    return false;
                }
            }
            return true;
        }
        return false;
    }
}