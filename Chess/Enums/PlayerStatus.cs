namespace Chess.Enums;
public enum PlayerStatus
{
    Normal,
    Checked,
    Checkmate,
    Won,
    Stalemate,
    Draw,
    Resigned
}

// GameStatus splitted into GameStatus and PlayerStatus,
// which serve different purposes,
// to make it easier for controller to control game flow.

// Since there's SetPlayerGameStatus in the controller, 
// it's obvious that player and game have their own statuses.