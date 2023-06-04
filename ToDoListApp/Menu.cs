namespace ToDoListApp {
#nullable disable warnings
  public class Menu : List<MenuOption> {
    public int selection;
    string caption;
    public Menu(string _caption) {
      caption = _caption;
    }
    public void Render() {
      int pos = 0;
      Console.WriteLine(this.caption);
      foreach (MenuOption option in this) {
        Console.WriteLine($"{pos}. {option.label}");
        ++pos;
      }
      OptionSelection();
    }
    int GetAnswer() {
      Console.WriteLine("Choose an option:");
      string answer = Console.ReadLine();
      try {
        selection = int.Parse(answer);
      }
      catch {
        Console.WriteLine("No number provided, try again");
        return GetAnswer();
      }
      if (selection >= this.Count || selection < 0) {
        Console.WriteLine("Answer is bigger or less than any option in the list, try again");
        return GetAnswer();
      }
      else
        return selection;
    }
    void OptionSelection() {
      selection = GetAnswer();
      ClearText();
      this[selection].action();
    }
    void ClearText() => Console.Clear();
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
