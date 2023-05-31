using System.Reflection;

namespace ToDoListApp {
  public class Program {
    static void Main(string[] args) {
      ToDoList list = new();
      list.Add(new("Caption 1", "Description 1", DateTime.Now));
      list.Add(new("Caption 2", "Description 2", DateTime.Now));
      list.Add(new("Caption 3", "Description 3", DateTime.Now));
      Menu mainMenu = new("<Main menu>");
      Menu showTasksMenu = new("<Your tasks>");
      Menu taskMenu = new("<What to do with task?>");

      mainMenu.Add(new MenuOption("Exit", () => Console.ReadKey()));
      mainMenu.Add(new MenuOption("Show tasks", () => showTasksMenu.Render()));

      RefillTasksMenu();

      taskMenu.Add(new MenuOption("Back to list", () => showTasksMenu.Render()));
      taskMenu.Add(
        new MenuOption("Delete", () => {
          list.Remove(list[showTasksMenu.selection - 1]);
          RefillTasksMenu();
          showTasksMenu.Render();
        })
      );
      taskMenu.Add(
        new MenuOption("Edit", () => {
          Console.WriteLine(showTasksMenu.selection);
          Console.WriteLine("New caption: (enter if don't want to change)");
          string tmpCap = Console.ReadLine();
          taskMenu.clearCount += 2;
          Console.WriteLine("New description: (enter if don't want to change)");
          string tmpDesc = Console.ReadLine();
          taskMenu.clearCount += 2;
          Console.WriteLine("New deadline: (enter if don't want to change)");
          string tmpDeadLine = Console.ReadLine();
          taskMenu.clearCount += 2;
          if (tmpCap != "")
            list[showTasksMenu.selection - 1].caption = tmpCap;
          if (tmpDesc != "")
            list[showTasksMenu.selection - 1].description = tmpDesc;
          if (tmpDeadLine != "")
            list[showTasksMenu.selection - 1].deadLine = tmpDeadLine;
          RefillTasksMenu();
          showTasksMenu.Render();
        })
      );

      mainMenu.Render();

      void RefillTasksMenu() {
        showTasksMenu.Clear();
        showTasksMenu.Add(new MenuOption("Back to second", () => mainMenu.Render()));
        foreach (TDTask task in list)
          showTasksMenu.Add(new MenuOption($"{task.caption} {task.description} {task.deadLine}", () => taskMenu.Render()));
      }
    }
  }
}