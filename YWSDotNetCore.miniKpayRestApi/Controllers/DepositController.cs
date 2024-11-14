using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using YWSDotNetCore.miniKpayRestApi.Model;

namespace YWSDotNetCore.miniKpayRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositController : ControllerBase
    {
        public readonly string _connectionString = "Data Source=.;Initial Catalog=YWSKpayMini;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";
        [HttpGet]
        public IActionResult GetDeposits()
        {
            List<DepositModel> lst = new List<DepositModel>();

            SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();

            string query = @"SELECT [DepositId]
                              ,[MobileNumber]
                              ,[Balance]
                              ,[DeleteFlag]
                          FROM [dbo].[tbl_deposit] WHERE DeleteFlag = 0";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["DepositId"]);
                Console.WriteLine(reader["MobileNumber"]);
                Console.WriteLine(reader["Balance"]);

                var item = new DepositModel
                {
                    DepositId = Convert.ToInt32(reader["DepositId"]),
                    MobileNumber = Convert.ToString(reader["MobileNumber"]),
                    Balance = Convert.ToDecimal(reader["Balance"]),
                    DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"])
                };

                lst.Add(item);

            }

            connection.Close();

            return Ok(lst);

        }

        [HttpGet("{id}")]
        public IActionResult GetDeposit(int id)
        {

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"SELECT [DepositId]
                              ,[MobileNumber]
                              ,[Balance]
                              ,[DeleteFlag]
                          FROM [dbo].[tbl_deposit] where DepositId = @DepositId";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@DepositId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();

            if (dt.Rows.Count == 0)
            {
                return NotFound("No Data Found");
            }

            DataRow dr = dt.Rows[0];

            var blog = new DepositModel
            {
                DepositId = Convert.ToInt32(dr["DepositId"]),
                MobileNumber = Convert.ToString(dr["MobileNumber"]),
                Balance = Convert.ToDecimal(dr["Balance"]),
                DeleteFlag = Convert.ToBoolean(dr["DeleteFlag"])
            };

            return Ok(blog);

        }
    }
}
