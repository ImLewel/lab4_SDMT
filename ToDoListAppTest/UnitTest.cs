using ToDoListApp;

namespace ToDoListAppTest {
  [TestClass]
  public class UnitTest {
    [TestMethod]
    public void TaskCreationTest() {
      TDTask task = new("Caption1", "Description1", DateTime.Now);
      Assert.AreEqual(task.Caption, "Caption1");
      task = new("Caption2", null, DateTime.Parse("05.07.2023"));
      Assert.AreEqual(task.Description, "No description");
      task = new("Caption3", "Description3", null);
      ;
      Assert.AreEqual(task.DeadLine, "No deadline");
    }
    [TestMethod]
    public void ListCreationTest() {
      ToDoList list = new();
      list.Add(new ("Do 1", null, DateTime.Parse("2023 31 May 00:02")));
      string capComp = "Do 1";
      string descComp = "No description";
      string deadLComp = "2023 31 May 00:02";
      Assert.AreEqual(list[0].Caption, capComp);
      Assert.AreEqual(list[0].Description, descComp);
      Assert.AreEqual(list[0].DeadLine, deadLComp);
      list.Save();
      list.Get();
      Assert.AreEqual(list[0].Caption, capComp);
      Assert.AreEqual(list[0].Description, descComp);
      Assert.AreEqual(list[0].DeadLine, deadLComp);
    }
  }
}