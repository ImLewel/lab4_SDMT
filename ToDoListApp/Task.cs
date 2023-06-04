using Newtonsoft.Json;
using System.Globalization;
using System.Runtime.Serialization;

namespace ToDoListApp {
#pragma warning disable
  public class TDTask {
    [JsonProperty(Required = Required.AllowNull)]
    public string? Caption;
    [JsonProperty(Required = Required.AllowNull)]
    public string? Description;
    [JsonProperty(Required = Required.AllowNull)]
    public DateTime? DeadLine;
    [JsonRequired]
    public string Done = "Not done yet";
    [JsonProperty(Required = Required.AllowNull)]
    public DateTime CompletionDate;
    [JsonIgnore]
    public string deadLineStr;
    [JsonIgnore]
    public readonly static CultureInfo Culture = new CultureInfo("en-EN");
    [JsonIgnore]
    public readonly static string Pattern = "yyyy dd MMMM HH:mm";
    public TDTask(string? capt, string? desc, DateTime? deadL) {
      Set(capt, desc, deadL);
    }
    public void Set(string? capt, string? desc, DateTime? deadL) {
      Caption = ((capt is null) || (capt == " ")) ? "No caption" : capt;
      Description = ((desc is null) || (desc == " ")) ? "No description" : desc;
      DeadLine = (deadL is null) ? null : deadL;
      SetDeadLineStr();
    }
    public void SetDone() {
      if (Done != "Done") {
        Done = "Done";
        CompletionDate = DateTime.Now;
      }
    }
    [OnDeserialized]
    void OnDeserializedMethod(StreamingContext context) => SetDeadLineStr();
    void SetDeadLineStr() =>
      deadLineStr = (DeadLine is null) ? "No deadline" : ((DateTime)DeadLine).ToString(Pattern, Culture);

    public override string ToString() {
      if (Done == "Done")
        return 
          $"\n\tCaption: {Caption}\n" +
          $"\tDescription: {Description}\n" +
          $"\tDeadline: {deadLineStr}\n" +
          $"\tDone: {Done}\n" +
          $"\tCompletion date: {CompletionDate.ToString(Pattern, Culture)}";
      else
        return 
          $"\n\tCaption: {Caption}\n" +
          $"\tDescription: {Description}\n" +
          $"\tDeadline: {deadLineStr}\n" +
          $"\tDone: {Done}";
    }
  }
}