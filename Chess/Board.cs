public class Board {
    private Piece?[,] _boxes;
    private Box _nowhere = new Box(99,99);

    public Board() {
        _boxes = new Piece?[8,8];

        for (int i = 0; i < 8; i++) {
            _boxes[6,i] = new Pawn(Colors.White,new Box(6,i));
            _boxes[1,i] = new Pawn(Colors.Black,new Box(1,i));
        }
        
        _boxes[7,0] = new Rook(Colors.White,new Box(7,0));
        _boxes[7,7] = new Rook(Colors.White,new Box(7,7));
        _boxes[7,1] = new Knight(Colors.White,new Box(7,1));
        _boxes[7,6] = new Knight(Colors.White,new Box(7,6));
        _boxes[7,2] = new Bishop(Colors.White,new Box(7,2));
        _boxes[7,5] = new Bishop(Colors.White,new Box(7,5));
        _boxes[7,3] = new Queen(Colors.White,new Box(7,3));
        _boxes[7,4] = new King(Colors.White,new Box(7,4));
        
        _boxes[0,0] = new Rook(Colors.Black,new Box(0,0));
        _boxes[0,7] = new Rook(Colors.Black,new Box(0,7));
        _boxes[0,1] = new Knight(Colors.Black,new Box(0,1));
        _boxes[0,6] = new Knight(Colors.Black,new Box(0,6));
        _boxes[0,2] = new Bishop(Colors.Black,new Box(0,2));
        _boxes[0,5] = new Bishop(Colors.Black,new Box(0,5));
        _boxes[0,3] = new Queen(Colors.Black,new Box(0,3));
        _boxes[0,4] = new King(Colors.Black,new Box(0,4));
    }

    public Piece?[,] GetBoard() {
        return _boxes;
    }

    public List<Piece> GetPieces() {
        List<Piece> pieces = [];
        foreach(var piece in GetBoard())
            if (piece != null)
                pieces.Add(piece);
        return pieces;
    }

    public Piece? GetPieceAt(Box position) {
        return _boxes[position.Y, position.X];
    }

    public static bool IsInsideBoard(Box position) {
        return position.Y >= 0 && position.Y <= 7 && position.X >=0 && position.X <= 7;
    }

    public bool MovePiece(Box from, Box to, Action<Piece?, Piece?, Pawn?> onMoveMade)
    {
        Piece? movingPiece = GetPieceAt(from);
        Piece? killedPiece = GetPieceAt(to);
        Pawn? promotedPawn = null;

        if (movingPiece == null) return false;

        if (movingPiece is Pawn movingPawn) movingPawn.JustForwardTwo = false;

        // Promotion
        if (movingPiece is Pawn pawn && (to.Y == 0 || to.Y == 7)) {
            promotedPawn = pawn;
        }

        // If a pawn just moves two steps forward, the enemy's pawn at immidiate side can en passant
        if (movingPiece is Pawn justforwardtwoPawn && Math.Abs(to.Y - from.Y) == 2) {
            justforwardtwoPawn.JustForwardTwo = true;
            List<Box> adjacentPositions = [new (to.Y, to.X-1), new (to.Y, to.X + 1)];
            foreach (var adjacentPosition in adjacentPositions) {
                if (GetPieceAt(adjacentPosition) is Pawn enemyPawn && enemyPawn.Color != movingPiece.Color && IsInsideBoard(adjacentPosition)) {
                    enemyPawn.CanEnPassant = true;
                }
            }
        }

        // En Passant
        if (movingPiece is Pawn enpassantPawn && enpassantPawn.CanEnPassant && Math.Abs(to.X - from.X) == 1) {
            int stepback = enpassantPawn.Color == Colors.White? 1 : -1;
            Box behind = new Box(to.Y + stepback, to.X);
            if (GetPieceAt(behind) is Pawn enemyPawn && enemyPawn?.Color != enpassantPawn.Color) {
                    killedPiece = enemyPawn;
            }
            else return false;
        }

        if (movingPiece is Pawn movedPawn) movedPawn.CanEnPassant = false;

        // Castling
        if (movingPiece is King king && Math.Abs(from.X - to.X) == 2) {
            int direction = to.X - king.CurrentPosition.X; // Short castle to the right
            int rookOldCol = direction == 1? 7 : 0;
            int rookNewCol = direction == 1? 5 : 3;
            Box rookOldPos = new Box(king.CurrentPosition.Y, rookOldCol);
            Box rookNewPos = new Box(king.CurrentPosition.Y, rookNewCol);

            // Move Rook (King already moved by normal move)
            Piece? rook = GetPieceAt(rookOldPos);
            if (rook is Rook) {
                _boxes[rookNewPos.Y, rookNewPos.X] = rook;
                _boxes[rookOldPos.Y, rookOldPos.X] = null;
                rook.CurrentPosition = rookNewPos;
                rook.IsMoved = true;
            }
        } 

        // Normal move
        if (killedPiece is not null) {
            _boxes[killedPiece.CurrentPosition.Y, killedPiece.CurrentPosition.X] = null; 
        }

        _boxes[to.Y, to.X] = movingPiece;
        _boxes[from.Y, from.X] = null;
        movingPiece.CurrentPosition = to;
        movingPiece.IsMoved = true;
        onMoveMade?.Invoke(movingPiece, killedPiece, promotedPawn);

        return true;
    }

    public void KillPiece(Piece targetPiece) {
        targetPiece.CurrentPosition = _nowhere;
        targetPiece.IsKilled = true;
    }

    public bool IsUnderAttack(Box pos, Colors color) {
        foreach (var enemyPiece in _boxes) {
            if (enemyPiece is not null && enemyPiece.Color != color) {
                if (enemyPiece is King) continue;
                List<Box> enemyMoves = enemyPiece.GetValidMoves(this);
                if (enemyMoves.Contains(pos)) {
                    return true;
                }
            }
        }
        return false;
    }

    public bool IsFriendlyPieceAt(Box pos, Colors color) {
        Piece? piece = GetPieceAt(pos);
        return piece != null && piece.Color == color;
    }

    public King? FindKing(Colors color) {
        King? king = null;
        foreach (Piece? piece in _boxes) {
            if (piece is not null && piece.Color == color && piece is King) {
                king = (King)piece;
            }
        }
        return king;
    }

    public Piece? SimulateMove(Box from, Box to) {
        Piece? killedPiece = GetPieceAt(to);

        _boxes[to.Y, to.X] = _boxes[from.Y, from.X];
        _boxes[from.Y, from.X] = null;
        
        return killedPiece;
    }

    public void UndoSimulation(Box origin, Box simulatedMove, Piece? killedPiece) {
        _boxes[origin.Y, origin.X] = _boxes[simulatedMove.Y, simulatedMove.X];
        _boxes[simulatedMove.Y, simulatedMove.X] = killedPiece;
    }

    public string GenerateBoardState() {
        System.Text.StringBuilder sb = new();
        
        for (int row = 0; row < 8; row++) {
            int emptyCount = 0;

            for (int col = 0; col < 8; col++) {
                Piece? piece = _boxes[row, col];

                if (piece == null) {
                    emptyCount++;
                } else {
                    if (emptyCount > 0) {
                        sb.Append(emptyCount);  // Append the number of empty squares
                        emptyCount = 0;
                    }
                    
                    char pieceChar = piece switch {
                        Pawn   => 'P',
                        Knight => 'N',
                        Bishop => 'B',
                        Rook   => 'R',
                        Queen  => 'Q',
                        King   => 'K',
                        _      => '?'
                    };

                    sb.Append(piece.Color == Colors.White ? pieceChar : char.ToLower(pieceChar));
                }
            }

            if (emptyCount > 0) {
                sb.Append(emptyCount);
            }

            if (row < 7) sb.Append('/');  // Separate ranks with '/'
        }

        return sb.ToString();
    }

}