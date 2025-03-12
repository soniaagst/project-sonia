public class Board {
    private Piece?[,] _grid;
    private Action<Position, Position> swap;

    public Board() {
        _grid = new Piece?[8,8];
        initializeBoard();
        swap = (currentPosition, newPosition) => {
            (_grid[currentPosition.Row, currentPosition.Col], _grid[newPosition.Row, newPosition.Col]) = (_grid[newPosition.Row, newPosition.Col], _grid[currentPosition.Row, currentPosition.Col]);
        };
    }

    private void initializeBoard() {
        for (int i = 0; i < 8; i++) {
            _grid[6,i] = new Pawn(PieceColor.White,6,i);
            _grid[1,i] = new Pawn(PieceColor.Black,1,i);
        }
        _grid[7,0] = new Rook(PieceColor.White,7,0);
        _grid[7,7] = new Rook(PieceColor.White,7,7);
        _grid[7,1] = new Knight(PieceColor.White,7,1);
        _grid[7,6] = new Knight(PieceColor.White,7,6);
        _grid[7,2] = new Bishop(PieceColor.White,7,2);
        _grid[7,5] = new Bishop(PieceColor.White,7,5);
        _grid[7,3] = new Queen(PieceColor.White,7,3);
        _grid[7,4] = new King(PieceColor.White,7,4);
        
        _grid[0,0] = new Rook(PieceColor.Black,0,0);
        _grid[0,7] = new Rook(PieceColor.Black,0,7);
        _grid[0,1] = new Knight(PieceColor.Black,0,1);
        _grid[0,6] = new Knight(PieceColor.Black,0,6);
        _grid[0,2] = new Bishop(PieceColor.Black,0,2);
        _grid[0,5] = new Bishop(PieceColor.Black,0,5);
        _grid[0,3] = new Queen(PieceColor.Black,0,3);
        _grid[0,4] = new King(PieceColor.Black,0,4);
    }

    public Piece?[,] GetBoard() {
        return _grid;
    }

    public Piece? GetPieceAt(Position position) {
        return _grid[position.Row, position.Col];
    }

    public bool MovePiece(Position currentPosition, Position newPosition, out Position? lastMovementOrigin) {
        Piece? piece = GetPieceAt(currentPosition);
        if (piece is Pawn) {
            int vertDistance = Math.Abs(newPosition.Row - currentPosition.Row);
            int horiDistance = Math.Abs(newPosition.Col - currentPosition.Col);
            if (vertDistance == 1 && horiDistance == 1) {
                Piece? targetPiece = GetPieceAt(newPosition);
                if (targetPiece is not null && targetPiece.Color != piece.Color) {
                    Piece.Kill(targetPiece);
                    lastMovementOrigin = currentPosition;
                    swap(currentPosition, newPosition);
                    return true;
                }
                else {lastMovementOrigin = null; return false;}
            }
            else if(piece.Move(newPosition, out lastMovementOrigin)) {
                swap(currentPosition, newPosition);
                return true;
            }
            else {lastMovementOrigin = null; return false;}
        }
        else if(piece!.Move(newPosition, out lastMovementOrigin)) {
            swap(currentPosition, newPosition);
            return true;
        }
        else {lastMovementOrigin = null; return false;}
    }

    public void KillPiece(Piece targetPiece) {
        Position position = targetPiece.CurrentPosition;
        Piece.Kill(targetPiece);
        _grid[position.Row, position.Col] = null;
    }
}