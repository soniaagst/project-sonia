// Board board = new Board();
// Display display = new Display();

// //white
// Position pawnOldpos = new Position(6,1);
// Position pawnNewpos = new Position(4,1);

// Piece? pawnPiece = board.GetPieceAt(pawnOldpos);

// Position? whiteOrigin = new();
// board.MovePiece(pawnOldpos, pawnNewpos, out whiteOrigin);

// display.DisplayBoard(board, whiteOrigin);

// //black
// Position blackPawnOldpos = new Position(1,2);
// Position blackPawnNewpos = new Position(3,2);

// Piece? blackPawnPiece = board.GetPieceAt(blackPawnOldpos);

// Position? blackOrigin = new();
// board.MovePiece(blackPawnOldpos, blackPawnNewpos, out blackOrigin);

// display.DisplayBoard(board, blackOrigin);

// // white
// pawnOldpos = pawnPiece!.CurrentPosition;
// pawnNewpos = new Position(3,2);

// board.MovePiece(pawnOldpos, pawnNewpos, out whiteOrigin);

// display.DisplayBoard(board, whiteOrigin);

// test ParseInput
(Position currentpos, Position newpos) = Utility.ParseInput("enter move ");
Console.WriteLine($"{currentpos.Row}, {currentpos.Col} -> {newpos.Row}, {newpos.Col}");