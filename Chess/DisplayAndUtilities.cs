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
    public static void AskNonNullInput(string? message = null) {
        Console.Write(message);
        string? input = Console.ReadLine();
        while (string.IsNullOrEmpty(input)) {
            Console.WriteLine("Input cannot empty. Try again.");
            Console.Write(message);
            input = Console.ReadLine();
        }
    }

    // public static bool TryParseInput(out Movement movement) {
    //     // split positions apart on ' '
    //     // check if they're valid position type
    //     // check if out of board
    //     return true;
    // }
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