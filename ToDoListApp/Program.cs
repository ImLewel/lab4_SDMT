namespace ToDoListApp {

  public class Program {
    enum DoInNotItn {
      ShowAll,
      Add,
      Remove,
      Edit,
      SortDeadline,
      ShowExpired,
      SetDone
    }
    static void Main(string[] args) {
      if (args.Contains("-i")) {
        InteractiveMode();
      }
      else if (args.Contains("-ni")) {
        if (args.Contains("-showAll"))
          NotInteractiveMode(DoInNotItn.ShowAll);
        else if (args.Contains("-add")) {
          int argPos = Array.IndexOf(args, "-add");
          args = new string[] { args[argPos + 1], args[argPos + 2], args[argPos + 3] };
          NotInteractiveMode(DoInNotItn.Add);
        }
        else if (args.Contains("-rem")) {
          int argPos = Array.IndexOf(args, "-rem");
          NotInteractiveMode(DoInNotItn.Remove, args[argPos + 1]);
        }
        else if (args.Contains("-edit")) {
          int argPos = Array.IndexOf(args, "-edit");
          args = new string[] { args[argPos + 1], args[argPos + 2], args[argPos + 3], args[argPos + 4] };
          NotInteractiveMode(DoInNotItn.Edit);
        }
        else if (args.Contains("-sortDL"))
          NotInteractiveMode(DoInNotItn.SortDeadline);
        else if (args.Contains("-showExp"))
          NotInteractiveMode(DoInNotItn.ShowExpired);
        else if (args.Contains("-setDone")) {
          int argPos = Array.IndexOf(args, "-setDone");
          NotInteractiveMode(DoInNotItn.SetDone, args[argPos + 1]);
        }
      }
      else if (args.Contains("-help")) {
        string message = 
          $"===Help===\n" +
          $"-help : show this message\n" +
          $"-i : enter interactive mode\n" +
          $"-ni [flags(see below)]: enter not interactive mode\n" +
          $"<Not interactive mode flags>\n" +
          $"-showAll : show all tasks\n" +
          $"-add [Caption] [Description] [Date] : add task\n" +
            $"\t Notice: [Date] argument should be passed as \"YOUR DATE\"\n" +
            $"\t The date format is \"yyyy dd MMMM HH:mm\", so you should write i.e. 2022 22 April 12:46\n" +
            $"\t If the format is wrong, it will automatically set as \"No deadline\"\n" +
          $"-edit [Task number] [Caption] [Description [Date] : edit certain task\n" +
            $"\t Notice: [Date] argument should be passed as \"YOUR DATE\"\n" +
            $"\t The date format is \"yyyy dd MMMM HH:mm\", so you should write i.e. 2022 22 April 12:46\n" +
            $"\t If the format is wrong, it will automatically set as \"No deadline\"\n" +
            $"\t To keep the certain task`s field you can pass \"\", to unset this field pass \" \"\n" +
          $"-rem [Task number] : delete certain task\n" +
          $"-sortDL : show tasks with deadlines, sorted from near to far deadline\n" +
          $"-showExp : show tasks, which aren't done but deadline is expired";
        Console.WriteLine(message);
      }

      void NotInteractiveMode(DoInNotItn state, string posToParse = "") {
        ToDoList list = new();
        list.Get();
        int tmp;
        bool done = false;
        switch (state) {
          case DoInNotItn.ShowAll:
            ShowAll();
            break;
          case DoInNotItn.Add:
            if (args.Length == 3) {
              Add();
              done = true;
            }
            else
              Console.WriteLine("Too few arguments!");
            break;
          case DoInNotItn.Remove:
            if (int.TryParse(posToParse, out tmp)) {
              if (tmp >= 0 && tmp < list.Count) {
                list.Remove(tmp);
                done = true;
                Console.WriteLine("Task deleted successfully!");
              }
              else
                Console.WriteLine("Position is outside the list!");
            }
            else
              Console.WriteLine("Given parameter is not an integer!");
            break;
          case DoInNotItn.Edit:
            if (args.Length == 4) {
              if (int.TryParse(args[0], out tmp)) {
                if (tmp >= 0 && tmp < list.Count) {
                  Edit(tmp);
                  done = true;
                }
              }
            }
            else
              Console.WriteLine("Too few arguments!");
            break;
          case DoInNotItn.SortDeadline:
            SortByDeadline();
            break;
          case DoInNotItn.ShowExpired:
            ShowExpired();
            break;
          case DoInNotItn.SetDone:
            if (int.TryParse(posToParse, out tmp)) {
              if (tmp >= 0 && tmp < list.Count) {
                list[tmp].SetDone();
                done = true;
                Console.WriteLine($"Task <{tmp}> is set to done successfully!");
              }
              else
                Console.WriteLine("Position is outside the list!");
            }
            else
              Console.WriteLine("Given parameter is not an integer!");
            break;
        }
        if (done)
          list.Save();
        return;

        void ShowAll() {
          int pos = 0;
          foreach (TDTask task in list) {
            Console.WriteLine($"{pos}. " + task.ToString());
            ++pos;
          }
        }
        void Add() {
          if (list.Add(args[0], args[1], args[2]))
            Console.WriteLine("Task added successfully!");
          else
            Console.WriteLine("Wrong DateTime format, add not done!");
        }
        void Edit(int pos) {
          if (list.Edit(pos, args[1], args[2], args[3]))
            Console.WriteLine("Task edited successfully!");
          else
            Console.WriteLine("Wrong DateTime format, edit not done!");
        }
        void SortByDeadline() {
          var selectedWithDeadlines = list.Where(task => task.deadLineStr != "No deadline" && task.Done != "Done").ToList();
          selectedWithDeadlines.Sort((first, second) => ((DateTime)first.DeadLine).CompareTo((DateTime)second.DeadLine));
          foreach (TDTask task in selectedWithDeadlines)
            Console.WriteLine(task.ToString());
        }
        void ShowExpired() {
          var selectedExpired = list.Where(task => task.DeadLine < DateTime.Now && task.Done != "Done").ToList();
          foreach (TDTask task in selectedExpired)
            Console.WriteLine(task.ToString());
        }
      }

      void InteractiveMode() {
        Menu MainMenu = new("<Main menu>");
        Menu TaskList = new("<Your tasks>");
        Menu TaskOptions = new("<What to do with task?>");
        Menu FilterMenu = new("<Filter tasks by>");
        Menu DeadlineList = new("<Tasks filtered from near to far deadline>");
        Menu ExpiredList = new("<Tasks which aren't done but expired>");
        ToDoList list = new();
        list.Get();
        MainMenu.Add(new MenuOption("Exit", () => Environment.Exit(0)));
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

        FilterMenu.Add(new("Back to main", () => MainMenu.Render()));
        FilterMenu.Add(new("From near to far deadline", () => {
            RefillDeadlineList();
            DeadlineList.Render();
          })
        );
        FilterMenu.Add(new("Expired", () => {
            RefillExpiredList();
            ExpiredList.Render();
          })
        );

        MainMenu.Render();

        void RefillTasksMenu() {
          TaskList.Clear();
          list.Save();
          TaskList.Add(new MenuOption("Back to main", () => MainMenu.Render()));
          foreach (TDTask task in list)
            TaskList.Add(new(task.ToString(), () => TaskOptions.Render()));
        }

        void RefillDeadlineList() {
          DeadlineList.Clear();
          DeadlineList.Add(new MenuOption("Back to filters", () => FilterMenu.Render()));
          var selectedWithDeadlines = list.Where(task => task.deadLineStr != "No deadline" && task.Done != "Done").ToList();
          selectedWithDeadlines.Sort((first, second) => ((DateTime)first.DeadLine).CompareTo((DateTime)second.DeadLine));
          foreach (TDTask task in selectedWithDeadlines)
            DeadlineList.Add(new(task.ToString(), () => DeadlineList.Render()));
        }

        void RefillExpiredList() {
          ExpiredList.Clear();
          ExpiredList.Add(new MenuOption("Back to filters", () => FilterMenu.Render()));
          var selectedExpired = list.Where(task => task.DeadLine < DateTime.Now && task.Done != "Done").ToList();
          foreach (TDTask task in selectedExpired)
            ExpiredList.Add(new(task.ToString(), () => DeadlineList.Render()));
        }
      }
    }
  }
}