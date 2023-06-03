﻿using Newtonsoft.Json;

namespace ToDoListApp {
  public class ToDoList : List<TDTask> {
    string savePath;
    public ToDoList(string saveFileName = "data.txt") {
      string workingDirectory = Environment.CurrentDirectory;
      string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
      savePath = Path.Combine(projectDirectory, saveFileName);
    }

    public void Save() => File.WriteAllText(savePath, string.Join("\n", this.Select(task => JsonConvert.SerializeObject(task))));
    public void Get() {
      Clear();
      string[] allData = File.ReadAllText(savePath).Split("\n");
      foreach (string text in allData) {
        TDTask tmpTask = JsonConvert.DeserializeObject<TDTask>(text);
        this.Add(tmpTask);
      }
    }
    public new bool Remove(TDTask item) {
      bool res = base.Remove(item);
      if (res)
        Console.WriteLine("Successfully removed task!");
      else
        Console.WriteLine("Task removing failed!");
      Console.ReadLine();
      Console.Clear();
      return res;
    }

    public void Edit(int pos) {
      string message =
        $"===Editing task===\n" +
        $"Hit Enter if you want to keep the field\n" +
        $"Hit Space and Enter if you wan't unset the field\n" +
        $"Type anything you want to change the field\n" +
        $"Notice: the date format is \"yyyy dd MMMM HH:mm\", so you should write i.e. 2022 22 April 12:46\n" +
        $"Notice: if the format is wrong, it will automatically set as \"No deadline\"\n";
      Console.WriteLine(message);

      DateTime? nTmpDt = null;
      string? tmpCap = Ask("New caption:");
      string? tmpDesc = Ask("New description:");
      string? tmpDeadLine = Ask("New deadline:");

      if (tmpCap == " ")
        tmpCap = null;
      else if (tmpCap == "") 
        tmpCap = this[pos].Caption;
      if (tmpDesc == " ")
        tmpDesc = null;
      else if (tmpDesc == "") tmpDesc = this[pos].Description;
      if (tmpDeadLine == "")
        nTmpDt = this[pos].DeadLine;
      else {
        try {
          if (tmpDeadLine != " ")
            nTmpDt = DateTime.Parse(tmpDeadLine);
          else
            nTmpDt = null;   
        }
        catch (FormatException e) {
          Console.WriteLine("Wrong format, changes not applied!");
        }
      }
      this[pos].Set(tmpCap, tmpDesc, nTmpDt);
      Console.WriteLine("Successfully edited the task!");
      Console.ReadLine();
      Console.Clear();
    }

    public void Add() {
      string message =
        $"===Adding task===\n" +
        $"Type anything you want to set the field\n" +
        $"Hit Space and Enter if you want to set the field as default\n" +
        $"Notice: the date format is \"yyyy dd MMMM HH:mm\", so you should write i.e. 2022 22 April 12:46\n" +
        $"Notice: if the format is wrong, it will automatically set as \"No deadline\"\n";
      Console.WriteLine(message);

      DateTime? nTmpDt = null;
      string? tmpCap = Ask("New caption:");
      string? tmpDesc = Ask("New description:");
      string? tmpDeadLine = Ask("New deadline:");

      if (tmpCap == " " || tmpCap == "")
        tmpCap = null;
      if (tmpDesc == " " || tmpDesc == "")
        tmpDesc = null;
      if (tmpDeadLine == " " || tmpDeadLine == "")
        nTmpDt = null;
      else {
        try {
          nTmpDt = DateTime.Parse(tmpDeadLine);
        }
        catch (FormatException e) {
          Console.WriteLine("Wrong format!");
        }
      }
      Add(new(tmpCap, tmpDesc, nTmpDt));
      Console.WriteLine("Successfully added the task!");
      Console.ReadLine();
      Console.Clear();
    }

    string? Ask(string text) {
      Console.WriteLine(text);
      return Console.ReadLine();
    }
  }
}
