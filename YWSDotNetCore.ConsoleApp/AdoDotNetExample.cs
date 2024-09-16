using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YWSDotNetCore.ConsoleApp
{
    public class AdoDotNetExample
    {
        public readonly string _connectionString = "Data Source=.;Initial Catalog=YWSDotNetCore;User ID=sa;Password=sasa@123";
        public void Read()
        {
           
            Console.WriteLine("Connection string" + _connectionString);
            SqlConnection connection = new SqlConnection(_connectionString);

            Console.WriteLine("Connection openning......");
            connection.Open();
            Console.WriteLine("Connection opened.");


            string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_Blog]  WHERE DeleteFlag = 0";
            SqlCommand cmd = new SqlCommand(query, connection);
            //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //adapter.Fill(dt);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["BlogId"]);
                Console.WriteLine(reader["BlogTitle"]);
                Console.WriteLine(reader["BlogAuthor"]);
                Console.WriteLine(reader["BlogContent"]);
            }

            //For one line read data to show write after connection open 
            //foreach (DataRow dr in dt.Rows)
            //{
            //    Console.WriteLine(dr["BlogId"]);
            //    Console.WriteLine(dr["BlogTitle"]);
            //    Console.WriteLine(dr["BlogAuthor"]);
            //    Console.WriteLine(dr["BlogContent"]);
            //}

            Console.WriteLine("Connnection closing.....");
            connection.Close();
            Console.WriteLine("Connection closed.");

            //Dataset
            //DataTable
            //DataRow
            //DataColumn

            //foreach (DataRow dr in dt.Rows)
            //{
            //    Console.WriteLine(dr["BlogId"]);
            //    Console.WriteLine(dr["BlogTitle"]);
            //    Console.WriteLine(dr["BlogAuthor"]);
            //    Console.WriteLine(dr["BlogContent"]);
            //}
        }


        public void Create()
        {
            Console.WriteLine("Enter Blog Title");
            string title = Console.ReadLine();

            Console.WriteLine("Enter Blog Author");
            string author = Console.ReadLine();

            Console.WriteLine("Enter Blog Content");
            string content = Console.ReadLine();


            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            //string query2 = $@"INSERT INTO [dbo].[Tbl_Blog]
            //           ([BlogTitle]
            //           ,[BlogAuthor]
            //           ,[BlogContent]
            //           ,[DeleteFlag])
            //     VALUES
            //           ('{title}'
            //           ,'{author}'
            //           ,'{content}'
            //           ,0)";

            string query2 = $@"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent]
           ,[DeleteFlag])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent
           ,0)";
            SqlCommand cmd2 = new SqlCommand(query2, connection);
            cmd2.Parameters.AddWithValue("@BlogTitle", title);
            cmd2.Parameters.AddWithValue("@BlogAuthor", author);
            cmd2.Parameters.AddWithValue("@BlogContent", content);

            //SqlDataAdapter adapter = new SqlDataAdapter(cmd2);
            //DataTable dt = new DataTable();
            //adapter.Fill(dt);

            int result = cmd2.ExecuteNonQuery();

            connection.Close();

            //if(result == 1)
            //{
            //    Console.WriteLine("Saving Successful");
            //}
            //else
            //{
            //    Console.WriteLine("Saving Failed");
            //}

            Console.WriteLine(result == 1 ? "Saving Successful" : "Saving Failed");
        }


        public void Edit()
        {
            Console.Write("Enter BlogId : ");
            string id = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_Blog] where BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId",id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            if(dt.Rows.Count == 0)
            {
                Console.WriteLine("No Data Found");
                return;
            }

            DataRow dr = dt.Rows[0];

            Console.WriteLine(dr["BlogId"]);
            Console.WriteLine(dr["BlogTitle"]);
            Console.WriteLine(dr["BlogAuthor"]);
            Console.WriteLine(dr["BlogContent"]);

        }
    }
}
