public class Display {
    public void DisplayBoard(Board board, Position? lastMovementOrigin = null) {
        Console.WriteLine("  A  B  C  D  E  F  G  H  ");
        for (int row = 0; row < 8; row++) {
            Console.Write(8-row);
            for (int col = 0; col < 8; col++) {
                if (lastMovementOrigin?.Row == row && lastMovementOrigin?.Col == col) {
                    Console.Write(" ＊");
                }
                else if (board.GetBoard()[row, col] == null) {
                    Console.Write(" ・");
                }
                else {
                    Piece piece = board.GetBoard()[row, col]!;
                    if (piece!.Color is PieceColor.White) {
                        if (piece is Pawn) Console.Write(" ♙ ");
                        if (piece is Knight) Console.Write(" ♘ ");
                        if (piece is Bishop) Console.Write(" ♗ ");
                        if (piece is Rook) Console.Write(" ♖ ");
                        if (piece is Queen) Console.Write(" ♕ ");
                        if (piece is King) Console.Write(" ♔ ");
                    }
                    else {
                        if (piece is Pawn) Console.Write(" ♟ ");
                        if (piece is Knight) Console.Write(" ♞ ");
                        if (piece is Bishop) Console.Write(" ♝ ");
                        if (piece is Rook) Console.Write(" ♜ ");
                        if (piece is Queen) Console.Write(" ♛ ");
                        if (piece is King) Console.Write(" ♚ ");
                    }
                }
            }
            Console.WriteLine($" {8-row}");
        }
        Console.WriteLine("  A  B  C  D  E  F  G  H  ");
    }

    public void DisplayMessage(string message) {
        Console.WriteLine(message);
    }
}

public static class Utility {
    private static string AskNonNullInput(string? message = null) {
        Console.Write(message);
        string? input = Console.ReadLine();
        while (string.IsNullOrEmpty(input)) {
            Console.WriteLine("Input cannot empty. Try again.");
            Console.Write(message);
            input = Console.ReadLine();
        }
        return input;
    }

    public static (Position, Position) ParseInput(string message) {
        string input = AskNonNullInput(message);
        while (input.Split(' ').Count() != 2) {
            Console.WriteLine("Invalid input. Try again.");
            input = AskNonNullInput(message);
        }
        while (input.Split(' ')[0].ToCharArray().Count() !=2 || input.Split(' ')[1].ToCharArray().Count() !=2) {
            Console.WriteLine("Invalid input. Try again.");
            input = AskNonNullInput(message);
        }
        (Position currentPos, Position newPos) = (new(), new());
        Position[] positions = [currentPos, newPos];
        string[] pos = input.Split(' ');
        for (int i = 0; i < 2; i++) {
            char[] rowcol = pos[i].ToCharArray();
            (rowcol[0], rowcol[1]) = (rowcol[1], rowcol[0]); //swapping
            int row = 56 - rowcol[0];
            int col = rowcol[1] - 'A';
            positions[i].Row = row; positions[i].Col = col;
        }
        (currentPos, newPos) = (positions[0], positions[1]);
        if (!IsInsideBoard(currentPos) || !IsInsideBoard(newPos)) {
            Console.WriteLine("Invalid input. Try again.");
            return ParseInput(message);
        }
        return (currentPos, newPos);
    }

    private static bool IsInsideBoard(Position position) {
        if (position.Row < 0 || position.Row > 7 || position.Col < 0 || position.Col > 7) return false;
        return true;
    }
}

public enum PieceColor {
    White, Black
}

public enum GameStatus {
    Running, Finished
}

public struct Position {
    public int Row;
    public int Col;
    public Position(int row, int col) {
        Row = row; Col = col;
    }
}

public enum PromoteOption {
    Queen, Rook, Bishop, Knight
}

public enum PlayerStatus {
    Normal, Checked, Checkmate, Won, Stalemate, Draw, Resigned
}