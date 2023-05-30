using System.Reflection;

namespace ToDoListApp {
  public class Program {
    static void Main(string[] args) {
      ToDoList list = new();
      list.Add(new TDTask("Do 1", DateTime.Now));
      list.Add(new TDTask("Do 2", "Desc smth 2", DateTime.Now));
      list.Add(new TDTask("Do 3", "Desc smth 3"));
      list.Save();
      list.Get();
      foreach (TDTask task in list) {
        Console.WriteLine($"Caption: {task.caption}, Description: {task.description}, Dead line: {task.deadLine}");
      }
    }
  }
}