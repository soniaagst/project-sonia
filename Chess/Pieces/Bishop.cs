public class Bishop : Piece {
    public Bishop(Colors color, Position position) : base(color, position) {}

    public override List<Position> GetValidMoves(Board board) {
        List<Position> validMoves = new List<Position>();
        int[] directions = [ -1, 1 ];

        foreach (int rowDir in directions) {
            foreach (int colDir in directions) {
                Position destination = CurrentPosition;

                while (true) {
                    destination = new Position(destination.Row + rowDir, destination.Col + colDir);
                    if (!Board.IsInsideBoard(destination)) break;
                    
                    Piece? pieceAtDestination = board.GetPieceAt(destination);
                    if (pieceAtDestination == null) {
                        validMoves.Add(destination);
                    } else {
                        if (pieceAtDestination.Color != Color) validMoves.Add(destination);
                        break;
                    }
                }
            }
        }
        return validMoves;
    }
}