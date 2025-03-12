Board board = new Board();
Display display = new Display();
// Utility.AskNonNullInput("enter some string: ");

//white
Position pawnOldpos = new Position(6,1);
Position pawnNewpos = new Position(4,1);

Piece? pawnPiece = board.GetPieceAt(pawnOldpos);

Position? whiteOrigin = new();
board.MovePiece(pawnPiece!, pawnOldpos, pawnNewpos, out whiteOrigin);

display.DisplayBoard(board, whiteOrigin);

//black
Position blackPawnOldpos = new Position(1,2);
Position blackPawnNewpos = new Position(3,2);

Piece? blackPawnPiece = board.GetPieceAt(blackPawnOldpos);

Position? blackOrigin = new();
board.MovePiece(blackPawnPiece!, blackPawnOldpos, blackPawnNewpos, out blackOrigin);

display.DisplayBoard(board, blackOrigin);

// white
pawnOldpos = pawnPiece!.CurrentPosition;
pawnNewpos = new Position(3,2);

board.MovePiece(pawnPiece!, pawnOldpos, pawnNewpos, out whiteOrigin);

display.DisplayBoard(board, whiteOrigin);