public class Pawn : Piece {
    public bool CanEnPassant {get; set;}
    public Pawn(PieceColor color, Position position) : base(color, position) {
        CanEnPassant = false;
    }

    public override List<Position> GetValidMoves(Board board) {
        List<Position> validmoves = new();
        int direction = (Color == PieceColor.White) ? -1 : 1; // If pawn color is white, move up. If black, move down.

        Position forwardOne = new Position(CurrentPosition.Row + direction, CurrentPosition.Col);
        if (Board.IsInsideBoard(forwardOne) && board.GetPieceAt(forwardOne) == null) {
            validmoves.Add(forwardOne);

            Position forwardTwo = new Position(CurrentPosition.Row + (2 * direction), CurrentPosition.Col);
            if (!IsMoved && Board.IsInsideBoard(forwardTwo) && board.GetPieceAt(forwardTwo) == null) {
                validmoves.Add(forwardTwo);
            }
        }

        // Killing move
        Position[] diagonals = {
            new Position(CurrentPosition.Row + direction, CurrentPosition.Col - 1),
            new Position(CurrentPosition.Row + direction, CurrentPosition.Col + 1)
        };
        foreach (var diag in diagonals) {
            if (Board.IsInsideBoard(diag)) {
                Piece? targetPiece = board.GetPieceAt(diag);
                if (targetPiece != null && targetPiece.Color != Color) {
                    validmoves.Add(diag);
                }
            }
        }
        // En passant
        Position[] adjacentPositions = {
            new Position(CurrentPosition.Row, CurrentPosition.Col + 1),
            new Position(CurrentPosition.Row, CurrentPosition.Col - 1)
        };
        if (CanEnPassant) {
            foreach (var adjacentPos in adjacentPositions) {
                Piece? targetPiece = board.GetPieceAt(adjacentPos);
                if (targetPiece != null && targetPiece.Color != Color) {
                    foreach (var diag in diagonals) {
                        if (diag.Col == targetPiece.CurrentPosition.Col) {
                            validmoves.Add(diag);
                        }
                    }
                }
            }
        }

        return validmoves;
    }
}