namespace YWSDotNetCore.RestApi.DataModels
{
    public class TaskCategoryDataModel
    {

    public int CategoryID { get; set; }
    public string? CategoryName { get; set; }

        // Navigation property (Optional)
     public ICollection<TodoListDataMoedl> ToDoLists { get; set; } = new List<TodoListDataMoedl>();


    }
}
