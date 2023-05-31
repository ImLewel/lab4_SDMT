using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ToDoListApp {
  public class ToDoList : List<TDTask> {
    string savePath = "../../../data.txt";
    public void Save() {
      File.WriteAllText(savePath, String.Empty);
      string data = String.Empty;
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
      string tmpCap = Ask("New caption: (Press Enter to skip)", ref cursorUp);
      string tmpDesc = Ask("New description: (Press Enter to skip)", ref cursorUp);
      string tmpDeadLine = Ask("New deadline (format example 2022 22 April 12:46): (Press Enter to skip)", ref cursorUp);
      if (tmpCap != "")
        this[pos].caption = tmpCap;
      if (tmpDesc != "")
        this[pos].description = tmpDesc;
      if (tmpDeadLine != "")
        this[pos].deadLine = tmpDeadLine;
      return cursorUp;
    }

    string Ask(string text, ref int carriageUp) {
      Console.WriteLine(text);
      carriageUp += 2;
      return Console.ReadLine();
    }
  }
}
