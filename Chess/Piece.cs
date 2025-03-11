public abstract class Piece {
    public PieceColor Color {get; private set;}
    public bool IsKilled {get; set;}
    public bool IsMoved {get; protected set;}
    public Position CurrentPosition {get; protected set;}
    public List<Position> ValidMoves {get; private set;}

    public Piece(PieceColor color) {
        Color = color;
        IsMoved = false;
        ValidMoves = new List<Position>();
    }

    public bool IsValidMove(Position newPosition) {
        return true;
    }

    // public abstract bool Move(Position currentPosition, Position newPosition) {
    //     Board
    // }
}

public class Pawn : Piece {
    public Pawn(PieceColor color) : base(color) {}
}

public class Knight : Piece {
    public Knight(PieceColor color) : base(color) {}
}

public class Bishop : Piece {
    public Bishop(PieceColor color) : base(color) {}
}

public class Rook : Piece {
    public Rook(PieceColor color) : base(color) {}
}

public class Queen : Piece {
    public Queen(PieceColor color) : base(color) {}
}

public class King : Piece {
    public King(PieceColor color) : base(color) {}
}