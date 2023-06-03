using Newtonsoft.Json;

namespace ToDoListApp {
  public class ToDoList : List<TDTask> {
    string savePath;
    public ToDoList(string saveFileName = "data.txt") {
      string workingDirectory = Environment.CurrentDirectory;
      string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
      savePath = Path.Combine(projectDirectory, saveFileName);
    }

    public void Save() => File.WriteAllText(savePath, string.Join("\n", this.Select(task => JsonConvert.SerializeObject(task))));
    public void Get() {
      Clear();
      string[] allData = File.ReadAllText(savePath).Split("\n");
      foreach (string text in allData) {
        TDTask tmpTask = JsonConvert.DeserializeObject<TDTask>(text);
        this.Add(tmpTask);
      }
    }
    public new bool Remove(TDTask item) {
      bool res = base.Remove(item);
      if (res)
        Console.WriteLine("Successfully removed task!");
      else
        Console.WriteLine("Task removing failed!");
      Console.ReadLine();
      Console.Clear();
      return res;
    }

    public void Edit(int pos) {
      string message =
        $"===Editing task===\n" +
        $"Hit Enter if you don't wan't to keep the field\n" +
        $"Hit Space and Enter if you don't wan't unset the field\n" +
        $"Type anything you want to change the field\n" +
        $"Notice: the date format is \"yyyy dd MMMM HH:mm\", so you should write i.e. 2022 22 April 12:46\n";
      Console.WriteLine(message);

      DateTime? nTmpDt = null;
      string? tmpCap = Ask("New caption:");
      string? tmpDesc = Ask("New description:");
      string? tmpDeadLine = Ask("New deadline:");

      if (tmpCap == " ")
          tmpCap = null;
      else if (tmpCap == "") 
        tmpCap = this[pos].Caption;
      if (tmpDesc == " ")
        tmpDesc = null;
      else if (tmpDesc == "") tmpDesc = this[pos].Description;
      if (tmpDeadLine == "")
        nTmpDt = DateTime.ParseExact(this[pos].DeadLine, this[pos].Pattern, this[pos].Culture);
      else {
        try {
          if (tmpDeadLine != " ")
            nTmpDt = DateTime.ParseExact(tmpDeadLine, this[pos].Pattern, this[pos].Culture);
          else
            nTmpDt = null;   
        }
        catch (FormatException e) {
          Console.WriteLine("Wrong format, changes not applied!");
        }
      }
      this[pos] = new(tmpCap, tmpDesc, nTmpDt);
      Console.WriteLine("Successfully edited task!");
      Console.ReadLine();
      Console.Clear();
    }

    string? Ask(string text) {
      Console.WriteLine(text);
      return Console.ReadLine();
    }
  }
}
