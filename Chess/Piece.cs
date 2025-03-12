public abstract class Piece {
    public PieceColor Color {get; private set;}
    public bool IsKilled {get; set;}
    public Position CurrentPosition {get; set;}
    public List<Position> ValidMoves {get; private set;}

    public Piece(PieceColor color, int row, int col) {
        Color = color;
        IsKilled = false;
        CurrentPosition = new Position(row,col);
        ValidMoves = new List<Position>();
    }

    public virtual bool IsValidMove(Position newPosition) {
        if (ValidMoves is not null && ValidMoves.Contains(newPosition)) return true;
        return false;
    }

    
    public List<Position> GetValidMoves(Board board) {
        List<Position> newPositions = new();
        // check the board state (is a move blocked or kills an opponent)
        // the piece's movement rules?
        ValidMoves = newPositions;
        return newPositions;
    }

    public abstract bool Move(Position newPosition, out Position? lastMovementOrigin);
    
    public static bool Kill(Piece targetPiece) {
        targetPiece.CurrentPosition = new Position(9,9);
        targetPiece.IsKilled = true;
        return true;
    }
}