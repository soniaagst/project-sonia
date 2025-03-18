
public class Queen : Piece {
    public Queen(Colors color, Box position) : base(color, position) {}

    public override List<Box> GetValidMoves(Board board) {
        List<Box> validMoves = new();

        List<List<int>> directions = [];
        for (int row = -1; row <= 1; row++) {
            for (int col = -1; col <= 1; col++) {
                if (row != 0 || col != 0) directions.Add([row,col]);
            }
        }

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