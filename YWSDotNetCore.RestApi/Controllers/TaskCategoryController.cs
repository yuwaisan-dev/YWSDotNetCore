using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
    }
}
