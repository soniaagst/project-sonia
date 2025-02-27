TodoList todo = new TodoList("Homeworks");
todo.AddItem("Stochastics");
todo.AddItem("Cryptography");
todo.AddItem("Linear Algebra");
todo.CheckItem(2);
todo.CheckItem(3);
todo.UncheckItem(2);
Console.WriteLine("Showing To Do List");
todo.ShowItems();
todo.EditItem(2);
todo.DeleteItem(1);
Console.WriteLine("Showing To Do List");
todo.ShowItems();













// notes:
// bikin json untuk simpan todo list
// bikin option interface