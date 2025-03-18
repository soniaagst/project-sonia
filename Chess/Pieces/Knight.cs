public class Knight : Piece {
    public Knight(Colors color, Box position) : base(color, position) { }

    public override List<Box> GetValidMoves(Board board) {
        List<Box> validMoves = new List<Box>();
        List<List<int>> offsets = [[2,1], [2,-1], [-2,1], [-2,-1], [1,2], [1,-2], [-1,2], [-1,-2]];

        foreach (var offset in offsets) {
            Box newPos = new Box(CurrentPosition.Y + offset[0], CurrentPosition.X + offset[1]);

            if (Board.IsInsideBoard(newPos)) {
                Piece? pieceAtNewPos = board.GetPieceAt(newPos);
                if (pieceAtNewPos == null || pieceAtNewPos.Color != Color) {
                    validMoves.Add(newPos);
                }
            }
        }
        return validMoves;
    }
}
