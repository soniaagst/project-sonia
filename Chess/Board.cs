public class Board {
    private Piece?[,] _grid;

    public Board() {
        _grid = new Piece?[8,8];
        initializeBoard();
    }

    private void initializeBoard() {
        for (int i = 0; i < 8; i++) {
            _grid[6,i] = new Pawn(PieceColor.White);
            _grid[1,i] = new Pawn(PieceColor.Black);
        }
        _grid[7,0] = new Rook(PieceColor.White);
        _grid[7,7] = new Rook(PieceColor.White);
        _grid[7,1] = new Knight(PieceColor.White);
        _grid[7,6] = new Knight(PieceColor.White);
        _grid[7,2] = new Bishop(PieceColor.White);
        _grid[7,5] = new Bishop(PieceColor.White);
        _grid[7,3] = new Queen(PieceColor.White);
        _grid[7,4] = new King(PieceColor.White);
        
        _grid[0,0] = new Rook(PieceColor.Black);
        _grid[0,7] = new Rook(PieceColor.Black);
        _grid[0,1] = new Knight(PieceColor.Black);
        _grid[0,6] = new Knight(PieceColor.Black);
        _grid[0,2] = new Bishop(PieceColor.Black);
        _grid[0,5] = new Bishop(PieceColor.Black);
        _grid[0,3] = new Queen(PieceColor.Black);
        _grid[0,4] = new King(PieceColor.Black);
    }

    public Piece?[,] GetBoard() {
        return _grid;
    }

    public Piece? GetPieceAt(Position position) {
        return _grid[position.Row, position.Col];
    }
}