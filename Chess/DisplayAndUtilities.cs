public class Display : IDisplay {
    public void DisplayBoard(Board board, Box? lastMoveOrigin) {
        Console.WriteLine("  A  B  C  D  E  F  G  H  ");
        for (int row = 0; row < 8; row++) {
            Console.Write(8-row);
            for (int col = 0; col < 8; col++) {
                if (lastMoveOrigin?.Y == row && lastMoveOrigin?.X == col) {
                    Console.Write(" ＊");
                }
                else if (board.GetBoard()[row, col] == null) {
                    Console.Write(" ・");
                }
                else {
                    Piece piece = board.GetBoard()[row, col]!;
                    if (piece!.Color is Colors.White) {
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

    public void DisplayHistory(List<Move> movesHistory) {
        foreach (var item in movesHistory) {
            string pieceChar = item.Piece!.ToString();
            string dest = item.Destination.ToString();
            string kill = item.IsKill? "x" : "";
            string check = item.IsCheck? "+" : "";
            string promotion = item.IsPromotion? "=" : "";
            string promotedTo = item.IsPromotion? item.PromotedPiece!.ToString() : "";
            string shortCastle = item.IsShortCastle? "O-O" : "";
            string longCastle = item.IsLongCastle? "O-O-O" : "";
                
            if (item.IsShortCastle || item.IsLongCastle) {
                Console.Write($"{shortCastle}{longCastle}{check} ");
            }
            Console.Write($"{pieceChar}{kill}{dest}{promotion}{promotedTo}{check} ");
            
            if (movesHistory.IndexOf(item)%2 == 1) {
                Console.WriteLine();
            }
        }
        Console.WriteLine();
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
        Box[] move = [new(), new()]; // [from, to]
        string[] pos = input.Split(' ');
        for (int i = 0; i < 2; i++) {
            char[] rowcol = pos[i].ToCharArray();
            (rowcol[0], rowcol[1]) = (rowcol[1], rowcol[0]);
            int row = 56 - rowcol[0];
            int col = rowcol[1] - 'A';
            move[i].Y = row; move[i].X = col;
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
    public void DisplayBoard(Board board, Box? lastMoveOrigin);
    public void DisplayHistory(List<Move> movesHistory) ;
    public void DisplayMessage(string message);
    public PromoteOption AskPromotionChoice();
    public string AskNonNullInput(string? message);
    public bool TryParseMove(string input, out Movement? movement);
}

public enum Colors {
    White, Black
}

public enum GameStatus {
    Running, Finished
}

public struct Box {
    public int Y;
    public int X;
    public Box(int row, int col) {
        Y = row; X = col;
    }

    public Colors GetBoxColor() {
        return (Colors)((Y + X) % 2);
    }

    public override readonly string ToString()
    {
        char colLetter = X switch {
            0 => 'a',
            1 => 'b',
            2 => 'c',
            3 => 'd',
            4 => 'e',
            5 => 'f',
            6 => 'g',
            7 => 'h',
            _ => '?'
        };
        return $"{colLetter}{8-Y}";
    }
}

public enum PromoteOption {
    Queen, Rook, Bishop, Knight
}

public enum PlayerStatus {
    Normal, Checked, Checkmate, Won, Stalemate, Draw, Resigned
}