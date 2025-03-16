public class Board {
    private Piece?[,] _grid;
    private Position _abyss = new Position(99,99);

    public Board() {
        _grid = new Piece?[8,8];
        initializeBoard();
    }

    private void initializeBoard() {
        for (int i = 0; i < 8; i++) {
            _grid[6,i] = new Pawn(PieceColor.White,new Position(6,i));
            _grid[1,i] = new Pawn(PieceColor.Black,new Position(1,i));
        }
        _grid[7,0] = new Rook(PieceColor.White,new Position(7,0));
        _grid[7,7] = new Rook(PieceColor.White,new Position(7,7));
        _grid[7,1] = new Knight(PieceColor.White,new Position(7,1));
        _grid[7,6] = new Knight(PieceColor.White,new Position(7,6));
        _grid[7,2] = new Bishop(PieceColor.White,new Position(7,2));
        _grid[7,5] = new Bishop(PieceColor.White,new Position(7,5));
        _grid[7,3] = new Queen(PieceColor.White,new Position(7,3));
        _grid[7,4] = new King(PieceColor.White,new Position(7,4));
        
        _grid[0,0] = new Rook(PieceColor.Black,new Position(0,0));
        _grid[0,7] = new Rook(PieceColor.Black,new Position(0,7));
        _grid[0,1] = new Knight(PieceColor.Black,new Position(0,1));
        _grid[0,6] = new Knight(PieceColor.Black,new Position(0,6));
        _grid[0,2] = new Bishop(PieceColor.Black,new Position(0,2));
        _grid[0,5] = new Bishop(PieceColor.Black,new Position(0,5));
        _grid[0,3] = new Queen(PieceColor.Black,new Position(0,3));
        _grid[0,4] = new King(PieceColor.Black,new Position(0,4));
    }

    public Piece?[,] GetBoard() {
        return _grid;
    }

    public Piece? GetPieceAt(Position position) {
        return _grid[position.Row, position.Col];
    }

    public static bool IsInsideBoard(Position position) {
        return position.Row >= 0 && position.Row <= 7 && position.Col >=0 && position.Col <= 7;
    }

    public bool MovePiece(Position currentPosition, Position newPosition, out Piece? killedPiece, out Pawn? promotedPawn) {
        Piece? movingPiece = GetPieceAt(currentPosition);
        killedPiece = GetPieceAt(newPosition);
        promotedPawn = null;
        if (movingPiece == null) return false;
        if (movingPiece is Pawn pawn) pawn.CanEnPassant = false;

        // if a pawn reached the edge, it's a promotion
        if (movingPiece is Pawn && (newPosition.Row == 0 || newPosition.Row == 7)) {
            promotedPawn = (Pawn)movingPiece;
        }

        // // If a pawn moves two steps forward, the enemy's pawn at immidiate side can en passant
        // if (movingPiece is Pawn && Math.Abs(newPosition.Row - currentPosition.Row) == 2) {
        //     List<Position> adjacentPositions = [new (newPosition.Row, newPosition.Col-1), new (newPosition.Row, newPosition.Col + 1)];
        //     foreach (var adjacentPosition in adjacentPositions) {
        //         if (GetPieceAt(adjacentPosition) is Pawn enemyPawn && enemyPawn.Color != movingPiece.Color && IsInsideBoard(adjacentPosition)) {
        //             enemyPawn.CanEnPassant = true;
        //         }
        //     }
        // }

        // If King moves two steps, it's a castle. Move Rook too.
        if (movingPiece is King king && Math.Abs(currentPosition.Col - newPosition.Col) == 2) {
            HandleCastling(king, newPosition);
        } 

        // Normal move
        _grid[newPosition.Row, newPosition.Col] = movingPiece;
        _grid[currentPosition.Row, currentPosition.Col] = null;
        movingPiece.CurrentPosition = newPosition;
        movingPiece.IsMoved = true;
        return true;
    }

    public void KillPiece(Piece targetPiece) {
        Position position = targetPiece.CurrentPosition;
        _grid[position.Row, position.Col] = null;
        targetPiece.CurrentPosition = _abyss;
    }

    public void ReplacePiece(Pawn pawn, Piece promotedPiece) {
        Position position = pawn.CurrentPosition;
        pawn.CurrentPosition = _abyss;
        _grid[position.Row, position.Col] = promotedPiece;
    }

    private void HandleCastling(King king, Position kingNewPos) {
        int direction = kingNewPos.Col > king.CurrentPosition.Col ? 1 : -1; // Short castle to the right
        int rookOldCol = direction == 1? 7 : 0;
        int rookNewCol = direction == 1? 5 : 3;
        Position rookOldPos = new Position(king.CurrentPosition.Row, rookOldCol);
        Position rookNewPos = new Position(king.CurrentPosition.Row, rookNewCol);

        // Move Rook (King already moved by normal move)
        Piece? rook = GetPieceAt(rookOldPos);
        if (rook is Rook) {
            _grid[rookNewPos.Row, rookNewPos.Col] = rook;
            _grid[rookOldPos.Row, rookOldPos.Col] = null;
            rook.CurrentPosition = rookNewPos;
            rook.IsMoved = true;
        }
    }

    public bool IsUnderAttack(Position pos, PieceColor color) {
        foreach (var enemyPiece in _grid) {
            if (enemyPiece is not null && enemyPiece.Color != color) {
                if (enemyPiece is King) continue;
                List<Position> enemyMoves = enemyPiece.GetValidMoves(this);
                if (enemyMoves.Contains(pos)) {
                    return true;
                }
            }
        }
        return false;
    }

    public bool IsFriendlyPieceAt(Position pos, PieceColor color) {
        Piece? piece = GetPieceAt(pos);
        return piece != null && piece.Color == color;
    }

    public King? FindKing(PieceColor color) {
        King? king = null;
        foreach (Piece? piece in _grid) {
            if (piece is not null && piece.Color == color && piece is King) {
                king = (King)piece;
            }
        }
        return king;
    }
}