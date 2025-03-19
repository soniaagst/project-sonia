using Chess.Models;
using Chess.Enums;

namespace Chess.Interfaces;
public interface IDisplay
{
    public void DisplayBoard(Board board, Position? lastMoveOrigin);
    public void DisplayHistory(List<HistoryUnit> movesHistory);
    public void DisplayMessage(string message);
    public PromoteOption AskPromotionChoice();
    public string AskNonNullInput(string? message);
    public bool TryParseMove(string input, out Movement? movement);
}

// the class that needs interface the most is Display, hence IDisplay.