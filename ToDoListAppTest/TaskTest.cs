using Newtonsoft.Json;
using ToDoListApp;

namespace ToDoListAppTest {
#nullable disable warnings
  [TestClass]
  public class TaskTest {
    [TestMethod]
    public void TaskCreationTest() {
      TDTask task = new("Caption1", "Description1", DateTime.Now);
      Assert.AreEqual(task.Caption, "Caption1");

      task = new("Caption2", null, DateTime.Parse("05.07.2023"));
      Assert.AreEqual(task.Description, "No description");

      task = new("Caption3", "Description3", null);
      Assert.AreEqual(task.deadLineStr, "No deadline");
    }
    [TestMethod]
    public void TaskDeserializeTest() {
      TDTask task = new("Caption 1", "No description", DateTime.Parse("2023 May 31 00:02"));
      TDTask task2;
      string expectedSerialization = "{\"Caption\":\"Caption 1\",\"Description\":\"No description\",\"DeadLine\":\"2023-05-31T00:02:00\",\"Done\":\"Not done yet\",\"CompletionDate\":\"0001-01-01T00:00:00\"}";
      string serialized = JsonConvert.SerializeObject(task);
      Assert.AreEqual(expectedSerialization, serialized);
      task = JsonConvert.DeserializeObject<TDTask>(serialized);
      task2 = JsonConvert.DeserializeObject<TDTask>(expectedSerialization);
      Assert.AreEqual(task.ToString(), task2.ToString());
    }
  }
}