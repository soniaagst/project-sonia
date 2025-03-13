public class Bishop : Piece {
    public Bishop(PieceColor color, Position position) : base(color, position) {}

    public override List<Position> GetValidMoves(Board board) {
        List<Position> validMoves = new List<Position>();

        int[] directions = { -1, 1 }; // Diagonal directions
        foreach (int rowDir in directions) {
            foreach (int colDir in directions) {
                Position pos = CurrentPosition;
                while (true) {
                    pos = new Position(pos.Row + rowDir, pos.Col + colDir);
                    if (!Board.IsInsideBoard(pos)) break;
                    
                    Piece? pieceAtDest = board.GetPieceAt(pos);
                    if (pieceAtDest == null) {
                        validMoves.Add(pos);
                    } else {
                        if (pieceAtDest.Color != this.Color) validMoves.Add(pos); // Kill
                        break; // Stop if piece blocks path
                    }
                }
            }
        }
        return validMoves;
    }
}