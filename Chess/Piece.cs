public abstract class Piece {
    public PieceColor Color {get; private set;}
    public bool IsMoved {get; protected set;}
    public Position CurrentPosition {get; protected set;}
    public List<Position> LegalMoves {get; private set;}

    public Piece(PieceColor color) {
        Color = color;
        IsMoved = false;
        LegalMoves = new List<Position>();
    }

    public bool IsValidMove(Position newPosition) {
        return true;
    }
}

public class Pawn : Piece {

}

public class Knight : Piece {

}

public class Bishop : Piece {

}

public class Rook : Piece {

}

public class Queen : Piece {

}

public class King : Piece {
    
}