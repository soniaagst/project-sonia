
public class Pawn : Piece {
    public bool IsMoved {get; private set;}
    public bool CanPromote {get; set;}
    public Pawn(PieceColor color, Position position) : base(color, position) {
        IsMoved = false;
        CanPromote = false;
    }

    public override List<Position> GetValidMoves(Board board) {
        List<Position> moves = new();
        int direction = (Color == PieceColor.White) ? -1 : 1;

        // Normal forward move
        Position forwardOne = new Position(CurrentPosition.Row + direction, CurrentPosition.Col);
        if (Board.IsInsideBoard(forwardOne) && board.GetPieceAt(forwardOne) == null) {
            moves.Add(forwardOne);

            // First move can go two squares
            Position forwardTwo = new Position(CurrentPosition.Row + (2 * direction), CurrentPosition.Col);
            if (!IsMoved && Board.IsInsideBoard(forwardTwo) && board.GetPieceAt(forwardTwo) == null) {
                moves.Add(forwardTwo);
            }
        }

        // Sleding
        Position[] diagonals = {
            new Position(CurrentPosition.Row + direction, CurrentPosition.Col - 1),
            new Position(CurrentPosition.Row + direction, CurrentPosition.Col + 1)
        };

        foreach (var diag in diagonals) {
            if (Board.IsInsideBoard(diag)) {
                Piece? target = board.GetPieceAt(diag);
                if (target != null && target.Color != Color) {
                    moves.Add(diag);
                }
            }
        }

        return moves;
    }
}