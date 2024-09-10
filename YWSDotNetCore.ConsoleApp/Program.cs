// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello World");
Console.ReadKey();

//md =>markdown

//C# <==> Database

//ADO.Net
//Drapper(ORM)
//EFCore /Entity Framework(ORM)


//C# => sql query => 

//nuget

string connectionString = "Data Source=.;Initial Catalog=YWSDotNetCore;User ID=sa;Password=sasa@123";
Console.WriteLine("Connection string" + connectionString);
SqlConnection connection = new SqlConnection(connectionString);

Console.WriteLine("Connection openning......");
connection.Open();
Console.WriteLine("Connection opened.");


string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_Blog]  WHERE DeleteFlag = 0";
SqlCommand cmd = new SqlCommand(query,connection);
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

Console.ReadKey();
