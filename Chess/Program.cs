using Chess.Controller;
using Chess.Models;
using Chess.Enums;

Display display = new();

string whiteName = display.AskNonNullInput("Enter White player name: ");
string blackName = display.AskNonNullInput("Enter Black player name: ");

Player whitePlayer = new(whiteName, PieceColor.White);
Player blackPlayer = new(blackName, PieceColor.Black);

GameController game = new(display, whitePlayer, blackPlayer);
game.Play();