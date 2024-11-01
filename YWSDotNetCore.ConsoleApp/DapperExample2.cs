using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YWSDotNetCore.ConsoleApp.Models;
using YWSDotNetCore.Shared;

namespace YWSDotNetCore.ConsoleApp
{
    internal class DapperExample2
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=YWSDotNetCore;User ID=sa;Password=sasa@123";

        private readonly DapperService _dapperService;

        public DapperExample2()
        {
            _dapperService = new DapperService(_connectionString);          
        }
        public void Read()
        {
                string query = @"select * from tbl_blog where DeleteFlag = 0;";
                var lst = _dapperService.Query<BlogDataModel>(query).ToList();

                foreach (var item in lst)
                {
                    Console.WriteLine(item.BlogId);
                    Console.WriteLine(item.BlogTitle);
                    Console.WriteLine(item.BlogAuthor);
                    Console.WriteLine(item.BlogContent);
                }           
        }

        public void Edit(int id)
        {
                string query = @"select * from tbl_blog where DeleteFlag = 0 and BlogId = @BlogId;";
                var item = _dapperService.QueryFirstOrDefault<BlogDataModel>(query, new BlogDataModel
                {
                    BlogId = id
                });
                if (item is null)
                {
                    Console.WriteLine("No Dada Found.");
                    return;
                }

                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
        }

        public void Create(string title, string author, string content)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent]
           ,[DeleteFlag])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent
           ,0)";
           
                int result = _dapperService.Execute(query, new BlogDataModel
                {
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content
                });
                Console.WriteLine(result == 1 ? "Saving Successful." : "Saving Failed.");
            
        }

        public void Update(int id, string title, string author, string content)
        {
            string query = @"UPDATE [dbo].[Tbl_Blog]
         SET [BlogTitle] = @BlogTitle
              ,[BlogAuthor] = @BlogAuthor
              ,[BlogContent] = @BlogContent
              ,[DeleteFlag] = 0
         WHERE BlogId = @BlogId";

           
                int result = _dapperService.Execute(query, new BlogDataModel
                {
                    BlogId = id,
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content
                });

                Console.WriteLine(result == 1 ? "Update Successful." : "Update Failed.");
            
        }

        public void Delete(int id)
        {
            string query = @"DELETE FROM [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";

                int result = _dapperService.Execute(query, new BlogDataModel
                {
                    BlogId = id
                });

                Console.WriteLine(result == 1 ? "Delete Successful" : "Delete Failed");
            
        }
    }
}
