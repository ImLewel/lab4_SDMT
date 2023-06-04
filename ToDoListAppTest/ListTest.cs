using ToDoListApp;

namespace ToDoListAppTest {
#nullable disable warnings
  [TestClass]
  public class ListTest {
    [TestMethod]
    public void ListCreationTest() {
      ToDoList list = new();
      string expectedCaption = "Caption 1";
      string expectedDescription = "No description";
      string expectedDeadLine = "2023 31 May 00:02";

      list.Add(new ("Caption 1", null, DateTime.Parse("2023 31 May 00:02")));
      Assert.AreEqual(expectedCaption, list[0].Caption);
      Assert.AreEqual(expectedDescription, list[0].Description);
      Assert.AreEqual(expectedDeadLine, list[0].deadLineStr);

      list.Add(null, "Description 2", "2005 22 December 02:02");
      expectedCaption = "No caption";
      expectedDescription = "Description 2";
      expectedDeadLine = "2005 22 December 02:02";
      Assert.AreEqual(expectedCaption, list[1].Caption);
      Assert.AreEqual(expectedDescription, list[1].Description);
      Assert.AreEqual(expectedDeadLine, list[1].deadLineStr);

    }
    [TestMethod]
    public void ListGetSaveTest() {
      ToDoList list = new();
      ToDoList list2 = new();

      list.Add(new("Caption 1", null, DateTime.Parse("2023 31 May 00:02")));
      list.Add(null, "Description 2", "2005 22 December 02:02");
      list.Add("Caption 3", "Description 3", (string?)null);

      list2.Add(new("Caption 1", null, DateTime.Parse("2023 31 May 00:02")));
      list2.Add(null, "Description 2", "2005 22 December 02:02");
      list2.Add("Caption 3", "Description 3", (string?)null);

      list.Save();
      list.Get();
      for (int i = 0; i < list.Count; ++i)
        Assert.AreEqual(list[i].ToString(), list2[i].ToString());
      list2[1].Caption = "Caption 2";
      Assert.AreNotEqual(list[1].Caption, list2[1].Caption);
    }
    [TestMethod]
    public void EditTest() {
      ToDoList list = new();
      string before;

      list.Add(null, "Description 1", "2005 22 December 02:02");
      before = list[0].ToString();
      Assert.AreEqual("No caption", list[0].Caption);

      list.Edit(0, "Caption 1", "", "");
      Assert.AreNotEqual(before, list[0].ToString());
      Assert.AreEqual("Caption 1", list[0].Caption);
    }
  }
}