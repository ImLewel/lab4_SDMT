using ToDoListApp;

namespace ToDoListAppTest {
  [TestClass]
  public class UnitTest {
    [TestMethod]
    public void TaskCreationTest() {
      TDTask task = new("Caption1", "Description1", DateTime.Now);
      Assert.AreEqual(task.caption, "Caption1");
      task = new("Caption2", DateTime.Parse("05.07.2023"));
      Assert.AreEqual(task.description, "No description"); 
      task = new("Caption3", "Description3");
      Assert.AreEqual(task.deadLine, "No deadline");
    }
    [TestMethod]
    public void ListCreationTest() {
      ToDoList list = new();
      list.Add(new ("Do 1", DateTime.Parse("2023 31 травня 00:02")));
      string capComp = "Do 1";
      string descComp = "No description";
      string deadLComp = "2023 31 травня 00:02";
      Assert.AreEqual(list[0].caption, capComp);
      Assert.AreEqual(list[0].description, descComp);
      Assert.AreEqual(list[0].deadLine, deadLComp);
      list.Save();
      list.Get();
      Assert.AreEqual(list[0].caption, capComp);
      Assert.AreEqual(list[0].description, descComp);
      Assert.AreEqual(list[0].deadLine, deadLComp);
    }
  }
}