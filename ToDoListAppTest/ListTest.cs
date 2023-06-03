using ToDoListApp;

namespace ToDoListAppTest {
  [TestClass]
  public class ListTest {
    [TestMethod]
    public void ListCreationTest() {
      ToDoList list = new();
      string expectedCaption = "Do 1";
      string expectedDescription = "No description";
      string expectedDeadLine = "2023 31 May 00:02";

      list.Add(new ("Do 1", null, DateTime.Parse("2023 31 May 00:02")));

      Assert.AreEqual(list[0].Caption, expectedCaption);
      Assert.AreEqual(list[0].Description, expectedDescription);
      Assert.AreEqual(list[0].DeadLine, expectedDeadLine);

      list.Save();
      list.Get();

      Assert.AreEqual(list[0].Caption, expectedCaption);
      Assert.AreEqual(list[0].Description, expectedDescription);
      Assert.AreEqual(list[0].DeadLine, expectedDeadLine);
    }
  }
}