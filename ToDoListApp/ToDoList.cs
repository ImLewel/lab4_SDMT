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
  }
}
