public abstract class Piece {
    public PieceColor Color {get; private set;}
    public bool IsKilled {get; set;}
    public Position CurrentPosition {get; protected set;}
    public List<Position> ValidMoves {get; set;}

    public Piece(PieceColor color) {
        Color = color;
        ValidMoves = new List<Position>();
    }

    public bool IsValidMove(Position newPosition) {
        if (ValidMoves is not null && ValidMoves.Contains(newPosition)) return true;
        return false;
    }

    public abstract bool Move(Position newPosition, out Position? lastMovementOrigin);
}

public class Pawn : Piece {
    public bool IsMoved {get; private set;}
    public bool CanPromote {get; set;}
    public Pawn(PieceColor color) : base(color) {
        IsMoved = false;
        CanPromote = false;
    }

    public override bool Move(Position newPosition, out Position? lastMovementOrigin) {
        int distance = Math.Abs(newPosition.Row - CurrentPosition.Row);
        if (distance > 1) { // En Passant checking
            if (distance > 2 || IsMoved) {
                lastMovementOrigin = null;
                return false;
            }
            lastMovementOrigin = CurrentPosition;
            CurrentPosition = newPosition;
            IsMoved = true;
            return true;
        }
        if (newPosition.Row == 0 || newPosition.Row == 7) CanPromote = true;
        lastMovementOrigin = CurrentPosition;
        CurrentPosition = newPosition;
        IsMoved = true;
        return true;
    }

    public void Promote() {}
}

public class Knight : Piece {
    public Knight(PieceColor color) : base(color) {}

    public override bool Move(Position newPosition, out Position? lastMovementOrigin)
    {
        throw new NotImplementedException();
    }
}

public class Bishop : Piece {
    public Bishop(PieceColor color) : base(color) {}

    public override bool Move(Position newPosition, out Position? lastMovementOrigin)
    {
        throw new NotImplementedException();
    }
}

public class Rook : Piece {
    public bool IsMoved {get; set;}
    public Rook(PieceColor color) : base(color) {
        IsMoved = false;
    }

    public override bool Move(Position newPosition, out Position? lastMovementOrigin)
    {
        throw new NotImplementedException();
    }
}

public class Queen : Piece {
    public Queen(PieceColor color) : base(color) {}

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
    public King(PieceColor color) : base(color) {
        IsMoved = false;
    }

    public override bool Move(Position newPosition, out Position? lastMovementOrigin)
    {
        throw new NotImplementedException();
    }
}