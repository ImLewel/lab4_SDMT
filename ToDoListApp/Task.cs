using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListApp {
  public class TDTask {
    public string caption;
    public string description;
    public string deadLine;
    bool done = false;
    string pattern = "yyyy dd MMMM HH:mm";
    [JsonConstructor]
    public TDTask(string capt, string desc, DateTime deadL) {
      caption = capt;
      description = desc;
      deadLine = deadL.ToString(pattern);
    }
    public TDTask(string capt, string desc) {
      caption = capt;
      description = desc;
      deadLine = "No deadline";
    }
    public TDTask(string capt, DateTime deadL) {
      caption = capt;
      description = "No description";
      deadLine = deadL.ToString(pattern);
    }
    public void SetDone() => done = true;
  }
}