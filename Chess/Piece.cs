public abstract class Piece {
    public PieceColor Color {get; private set;}
    public bool IsKilled {get; set;}
    public Position CurrentPosition {get; set;}
    public List<Position> ValidMoves {get; set;}

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

    public abstract bool Move(Position newPosition, out Position? lastMovementOrigin);
    
    public bool Kill(Piece pieceToKill) {
        pieceToKill.CurrentPosition = new Position(9,9);
        pieceToKill.IsKilled = true;
        return true;
    }
}

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

    public void Promote() {}
}

public class Knight : Piece {
    public Knight(PieceColor color, int row, int col) : base(color, row, col) {}

    public override bool Move(Position newPosition, out Position? lastMovementOrigin)
    {
        throw new NotImplementedException();
    }
}

public class Bishop : Piece {
    public Bishop(PieceColor color, int row, int col) : base(color, row, col) {}

    public override bool Move(Position newPosition, out Position? lastMovementOrigin)
    {
        throw new NotImplementedException();
    }
}

public class Rook : Piece {
    public bool IsMoved {get; set;}
    public Rook(PieceColor color, int row, int col) : base(color, row, col) {
        IsMoved = false;
    }

    public override bool Move(Position newPosition, out Position? lastMovementOrigin)
    {
        throw new NotImplementedException();
    }
}

public class Queen : Piece {
    public Queen(PieceColor color, int row, int col) : base(color, row, col) {}

    public override bool Move(Position newPosition, out Position? lastMovementOrigin)
    {
        throw new NotImplementedException();
    }
}

public class King : Piece {
    public bool IsMoved {get; set;}
    public bool IsChecked {get; set;}
    public bool CanShortCastle {get; set;}
    public bool CanLongCastle {get; set;}
    public King(PieceColor color, int row, int col) : base(color, row, col) {
        IsMoved = false;
        IsChecked = false;
        CanShortCastle = false;
        CanLongCastle = false;
    }

    public override bool Move(Position newPosition, out Position? lastMovementOrigin)
    {
        // if the king move more than 1 step, it's a castle, then move the rook automatically
        throw new NotImplementedException();
    }
}