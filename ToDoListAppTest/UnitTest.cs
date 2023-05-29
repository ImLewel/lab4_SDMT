using ToDoListApp;

namespace ToDoListAppTest {
  [TestClass]
  public class UnitTest {
    [TestMethod]
    public void TaskCreationTest() {
      TDTask task = new("Caption1", "Description1", DateTime.Now);
      Assert.AreEqual(task.caption, "Caption1");
      task = new("Caption2", DateTime.Parse("05.07.2023"));
      Assert.AreEqual(task.description, ""); 
      task = new("Caption3", "Description3");
      Assert.AreEqual(task.deadLine, DateTime.MaxValue);
    }
  }
}