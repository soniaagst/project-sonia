Display display = new();

string whiteName = display.AskNonNullInput("Enter White player name: ");
string blackName = display.AskNonNullInput("Enter Black player name: ");

Player whitePlayer = new(whiteName, Colors.White);
Player blackPlayer = new(blackName, Colors.Black);

GameController game = new(display, whitePlayer, blackPlayer);
game.Play();