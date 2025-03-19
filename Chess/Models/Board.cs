using Chess.Models.Pieces;
using Chess.Enums;

namespace Chess.Models;
public class Board
{
    private Piece?[,] _grid;
    private Position _nowhere = new(99, 99);

    public Board()
    {
        _grid = new Piece?[8, 8];

        for (int i = 0; i < 8; i++)
        {
            _grid[6, i] = new Pawn(PieceColor.White, new Position(6, i));
            _grid[1, i] = new Pawn(PieceColor.Black, new Position(1, i));
        }

        _grid[7, 0] = new Rook(PieceColor.White, new Position(7, 0));
        _grid[7, 7] = new Rook(PieceColor.White, new Position(7, 7));
        _grid[7, 1] = new Knight(PieceColor.White, new Position(7, 1));
        _grid[7, 6] = new Knight(PieceColor.White, new Position(7, 6));
        _grid[7, 2] = new Bishop(PieceColor.White, new Position(7, 2));
        _grid[7, 5] = new Bishop(PieceColor.White, new Position(7, 5));
        _grid[7, 3] = new Queen(PieceColor.White, new Position(7, 3));
        _grid[7, 4] = new King(PieceColor.White, new Position(7, 4));

        _grid[0, 0] = new Rook(PieceColor.Black, new Position(0, 0));
        _grid[0, 7] = new Rook(PieceColor.Black, new Position(0, 7));
        _grid[0, 1] = new Knight(PieceColor.Black, new Position(0, 1));
        _grid[0, 6] = new Knight(PieceColor.Black, new Position(0, 6));
        _grid[0, 2] = new Bishop(PieceColor.Black, new Position(0, 2));
        _grid[0, 5] = new Bishop(PieceColor.Black, new Position(0, 5));
        _grid[0, 3] = new Queen(PieceColor.Black, new Position(0, 3));
        _grid[0, 4] = new King(PieceColor.Black, new Position(0, 4));
    }

    public Piece?[,] GetBoard()
    // absolutely needed in the controller for operations related to position
    {
        return _grid;
    }

    public List<Piece> GetPieces()
    {
        List<Piece> pieces = [];
        foreach (var piece in GetBoard())
            if (piece != null)
                pieces.Add(piece);
        return pieces;
    }

    public Piece? GetPieceAt(Position position)
    {
        return _grid[position.Row, position.Col];
    }

    public static bool IsInsideBoard(Position position)
    {
        return position.Row >= 0 && position.Row <= 7 && position.Col >= 0 && position.Col <= 7;
    }

    public bool MovePiece(Position from, Position to, Action<Piece?, Piece?, Pawn?> onMoveMade)
    {
        Piece? movingPiece = GetPieceAt(from);
        Piece? killedPiece = GetPieceAt(to);
        Pawn? promotedPawn = null;

        if (movingPiece == null) return false;

        if (movingPiece is Pawn movingPawn) movingPawn.JustForwardTwo = false;

        // Promotion
        if (movingPiece is Pawn pawn && (to.Row == 0 || to.Row == 7))
        {
            promotedPawn = pawn;
        }

        // If a pawn just moves two steps forward, the enemy's pawn at immidiate side can en passant
        if (movingPiece is Pawn justforwardtwoPawn && Math.Abs(to.Row - from.Row) == 2)
        {
            justforwardtwoPawn.JustForwardTwo = true;
            List<Position> adjacentPositions = [new(to.Row, to.Col - 1), new(to.Row, to.Col + 1)];
            foreach (var adjacentPosition in adjacentPositions)
            {
                if (GetPieceAt(adjacentPosition) is Pawn enemyPawn && enemyPawn.Color != movingPiece.Color && IsInsideBoard(adjacentPosition))
                {
                    enemyPawn.CanEnPassant = true;
                }
            }
        }

        // Handle En Passant
        if (movingPiece is Pawn enpassantPawn && enpassantPawn.CanEnPassant && Math.Abs(to.Col - from.Col) == 1)
        {
            int stepback = enpassantPawn.Color == PieceColor.White ? 1 : -1;
            Position behind = new(to.Row + stepback, to.Col);
            if (GetPieceAt(behind) is Pawn enemyPawn && enemyPawn?.Color != enpassantPawn.Color)
            {
                killedPiece = enemyPawn;
            }
            else return false;
        }

        if (movingPiece is Pawn movedPawn) movedPawn.CanEnPassant = false;

        // Handle castling
        if (movingPiece is King king && Math.Abs(from.Col - to.Col) == 2)
        {
            int direction = to.Col - king.CurrentPosition.Col > 1 ? 1 : -1;
            int rookOldCol = direction == 1 ? 7 : 0;
            int rookNewCol = direction == 1 ? 5 : 3;
            Position rookOldPos = new(king.CurrentPosition.Row, rookOldCol);
            Position rookNewPos = new(king.CurrentPosition.Row, rookNewCol);

            // Move Rook
            Piece? rook = GetPieceAt(rookOldPos);
            if (rook is Rook)
            {
                _grid[rookNewPos.Row, rookNewPos.Col] = rook;
                _grid[rookOldPos.Row, rookOldPos.Col] = null;
                rook.CurrentPosition = rookNewPos;
                rook.IsMoved = true;
            }
        }

        // Normal move
        if (killedPiece is not null)
        {
            _grid[killedPiece.CurrentPosition.Row, killedPiece.CurrentPosition.Col] = null;
        }

        _grid[to.Row, to.Col] = movingPiece;
        _grid[from.Row, from.Col] = null;
        movingPiece.CurrentPosition = to;
        movingPiece.IsMoved = true;
        onMoveMade?.Invoke(movingPiece, killedPiece, promotedPawn);

        return true;
    }

    public void KillPiece(Piece targetPiece)
    {
        targetPiece.CurrentPosition = _nowhere;
        targetPiece.IsKilled = true;
    }

    public bool IsUnderAttack(Position pos, PieceColor color)
    {
        foreach (var enemyPiece in _grid)
        {
            if (enemyPiece is not null && enemyPiece.Color != color)
            {
                if (enemyPiece is King) continue;
                List<Position> enemyMoves = enemyPiece.GetValidMoves(this);
                if (enemyMoves.Contains(pos))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool IsFriendlyPieceAt(Position pos, PieceColor color)
    {
        Piece? piece = GetPieceAt(pos);
        return piece != null && piece.Color == color;
    }

    public King? FindKing(PieceColor color)
    {
        King? king = null;
        foreach (Piece? piece in _grid)
        {
            if (piece is not null && piece.Color == color && piece is King)
            {
                king = (King)piece;
            }
        }
        return king;
    }

    public Piece? SimulateMove(Position from, Position to)
    {
        Piece? killedPiece = GetPieceAt(to);

        _grid[to.Row, to.Col] = _grid[from.Row, from.Col];
        _grid[from.Row, from.Col] = null;

        return killedPiece;
    }

    public void UndoSimulation(Position origin, Position simulatedMove, Piece? killedPiece)
    {
        _grid[origin.Row, origin.Col] = _grid[simulatedMove.Row, simulatedMove.Col];
        _grid[simulatedMove.Row, simulatedMove.Col] = killedPiece;
    }
}