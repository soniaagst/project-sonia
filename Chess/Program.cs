Display display = new Display();
string whiteName = display.AskNonNullInput("Enter White player name: ");
string blackName = display.AskNonNullInput("Enter Black player name: ");
Player whitePlayer = new Player(whiteName, Colors.White);
Player blackPlayer = new Player(blackName, Colors.Black);
GameController game = new(display, whitePlayer, blackPlayer);
game.Play();