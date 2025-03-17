public class Pawn : Piece {
    public bool CanEnPassant {get; set;}
    public bool JustForwardTwo {get; set;}
    public Pawn(Colors color, Position position) : base(color, position) {
        CanEnPassant = false;
        JustForwardTwo = false;
    }

    public override List<Position> GetValidMoves(Board board) {
        List<Position> validmoves = new();
        int direction = (Color == Colors.White) ? -1 : 1; // If pawn color is white, move up. If black, move down.

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
                // Normal killing
                if (targetPiece != null && targetPiece.Color != Color) {
                    validmoves.Add(diag);
                }
                // En Passant
                if (CanEnPassant && targetPiece == null) {
                    Position behind = new Position(diag.Row - direction, diag.Col);
                    if (board.GetPieceAt(behind) is Pawn enemyPawn && enemyPawn.Color != Color && enemyPawn.JustForwardTwo) {
                        validmoves.Add(diag);
                    }
                }
            }
        }

        return validmoves;
    }
}