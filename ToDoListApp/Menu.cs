using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListApp {
  public class Menu : List<MenuOption> {
    public int selection;
    public int clearCount;
    string caption;
    public Menu(string _caption) {
      caption = _caption;
    }
    public void Render() {
      int pos = 0;
      clearCount = pos;
      Console.WriteLine(this.caption);
      ++clearCount;
      foreach (MenuOption option in this) {
        Console.WriteLine($"{pos}. {option.label}");
        ++pos;
        ++clearCount;
      }
      OptionSelection();
    }
    int GetAnswer() {
      Console.WriteLine("Choose an option:");
      ++clearCount;
      string answer = Console.ReadLine();
      ++clearCount;
      try {
        selection = int.Parse(answer);
      }
      catch (Exception e) {
        Console.WriteLine("No number provided, try again");
        ++clearCount;
        return GetAnswer();
      }
      if (selection >= this.Count || selection < 0) {
        Console.WriteLine("Answer is bigger or less than any option in the list, try again");
        ++clearCount;
        return GetAnswer();
      }
      else
        return selection;
    }
    void OptionSelection() {
      selection = GetAnswer();
      Console.CursorTop -= clearCount;
      for (int _pos = 0; _pos < clearCount; ++_pos)
        Console.WriteLine(new string(' ', Console.WindowWidth));
      Console.CursorTop -= clearCount;
      this[selection].action();
    }
  }
  public class MenuOption {
    public string label;
    public Action action;
    public MenuOption(string _label, Action _action) {
      label = _label;
      action = _action;
    }
  }
}
