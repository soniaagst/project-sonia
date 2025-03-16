public struct Movement {
    public  Position From {get;}
    public Position To {get;}
    public Movement(Position from, Position to) {
        From = from;
        To = to;
    }
}