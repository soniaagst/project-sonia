
public class Rook : Piece {
    public Rook(PieceColor color, Position position) : base(color, position) {}

    public override List<Position> GetValidMoves(Board board) {
        List<Position> validMoves = new();
        Position[] directions = {
            new Position(-1, 0), // Up
            new Position(1, 0),  // Down
            new Position(0, -1), // Left
            new Position(0, 1)   // Right
        };

        foreach (var dir in directions) {
            Position current = CurrentPosition;
            while (true) {
                current = new Position(current.Row + dir.Row, current.Col + dir.Col);
                if (!Board.IsInsideBoard(current)) break;

                Piece? piece = board.GetPieceAt(current);
                if (piece == null) {
                    validMoves.Add(current);
                } else {
                    if (piece.Color != Color) validMoves.Add(current); // Kill
                    break; // Stop when blocked
                }
            }
        }

        return validMoves;
    }
}