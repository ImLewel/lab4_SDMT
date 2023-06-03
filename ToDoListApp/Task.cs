using Newtonsoft.Json;
using System.Globalization;

namespace ToDoListApp {
  public class TDTask {
    [JsonRequired]
    public string Caption;
    public string Description;
    public string DeadLine;
    bool Done = false;
    [JsonIgnore]
    public readonly CultureInfo Culture = new CultureInfo("en-EN");
    [JsonIgnore]
    public readonly string Pattern = "yyyy dd MMMM HH:mm";
    [JsonConstructor]
    public TDTask(string? capt, string? desc, DateTime? deadL) {
      Caption = ((capt is null) || (capt == " ")) ? "No caption" : capt;
      Description = ((desc is null) || (desc == " ")) ? "No description" : desc;
      DeadLine = (deadL is null) ? "No deadline" : ((DateTime)deadL).ToString(Pattern, Culture);
    }

    public void SetDone() => Done = true;
    public override string ToString() {
      return $"Caption: {Caption}; Description: {Description}; Deadline: {DeadLine}";
    }
  }
}