using Newtonsoft.Json;
using System.Globalization;

namespace ToDoListApp {
  public class TDTask {
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string Caption;
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string Description;
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public DateTime? DeadLine;
    public string? deadLineStr;
    [JsonRequired]
    public string Done = "Not done yet";
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public DateTime CompletionDate;
    [JsonIgnore]
    public readonly static CultureInfo Culture = new CultureInfo("en-EN");
    [JsonIgnore]
    public readonly static string Pattern = "yyyy dd MMMM HH:mm";
    [JsonConstructor]
    public TDTask(string? capt, string? desc, DateTime? deadL) {
      Set(capt, desc, deadL);
    }
    public void Set(string? capt, string? desc, DateTime? deadL) {
      Caption = ((capt is null) || (capt == " ")) ? "No caption" : capt;
      Description = ((desc is null) || (desc == " ")) ? "No description" : desc;
      DeadLine = deadL;
      deadLineStr = (deadL is null) ? "No deadline" : ((DateTime)DeadLine).ToString(Pattern, Culture);
    }
    public void SetDone() {
      Done = "Done";
      CompletionDate = DateTime.Now;
    }
    public override string ToString() {
      if (Done == "Done")
        return $"Caption: {Caption}; Description: {Description}; Deadline: {deadLineStr}; Done: {Done}; Completion date: {CompletionDate.ToString(Pattern, Culture)}";
      else
        return $"Caption: {Caption}; Description: {Description}; Deadline: {deadLineStr}; Done: {Done}";
    }
  }
}