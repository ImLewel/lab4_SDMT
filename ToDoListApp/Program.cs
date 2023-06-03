namespace ToDoListApp {
  public class Program {
    static void Main(string[] args) {
      Menu MainMenu = new("<Main menu>");
      Menu TaskList = new("<Your tasks>");
      Menu TaskOptions = new("<What to do with task?>");
      Menu FilterMenu = new("<Filter tasks by>");
      Menu DeadlineList = new("<Tasks filtered from near to far deadline>");
      ToDoList list = new();
      list.Add(new("Caption 1", "Description 1", DateTime.Parse("2024 18 June 17:50")));
      list.Add(new("Caption 2", "Description 2", DateTime.Now));
      list.Add(new("Caption 3", "Description 3", DateTime.Parse("2022 12 April 12:46")));
      MainMenu.Add(new MenuOption("Exit", () => Console.ReadKey()));
      MainMenu.Add(new MenuOption("Show tasks", () => TaskList.Render()));
      MainMenu.Add(new MenuOption("Add task", () => {
          list.Add();
          RefillTasksMenu();
          MainMenu.Render();
        })
      );
      MainMenu.Add(new MenuOption("Show filtered by", () => FilterMenu.Render()));

      RefillTasksMenu();

      TaskOptions.Add(new MenuOption("Back to list", () => TaskList.Render()));
      TaskOptions.Add(
        new MenuOption("Delete", () => {
          list.Remove(list[TaskList.selection - 1]);
          RefillTasksMenu();
          TaskList.Render();
        })
      );
      TaskOptions.Add(
        new MenuOption("Edit", () => {
          list.Edit(TaskList.selection - 1);
          RefillTasksMenu();
          TaskList.Render();
        })
      );
      TaskOptions.Add(
        new MenuOption("Mark as done", () => {
          list[TaskList.selection - 1].SetDone();
          RefillTasksMenu();
          TaskList.Render();
        })
      );

      FilterMenu.Add(new ("Back to main", () => MainMenu.Render()));
      FilterMenu.Add(new ("From near to far deadline", () => {
          RefillDeadlineList();
          DeadlineList.Render();
        })
      );

      MainMenu.Render();

      void RefillTasksMenu() {
        TaskList.Clear();
        list.Save();
        TaskList.Add(new MenuOption("Back to main", () => MainMenu.Render()));
        foreach (TDTask task in list)
          TaskList.Add(new (task.ToString(), () => TaskOptions.Render()));
      }

      void RefillDeadlineList() {
        DeadlineList.Clear();
        DeadlineList.Add(new MenuOption("Back to filters", () => FilterMenu.Render()));
        var selectedWithDeadlines = list.Where(task => task.deadLineStr != "No deadline").ToList();
        selectedWithDeadlines.Sort((first, second) => ((DateTime)first.DeadLine).CompareTo((DateTime)second.DeadLine));
        foreach (TDTask task in selectedWithDeadlines)
          DeadlineList.Add(new(task.ToString(), () => DeadlineList.Render()));
      }
    }
  }
}