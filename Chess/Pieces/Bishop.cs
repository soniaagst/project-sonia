public class Bishop : Piece {
    public Bishop(PieceColor color, Position position) : base(color, position) {}

    public override List<Position> GetValidMoves(Board board) {
        List<Position> moves = new List<Position>();

        int[] directions = { -1, 1 }; // Diagonal directions
        foreach (int rowDir in directions) {
            foreach (int colDir in directions) {
                Position pos = CurrentPosition;
                while (true) {
                    pos = new Position(pos.Row + rowDir, pos.Col + colDir);
                    if (!Board.IsInsideBoard(pos)) break;
                    
                    Piece? pieceAtDest = board.GetPieceAt(pos);
                    if (pieceAtDest == null) {
                        moves.Add(pos);
                    } else {
                        if (pieceAtDest.Color != this.Color) moves.Add(pos); // Capture
                        break; // Stop if piece blocks path
                    }
                }
            }
        }
        return moves;
    }
}