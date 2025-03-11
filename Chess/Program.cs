Board board = new Board();
Position pos = new Position(3,2);
Display display = new Display();

display.AskNonNullInput("enter some string: ");
display.DisplayBoard(board,pos);