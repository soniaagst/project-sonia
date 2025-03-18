public class Bishop : Piece {
    public Bishop(Colors color, Box position) : base(color, position) {}

    public override List<Box> GetValidMoves(Board board) {
        List<Box> validMoves = new List<Box>();
        int[] directions = [ -1, 1 ];

        foreach (int rowDir in directions) {
            foreach (int colDir in directions) {
                Box destination = CurrentPosition;

                while (true) {
                    destination = new Box(destination.Row + rowDir, destination.Col + colDir);
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