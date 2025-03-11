public class Movement {
    public Player CurrentPlayer {get;}
    // public Piece PieceToMove {get; set;}
    public  Position CurrentPosition {get;}
    public Position NewPosition {get;}
    public Movement(Player player, Position currentPos, Position newPos) {
        CurrentPlayer = player;
        CurrentPosition = currentPos;
        NewPosition = newPos;
    }
}