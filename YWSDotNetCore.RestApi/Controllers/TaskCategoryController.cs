using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using YWSDotNetCore.RestApi.DataModels;
using YWSDotNetCore.RestApi.ViewModels;

namespace YWSDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskCategoryController : ControllerBase
    {
        public readonly string _connectionString = "Data Source=.;Initial Catalog=YWSDotNetCore;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";
        [HttpGet]
        public IActionResult GetAllCategorys()
        {
            List<TaskCategoryViewModel> categories= new List<TaskCategoryViewModel>();
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"SELECT [CategoryID]
      ,[CategoryName]
  FROM [dbo].[TaskCategory]";

            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var item = new TaskCategoryViewModel
                {
                    Id = Convert.ToInt32(reader["CategoryID"]),
                    Name = Convert.ToString(reader["CategoryName"])
                };
                categories.Add(item);
            }

            connection.Close();
            return Ok(categories);
        }

        [HttpPost]
        public IActionResult CreateTaskCategory(TaskCategoryDataModel category)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"INSERT INTO [dbo].[TaskCategory]
           ([CategoryName])
     VALUES
           (@CategoryName)";
            SqlCommand cmd2 = new SqlCommand(query, connection);
            cmd2.Parameters.AddWithValue("@CategoryName", category.CategoryName);
            
            int result = cmd2.ExecuteNonQuery();

            connection.Close();

            return Ok(result > 0 ? "Create Successful" : "Create Failed");

        }

        [HttpDelete]
        public IActionResult DeleteCategory(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"DELETE FROM [dbo].[TaskCategory]
      WHERE CategoryID = @CategoryID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@CategoryID", id);
            int result = cmd.ExecuteNonQuery();

            connection.Close();
            return Ok(result > 0 ? "Delete successful" : "Delete failed");
        }
    }
}
