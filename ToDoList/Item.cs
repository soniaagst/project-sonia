public class Item {
    // public int ID {get; set;} // perlu ID? perlu bikin fungsi untuk generate ID?
    public string Content {get; set;}
    public ItemStatus Status {get; set;}
    public Item(string content) {
        Content = content;
    }
}

public enum ItemStatus {
    Unchecked,
    Checked
}