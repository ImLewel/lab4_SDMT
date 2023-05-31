using Newtonsoft.Json;
using System.Globalization;

namespace ToDoListApp {
  public class TDTask {
    public string Caption;
    public string Description;
    public string DeadLine;
    bool Done = false;
    private readonly CultureInfo Culture = new CultureInfo("en-EN");
    private readonly string Pattern = "yyyy dd MMMM HH:mm";
    [JsonConstructor]
    public TDTask(string? capt, string? desc, DateTime? deadL) {
      Caption = ((capt is null) || (capt == " ")) ? "No caption" : capt;
      Description = ((desc is null) || (desc == " ")) ? "No description" : desc;
      DeadLine = (deadL is null) ? "No deadline" : ((DateTime)deadL).ToString(Pattern, Culture);
    }
/*    public TDTask(string capt, string desc) {
      Caption = capt;
      Description = desc;
      DeadLine = "No deadline";
    }
    public TDTask(string capt, DateTime deadL) {
      Caption = capt;
      Description = "No description";
      DeadLine = deadL.ToString(pattern, culture);
    }*/

    public void SetDone() => Done = true;
    public override string ToString() {
      return $"Caption: {Caption}; Description: {Description}; Deadline: {DeadLine}";
    }
  }
}