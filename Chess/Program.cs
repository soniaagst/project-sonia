Display display = new Display();
string whiteName = display.AskNonNullInput("Enter White player name: ");
string blackName = display.AskNonNullInput("Enter Black player name: ");
GameController game = new(display, whiteName, blackName);
game.Play();