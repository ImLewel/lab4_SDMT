using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListApp {
  public class TDTask {
    public string caption;
    public string description;
    public DateTime deadLine;
    bool done = false;
    public TDTask(string capt, string desc, DateTime deadL) {
      caption = capt;
      description = desc;
      deadLine = deadL;
    }
    public TDTask(string capt, string desc) {
      caption = capt;
      description = desc;
      deadLine = DateTime.MaxValue;
    }
    public TDTask(string capt, DateTime deadL) {
      caption = capt;
      description = "";
      deadLine = deadL;
    }
    public void SetDone() => done = true;
  }
}