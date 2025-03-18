public class Pawn : Piece {
    public bool CanEnPassant {get; set;}
    public bool JustForwardTwo {get; set;}
    public Pawn(Colors color, Box position) : base(color, position) {
        CanEnPassant = false;
        JustForwardTwo = false;
    }

    public override List<Box> GetValidMoves(Board board) {
        List<Box> validmoves = new();
        int direction = (Color == Colors.White) ? -1 : 1; // If pawn color is white, move up. If black, move down.

        Box forwardOne = new Box(CurrentPosition.Y + direction, CurrentPosition.X);
        if (Board.IsInsideBoard(forwardOne) && board.GetPieceAt(forwardOne) == null) {
            validmoves.Add(forwardOne);

            Box forwardTwo = new Box(CurrentPosition.Y + (2 * direction), CurrentPosition.X);
            if (!IsMoved && Board.IsInsideBoard(forwardTwo) && board.GetPieceAt(forwardTwo) == null) {
                validmoves.Add(forwardTwo);
            }
        }

        // Killing move
        Box[] diagonals = {
            new Box(CurrentPosition.Y + direction, CurrentPosition.X - 1),
            new Box(CurrentPosition.Y + direction, CurrentPosition.X + 1)
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
                    Box behind = new Box(diag.Y - direction, diag.X);
                    if (board.GetPieceAt(behind) is Pawn enemyPawn && enemyPawn.Color != Color && enemyPawn.JustForwardTwo) {
                        validmoves.Add(diag);
                    }
                }
            }
        }

        return validmoves;
    }
}