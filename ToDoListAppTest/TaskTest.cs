using ToDoListApp;

namespace ToDoListAppTest {
  [TestClass]
  public class TaskTest {
    [TestMethod]
    public void TaskCreationTest() {
      TDTask task = new("Caption1", "Description1", DateTime.Now);
      Assert.AreEqual(task.Caption, "Caption1");

      task = new("Caption2", null, DateTime.Parse("05.07.2023"));
      Assert.AreEqual(task.Description, "No description");

      task = new("Caption3", "Description3", null);
      Assert.AreEqual(task.DeadLine, "No deadline");
    }
  }
}