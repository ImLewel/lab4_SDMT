using Newtonsoft.Json;

namespace ToDoListApp {
  public class ToDoList : List<TDTask> {
    string workingDirectory = Environment.CurrentDirectory;
    string saveFileName = "data.txt";
    string savePath;
    public ToDoList() {
      string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
      savePath = Path.Combine(projectDirectory, saveFileName);
    }
    public void Save() {
      File.WriteAllText(savePath, String.Empty);
      string data;
      int pos = 0;
      foreach (TDTask task in this) {
        data = JsonConvert.SerializeObject(task);
        if (pos != this.Count - 1)
          File.AppendAllText(savePath, $"{data}\n");
        else
          File.AppendAllText(savePath, $"{data}");
        ++pos;
      }
    }
    public void Get() {
      Clear();
      string[] allData = File.ReadAllText(savePath).Split("\n");
      foreach (string text in allData) {
        TDTask tmpTask = JsonConvert.DeserializeObject<TDTask>(text);
        this.Add(tmpTask);
      }
    }
    public int Edit(int pos) {
      int cursorUp = 0;
      DateTime? nTmpDt;
      DateTime tmpDt;
      DateTime.TryParse(this[pos].DeadLine, out tmpDt);
      nTmpDt = tmpDt;
      string? tmpCap = Ask("New caption: (Press Enter to skip)", ref cursorUp);
      string? tmpDesc = Ask("New description: (Press Enter to skip)", ref cursorUp);
      string? tmpDeadLine = Ask("New deadline (format example 2022 22 April 12:46): (Press Enter to skip)", ref cursorUp);
      if (tmpCap == " ")
          tmpCap = null;
      else if (tmpCap == "") 
        tmpCap = this[pos].Caption;
      if (tmpDesc == " ")
        tmpDesc = null;
      else if (tmpDesc == "") tmpDesc = this[pos].Description;
      if (tmpDeadLine != "") {
        if (DateTime.TryParse(tmpDeadLine, out tmpDt) == false) {
          Ask("No date passed! Automatically set as \"No deadline\"", ref cursorUp);
          nTmpDt = null;
        }
        else
          nTmpDt = tmpDt;
      }
      this[pos] = new(tmpCap, tmpDesc, nTmpDt);
      return cursorUp;
    }

    string? Ask(string text, ref int carriageUp) {
      Console.WriteLine(text);
      carriageUp += 2;
      return Console.ReadLine();
    }
  }
}
