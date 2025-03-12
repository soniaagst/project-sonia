public class Pawn : Piece {
    public bool IsMoved {get; private set;}
    public bool CanPromote {get; set;}
    public Pawn(PieceColor color, int row, int col) : base(color, row, col) {
        IsMoved = false;
        CanPromote = false;
    }

    public override bool Move(Position newPosition, out Position? lastMovementOrigin) {
        int verticalDistance = Math.Abs(newPosition.Row - CurrentPosition.Row);
        int horizontalDistance = Math.Abs(newPosition.Col - CurrentPosition.Col);
        if (verticalDistance > 1) { // En Passant checking
            if (verticalDistance > 2 || IsMoved) {
                lastMovementOrigin = null;
                return false;
            }
            else {
                lastMovementOrigin = CurrentPosition;
                CurrentPosition = newPosition;
                IsMoved = true;
                return true;
            }
        }
        if (horizontalDistance > 0) {
            lastMovementOrigin = null;
            return false;
        }
        if (newPosition.Row == 0 || newPosition.Row == 7) CanPromote = true;
        lastMovementOrigin = CurrentPosition;
        CurrentPosition = newPosition;
        return true;
    }

    public void Promote(PromoteOption option) {
        
    }
}