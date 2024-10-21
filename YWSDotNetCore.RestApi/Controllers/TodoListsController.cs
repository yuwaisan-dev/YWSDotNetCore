using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using YWSDotNetCore.RestApi.DataModels;
using YWSDotNetCore.RestApi.ViewModels;

namespace YWSDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListsController : ControllerBase
    {
        public readonly string _connectionString = "Data Source=.;Initial Catalog=YWSDotNetCore;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";

        [HttpGet]
        public IActionResult GetAllTasks()
        {
            List<ToDoListViewModel> taskViewModels = new List<ToDoListViewModel>();
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"SELECT t.[TaskID]
      ,t.[TaskTitle]
      ,t.[TaskDescription]
      ,t.[CategoryID]
	  ,c.[CategoryName]
      ,t.[PriorityLevel]
      ,t.[Status]
      ,t.[DueDate]
      ,t.[CreatedDate]
      ,t.[CompletedDate]
  FROM [dbo].[ToDoList] t JOIN TaskCategory c ON c.CategoryID = t.CategoryID";
            
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                  var item =  new ToDoListViewModel
                    {
                        TaskID = Convert.ToInt32(reader["TaskID"]),
                        TaskTitle = Convert.ToString(reader["TaskTitle"]),
                        TaskDescription = Convert.ToString(reader["TaskDescription"]),
                        CategoryID = Convert.ToInt32(reader["CategoryID"]),
                        CategoryName = Convert.ToString(reader["CategoryName"]),
                        PriorityLevel = Convert.ToByte(reader["PriorityLevel"]),
                        Status = Convert.ToString(reader["Status"]),
                        DueDate = Convert.ToDateTime(reader["DueDate"]),
                        CompletedDate = reader["CompletedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["CompletedDate"])
                    };
                taskViewModels.Add(item);
                }
            
            connection.Close();
            return Ok(taskViewModels);
        }
    }
        
}
