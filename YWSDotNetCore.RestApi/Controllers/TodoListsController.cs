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

        [HttpPost]
        public IActionResult CreateTask(TodoListDataMoedl task)
        { 
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"INSERT INTO [dbo].[ToDoList]
           ([TaskTitle]
           ,[TaskDescription]
           ,[CategoryID]
           ,[PriorityLevel]
           ,[Status]
           ,[DueDate]
           ,[CreatedDate]
           ,[CompletedDate])
     VALUES
           (@TaskTitle
           ,@TaskDescription
           ,@CategoryID
           ,@PriorityLevel
           ,@Status
           ,@DueDate
           ,@CreatedDate
           ,@CompletedDate)";
            SqlCommand cmd2 = new SqlCommand(query, connection);
            cmd2.Parameters.AddWithValue("@TaskTitle", task.TaskTitle);
            cmd2.Parameters.AddWithValue("@TaskDescription", task.TaskDescription);
            cmd2.Parameters.AddWithValue("@CategoryID", task.CategoryID);
            cmd2.Parameters.AddWithValue("@PriorityLevel",task.PriorityLevel);
            cmd2.Parameters.AddWithValue("@Status", task.Status);
            cmd2.Parameters.AddWithValue("@DueDate", task.DueDate);
            cmd2.Parameters.AddWithValue("@CreatedDate", task.CreatedDate);
            cmd2.Parameters.AddWithValue("@CompletedDate", task.CompletedDate);

            int result = cmd2.ExecuteNonQuery();

            connection.Close();

            return Ok(result > 0 ? "Create Successful" : "Create Failed");

        }

        [HttpDelete]
        public IActionResult DeleteTask(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"DELETE FROM [dbo].[ToDoList]
      WHERE TaskID = @TaskID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@TaskID", id);

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            return Ok(result > 0 ? "Delteting successful" : "Deleting failed");
        }
    }
        
}
