public class TodoList {
    public string Title {get; set;}
    public Dictionary<int, Item> List = [];
    
    public TodoList(string title) {
        Title = title;
    }
    int id = 1;
    public void AddItem(string item) {
        List.Add(id, new Item(item));
        id++;
    }
    public void ShowItems() {
        Console.WriteLine($"[{Title}]");
        foreach (var item in List) {
            if (item.Value.Status == ItemStatus.Checked) {
                Console.WriteLine($"\u001b[9m{item.Value.Content} \u001b[0m");
            }
            else Console.WriteLine(item.Value.Content);
        }
    }
    public void CheckItem(int id) {
        List[id].Status = ItemStatus.Checked;
    }
    public void UncheckItem(int id) {
        List[id].Status = ItemStatus.Unchecked;
    }
    public void EditItem(int id) {
        Console.Write($"Edit <{List[id].Content}> to ... ");
        string? editedItem = Console.ReadLine();
        while (string.IsNullOrEmpty(editedItem)) {
            Console.WriteLine("Cannot empty.");
            editedItem = Console.ReadLine();
        }
        List[id].Content = editedItem;
    }
    public void DeleteItem(int id) {
        List.Remove(id);
    }
}
