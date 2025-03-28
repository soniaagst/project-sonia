using Chess.Models.Pieces;
using Chess.Enums;
using Chess.Interfaces;

namespace Chess.Models;
public class Display : IDisplay
{
    public void DisplayBoard(Board board, Position? lastMoveOrigin)
    // is a rename from DisplayMove, it shows the last move
    {
        Console.Clear();
        Console.WriteLine("   A  B  C  D  E  F  G  H   ");
        for (int row = 0; row < 8; row++)
        {
            Console.Write($"{8 - row} ");
            for (int col = 0; col < 8; col++)
            {
                if ((row + col)%2 == 0) Console.BackgroundColor = ConsoleColor.Yellow;
                else Console.BackgroundColor = ConsoleColor.Cyan;

                if (lastMoveOrigin?.Row == row && lastMoveOrigin?.Col == col)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.Write("   ");
                }
                else if (board.GetBoard()[row, col] == null)
                {
                    Console.Write("   ");
                }
                else
                {
                    Piece piece = board.GetBoard()[row, col]!;
                    if (piece!.Color is PieceColor.White)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        if (piece is Pawn) Console.Write(" ♙ ");
                        if (piece is Knight) Console.Write(" ♘ ");
                        if (piece is Bishop) Console.Write(" ♗ ");
                        if (piece is Rook) Console.Write(" ♖ ");
                        if (piece is Queen) Console.Write(" ♕ ");
                        if (piece is King) Console.Write(" ♔ ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        if (piece is Pawn) Console.Write(" ♟ ");
                        if (piece is Knight) Console.Write(" ♞ ");
                        if (piece is Bishop) Console.Write(" ♝ ");
                        if (piece is Rook) Console.Write(" ♜ ");
                        if (piece is Queen) Console.Write(" ♛ ");
                        if (piece is King) Console.Write(" ♚ ");
                    }
                }
                Console.ResetColor();
            }
            Console.WriteLine($" {8 - row}");
        }
        Console.WriteLine("   A  B  C  D  E  F  G  H   ");
    }

    public void DisplayHistory(List<HistoryUnit> movesHistory)
    {
        foreach (var item in movesHistory)
        {
            string pieceChar = item.Piece!.ToString();
            string dest = item.EndingPosition.ToString();
            string kill = item.IsKill ? "x" : "";
            string check = item.IsCheck ? "+" : "";
            string promotion = item.IsPromotion ? "=" : "";
            string promotedTo = item.IsPromotion ? item.PromotedPiece!.ToString() : "";
            string shortCastle = item.IsShortCastle ? "O-O" : "";
            string longCastle = item.IsLongCastle ? "O-O-O" : "";

            if (item.IsShortCastle || item.IsLongCastle)
            {
                Console.Write($"{shortCastle}{longCastle}{check} ");
            }
            else Console.Write($"{pieceChar}{kill}{dest}{promotion}{promotedTo}{check} ");

            if (movesHistory.IndexOf(item) == 1)
            {
                Console.WriteLine();
            }
        }
    }

    public void DisplayMessage(string message)
    {
        Console.WriteLine(message);
    }

    public PromoteOption AskPromotionChoice()
    {
        DisplayMessage("Pawn promoted! Options:\n (0)Queen\n (1)Rook\n (2)Bishop \n (3)Knight");
        string choicestr = AskNonNullInput("Choose your promotion (the default is 0)");
        int choice;
        int.TryParse(choicestr, out choice);
        return (PromoteOption)choice;
    }

    public string AskNonNullInput(string? message)
    {
        Console.Write(message);
        string? input = Console.ReadLine()?.ToUpper();
        while (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Input cannot empty. Try again.");
            Console.Write(message);
            input = Console.ReadLine()?.ToUpper();
        }
        return input;
    }

    public bool TryParseMove(string input, out Movement? movement)
    {
        if (input.Split(' ').Count() != 2)
        {
            movement = null;
            return false;
        }
        if (input.Split(' ')[0].ToCharArray().Count() != 2 || input.Split(' ')[1].ToCharArray().Count() != 2)
        {
            movement = null;
            return false;
        }
        Position[] move = [new(), new()];
        string[] pos = input.Split(' ');
        for (int i = 0; i < 2; i++)
        {
            char[] rowcol = pos[i].ToCharArray();
            (rowcol[0], rowcol[1]) = (rowcol[1], rowcol[0]);
            int row = 56 - rowcol[0];
            int col = rowcol[1] - 'A';
            move[i].Row = row; move[i].Col = col;
        }
        if (!Board.IsInsideBoard(move[0]) || !Board.IsInsideBoard(move[1]))
        {
            movement = null;
            return false;
        }
        movement = new Movement(move[0], move[1]);
        return true;
    }

    public void DisplayStartingScreen(out string whiteName, out string blackName) {
        Console.Clear();
        Console.WriteLine("=====================================");
        Console.WriteLine("        WELCOME TO CHESS GAME        ");
        Console.WriteLine("=====================================");
        Console.WriteLine(" Controls: ");
        Console.WriteLine(" - Enter moves like 'e2 e4' ");
        Console.WriteLine(" - Enter 'exit' to quit ");
        Console.WriteLine(" - Enter 'draw' to offer a draw ");
        Console.WriteLine("\nPress any key to start...");
        Console.ReadKey();
        Console.Clear();
        whiteName = AskNonNullInput("Enter White player name: ");
        blackName = AskNonNullInput("Enter Black player name: ");
    }

}