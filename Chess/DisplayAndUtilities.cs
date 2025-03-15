public class Display : IDisplay {
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

    public PromoteOption AskPromotionChoice() {
        DisplayMessage("Pawn promoted! Options:\n (0)Queen\n (1)Rook\n (2)Bishop \n (3)Knight");
        string choicestr = AskNonNullInput("Choose your promotion (the default is 0)");
        int choice;
        int.TryParse(choicestr, out choice);
        return (PromoteOption)choice;
    }

    public string AskNonNullInput(string? message) {
        Console.Write(message);
        string? input = Console.ReadLine()?.ToUpper();
        while (string.IsNullOrEmpty(input)) {
            Console.WriteLine("Input cannot empty. Try again.");
            Console.Write(message);
            input = Console.ReadLine()?.ToUpper();
        }
        return input;
    }

    public bool TryParseMove(string input, out Movement? movement) {
        if (input.Split(' ').Count() != 2) {
            movement = null;
            return false;
        }
        if (input.Split(' ')[0].ToCharArray().Count() !=2 || input.Split(' ')[1].ToCharArray().Count() !=2) {
            movement = null;
            return false;
        }
        Position[] move = [new(), new()]; // [from, to]
        string[] pos = input.Split(' ');
        for (int i = 0; i < 2; i++) {
            char[] rowcol = pos[i].ToCharArray();
            (rowcol[0], rowcol[1]) = (rowcol[1], rowcol[0]);
            int row = 56 - rowcol[0];
            int col = rowcol[1] - 'A';
            move[i].Row = row; move[i].Col = col;
        }
        if (!Board.IsInsideBoard(move[0]) || !Board.IsInsideBoard(move[1])) {
            movement = null;
            return false;
        }
        movement = new Movement(move[0], move[1]);
        return true;
    }
}

public interface IDisplay {
    public void DisplayBoard(Board board) {}
    public void DisplayMessage(string message) {}
    public PromoteOption AskPromotionChoice();
    public string AskNonNullInput(string? message);
    public bool TryParseMove(string input, out Movement? movement);
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