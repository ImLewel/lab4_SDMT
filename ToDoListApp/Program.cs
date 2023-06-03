﻿namespace ToDoListApp {
  public class Program {
    static void Main(string[] args) {
      Menu MainMenu = new("<Main menu>");
      Menu TaskList = new("<Your tasks>");
      Menu TaskOptions = new("<What to do with task?>");
      ToDoList list = new();

      list.Add(new("Caption 1", "Description 1", DateTime.Now));
      list.Add(new("Caption 2", "Description 2", DateTime.Now));
      list.Add(new("Caption 3", "Description 3", DateTime.Now));

      MainMenu.Add(new MenuOption("Exit", () => Console.ReadKey()));
      MainMenu.Add(new MenuOption("Show tasks", () => TaskList.Render()));

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

      MainMenu.Render();

      void RefillTasksMenu() {
        TaskList.Clear();
        list.Save();
        TaskList.Add(new MenuOption("Back to second", () => MainMenu.Render()));
        foreach (TDTask task in list)
          TaskList.Add(new (task.ToString(), () => TaskOptions.Render()));
      }
    }
  }
}