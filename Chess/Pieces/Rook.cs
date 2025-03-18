
public class Rook : Piece {
    public Rook(Colors color, Box position) : base(color, position) {}

    public override List<Box> GetValidMoves(Board board) {
        List<Box> validMoves = new();
        int[][] directions = [ [-1,0], [1,0], [0,-1], [0,1] ];

        foreach (var dir in directions) {
            Box destination = CurrentPosition;
            
            while (true) {
                destination = new Box(destination.Row + dir[0], destination.Col + dir[1]);
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

        return validMoves;
    }
}