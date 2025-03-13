public class Knight : Piece {
    public Knight(PieceColor color, Position position) : base(color, position) { }

    public override List<Position> GetValidMoves(Board board) {
        List<Position> moves = new List<Position>();
        List<List<int>> offsets = [[2,1], [2,-1], [-2,1], [-2,-1], [1,2], [1,-2], [-1,2], [-1,-2]];

        foreach (var offset in offsets) {
            Position newPos = new Position(CurrentPosition.Row + offset[0], CurrentPosition.Col + offset[1]);

            if (Board.IsInsideBoard(newPos)) {
                Piece? pieceAtNewPos = board.GetPieceAt(newPos);
                if (pieceAtNewPos == null || pieceAtNewPos.Color != this.Color) {
                    moves.Add(newPos); // Empty square or capture
                }
            }
        }
        return moves;
    }
}
