namespace YWSDotNetCore.RestApi.ViewModels
{
    public class ToDoListViewModel
    {
            public int TaskID { get; set; }
            public string? TaskTitle { get; set; }
            public string? TaskDescription { get; set; }
            public int CategoryID { get; set; }
            public string? CategoryName { get; set; }  // From TaskCategory
            public byte PriorityLevel { get; set; }
            public string? Status { get; set; }
            public DateTime DueDate { get; set; }
            public DateTime? CompletedDate { get; set; }       
    }
}
