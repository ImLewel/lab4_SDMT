using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListApp {
  public class TDTask {
    public string caption;
    public string description;
    public string deadLine;
    bool done = false;
    CultureInfo culture = new CultureInfo("en-EN");
    string pattern = "yyyy dd MMMM HH:mm";
    [JsonConstructor]
    public TDTask(string capt, string desc, DateTime deadL) {
      caption = capt;
      description = desc;
      deadLine = deadL.ToString(pattern, culture);
    }
    public TDTask(string capt, string desc) {
      caption = capt;
      description = desc;
      deadLine = "No deadline";
    }
    public TDTask(string capt, DateTime deadL) {
      caption = capt;
      description = "No description";
      deadLine = deadL.ToString(pattern, culture);
    }
    public void SetDone() => done = true;
    public override string ToString() {
      return $"Caption: {caption}; Description: {description}; Deadline: {deadLine}";
    }
  }
}