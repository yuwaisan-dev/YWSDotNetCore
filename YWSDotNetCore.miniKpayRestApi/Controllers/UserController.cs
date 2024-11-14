using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using YWSDotNetCore.miniKpayRestApi.Model;

namespace YWSDotNetCore.miniKpayRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly string _connectionString = "Data Source=.;Initial Catalog=YWSKpayMini;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";
        [HttpGet]
        public IActionResult GetUsers()
        {
            List<UserModel> lst = new List<UserModel>();

            SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();

            string query = @"SELECT [Id]
                          ,[FullName]
                          ,[MobileNumber]
                          ,[Balance]
                          ,[PinNumber]
                          ,[DeleteFlag]
                      FROM [dbo].[tbl_user] WHERE DeleteFlag = 0";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["Id"]);
                Console.WriteLine(reader["FullName"]);
                Console.WriteLine(reader["MobileNumber"]);
                Console.WriteLine(reader["PinNumber"]);
                
                var item = new UserModel
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    FullName = Convert.ToString(reader["FullName"]),
                    MobileNumber = Convert.ToString(reader["MobileNumber"]),
                    PinNumber = Convert.ToString(reader["PinNumber"]),
                    DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"])
                };

                lst.Add(item);

            }

            connection.Close();

            return Ok(lst);

        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"SELECT [Id]
                          ,[FullName]
                          ,[MobileNumber]
                          ,[Balance]
                          ,[PinNumber]
                          ,[DeleteFlag]
                      FROM [dbo].[tbl_user]where Id = @Id";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();

            if (dt.Rows.Count == 0)
            {
                return NotFound("No Data Found");
            }

            DataRow dr = dt.Rows[0];

            var blog = new UserModel
            {
                Id = Convert.ToInt32(dr["Id"]),
                FullName = Convert.ToString(dr["FullName"]),
                MobileNumber = Convert.ToString(dr["MobileNumber"]),
                PinNumber = Convert.ToString(dr["PinNumber"]),
                DeleteFlag = Convert.ToBoolean(dr["DeleteFlag"])
            };

            return Ok(blog);

        }

        [HttpPost]
        public IActionResult CreateUser(UserModel user)
        {
            
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();


            string query = $@"INSERT INTO [dbo].[tbl_user]
           ([FullName]
           ,[MobileNumber]
           ,[Balance]
           ,[PinNumber]
           ,[DeleteFlag])
     VALUES
           (@FullName
           ,@MobileNumber
           ,@Balance
           ,@PinNumber
           ,0)";
            SqlCommand cmd2 = new SqlCommand(query, connection);
            cmd2.Parameters.AddWithValue("@FullName", user.FullName);
            cmd2.Parameters.AddWithValue("@MobileNumber", user.MobileNumber);
            cmd2.Parameters.AddWithValue("@Balance", user.Balance);
            cmd2.Parameters.AddWithValue("@PinNumber", user.PinNumber);


            int result = cmd2.ExecuteNonQuery();

            connection.Close();

            return Ok(result > 0 ? "Create Successful" : "Create Failed");



        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, UserModel user)
        {


            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = $@"UPDATE [dbo].[tbl_user]
                               SET [FullName] = @FullName
                                  ,[MobileNumber] = @MobileNumber
                                  ,[Balance] = @Balance
                                  ,[PinNumber] = @PinNumber
                                  ,[DeleteFlag] = 0
                                WHERE Id = @Id";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@FullName", user.FullName);
            cmd.Parameters.AddWithValue("@MobileNumber", user.MobileNumber);
            cmd.Parameters.AddWithValue("@Balance", user.Balance);
            cmd.Parameters.AddWithValue("@PinNumber", user.PinNumber);

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            return Ok(result > 0 ? "UpdatePut successful." : "UpdatePut failed.");
        }

        //[HttpPatch("{id}")]
        //public IActionResult PatchUser(int id, UserModel user)
        //{
        //    string conditions = "";
        //    if (!string.IsNullOrEmpty(user.FullName))
        //    {
        //        conditions += " [FullName] = @FullName, ";
        //    }

        //    if (!string.IsNullOrEmpty(user.MobileNumber))
        //    {
        //        conditions += " [MobileNumber] = @MobileNumber, ";
        //    }

        //    if (!string.IsNullOrEmpty(user.Balance))
        //    {
        //        conditions += " [Balance] = @Balance, ";
        //    }

        //    if (!string.IsNullOrEmpty(user.PinNumber))
        //    {
        //        conditions += " [PinNumber] = @PinNumber, ";
        //    }
        //    if (conditions.Length == 0)
        //    {
        //        return BadRequest("Invalid Parameters");
        //    }

        //    conditions = conditions.Substring(0, conditions.Length - 2);

        //    SqlConnection connection = new SqlConnection(_connectionString);
        //    connection.Open();

        //    string query = $@"UPDATE [dbo].[tbl_user] SET {conditions} WHERE BlogId = @BlogId";

        //    SqlCommand cmd = new SqlCommand(query, connection);
        //    cmd.Parameters.AddWithValue("@Id", id);
        //    if (!string.IsNullOrEmpty(user.FullName))
        //    {
        //        cmd.Parameters.AddWithValue("@FullName", user.FullName);
        //    }
        //    if (!string.IsNullOrEmpty(user.MobileNumber))
        //    {
        //        cmd.Parameters.AddWithValue("@MobileNumber", user.MobileNumber);
        //    }
        //    if (!string.IsNullOrEmpty(user.Balance))
        //    {
        //        cmd.Parameters.AddWithValue("@Balance", user.Balance);
        //    }
        //    if (!string.IsNullOrEmpty(user.PinNumber))
        //    {
        //        cmd.Parameters.AddWithValue("@PinNumber", user.PinNumber);
        //    }
        //    int result = cmd.ExecuteNonQuery();

        //    connection.Close();

        //    return Ok(result > 0 ? "Update Successful" : "Update Failed");
        //}

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"DELETE FROM [dbo].[tbl_user] WHERE BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", id);
            int result = cmd.ExecuteNonQuery();

            connection.Close();
            return Ok(result > 0 ? "Delete successful" : "Delete failed");

        }
    }
}
