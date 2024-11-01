using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YWSDotNetCore.Shared;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static YWSDotNetCore.Shared.AdoDotNetService;

namespace YWSDotNetCore.ConsoleApp
{

    internal class AdoDotNetExample2
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=YWSDotNetCore;User ID=sa;Password=sasa@123";
        private readonly AdoDotNetService _adoDotNetService;

        public AdoDotNetExample2()
        {
            _adoDotNetService = new AdoDotNetService(_connectionString);
        }

        public void Read()
        {
            string query = @"SELECT [BlogId]
                              ,[BlogTitle]
                              ,[BlogAuthor]
                              ,[BlogContent]
                              ,[DeleteFlag]
                          FROM [dbo].[Tbl_Blog]  WHERE DeleteFlag = 0";
            var dt = _adoDotNetService.Query(query);
            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["BlogId"]);
                Console.WriteLine(dr["BlogTitle"]);
                Console.WriteLine(dr["BlogAuthor"]);
                Console.WriteLine(dr["BlogContent"]);
            }
        }

        public void Edit()
        {
            Console.Write("Enter BlogId : ");
            string id = Console.ReadLine();
           
            string query = @"SELECT [BlogId]
                                  ,[BlogTitle]
                                  ,[BlogAuthor]
                                  ,[BlogContent]
                                  ,[DeleteFlag]
                              FROM [dbo].[Tbl_Blog] where BlogId = @BlogId";

            //SqlParameterModel[] sqlParameters = new SqlParameterModel[1];
            //sqlParameters[0] = new SqlParameterModel
            //{
            //    Name = "@BlogId",
            //    Value = id
            //};

            //var dt = _adoDotNetService.Query(query, sqlParameters);

            var dt = _adoDotNetService.Query(query, new SqlParameterModel
            {
                Name = "@BlogId",
                Value = id
            });

            DataRow dr = dt.Rows[0];
            Console.WriteLine(dr["BlogId"]);
            Console.WriteLine(dr["BlogTitle"]);
            Console.WriteLine(dr["BlogAuthor"]);
            Console.WriteLine(dr["BlogContent"]);

        }

        public void Create()
        {
            Console.WriteLine("Enter Blog Title");
            string title = Console.ReadLine();

            Console.WriteLine("Enter Blog Author");
            string author = Console.ReadLine();

            Console.WriteLine("Enter Blog Content");
            string content = Console.ReadLine();

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

            //int result = _adoDotNetService.Execute(query2, new SqlParameterModel
            //{
            //    Name = "@BlogTitle",
            //    Value = title
            //}, new SqlParameterModel
            //{
            //    Name = "@BlogAuthor",
            //    Value = author
            //}, new SqlParameterModel
            //{
            //    Name = "@BlogContent",
            //    Value = content
            //});
            int result = _adoDotNetService.Execute(query2,
                new SqlParameterModel("@BlogTitle", title),
                new SqlParameterModel("@BlogAuthor", author),
                new SqlParameterModel("@BlogContent", content)
                );

            Console.WriteLine(result == 1 ? "Saving Successful" : "Saving Failed");

        }

        public void Update()
        {
            Console.WriteLine("Enter Blog Id");
            string id = Console.ReadLine();

            Console.WriteLine("Enter Blog Title");
            string title = Console.ReadLine();

            Console.WriteLine("Enter Blog Author");
            string author = Console.ReadLine();

            Console.WriteLine("Enter Blog Content");
            string content = Console.ReadLine();

            string query = $@"UPDATE [dbo].[Tbl_Blog]
                                   SET [BlogTitle] = @BlogTitle
                                      ,[BlogAuthor] = @BlogAuthor
                                      ,[BlogContent] = @BlogContent
                                      ,[DeleteFlag] = 0
                                 WHERE BlogId = @BlogId";
            //int result = _adoDotNetService.Execute(query, new SqlParameterModel
            //{
            //    Name = "@BlogId",
            //    Value = id
            //}, new SqlParameterModel
            //{
            //    Name = "@BlogTitle",
            //    Value = title
            //}, new SqlParameterModel
            //{
            //    Name = "@BlogAuthor",
            //    Value = author
            //}, new SqlParameterModel
            //{
            //    Name = "@BlogContent",
            //    Value = content
            //});
            int result = _adoDotNetService.Execute(query,
               new SqlParameterModel("@BlogId",id),
               new SqlParameterModel("@BlogTitle", title),
               new SqlParameterModel("@BlogAuthor", author),
               new SqlParameterModel("@BlogContent", content)
               );

            Console.WriteLine(result == 1 ? "Update Successful" : "Update Failed");

        }

        public void Delete()
        {
            Console.Write("Enter BlogId : ");
            string id = Console.ReadLine();

            string query = @"DELETE FROM [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";

            //int result = _adoDotNetService.Execute(query, new SqlParameterModel
            //{
            //    Name = "@BlogId",
            //    Value = id
            //});
            int result = _adoDotNetService.Execute(query, new SqlParameterModel("@BlogId", id));
            Console.WriteLine(result == 1 ? "Delete Successful" : "Delete Failed");

        }

    }
}
