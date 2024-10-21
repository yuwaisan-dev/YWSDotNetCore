using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using YWSDotNetCore.RestApi.DataModels;
using YWSDotNetCore.RestApi.ViewModels;

namespace YWSDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsDapperController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=YWSDotNetCore;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";

        [HttpGet]
        public IActionResult GetBlogs()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                //  string query = @"select * from tbl_blog where DeleteFlag = 0;";
                string query = @"
            select 
                BlogId as Id, 
                BlogTitle as Title, 
                BlogAuthor as Author, 
                BlogContent as Content 
            from tbl_blog where DeleteFlag = 0;";
                var lst = db.Query<BlogViewModel>(query).ToList();
                return Ok(lst);
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
   string query = @"
            select 
                BlogId as Id, 
                BlogTitle as Title, 
                BlogAuthor as Author, 
                BlogContent as Content 
            from tbl_blog where DeleteFlag = 0 and BlogId = @Id;";                
                var item = db.Query<BlogViewModel>(query, new BlogViewModel
                {
                    Id = id
                }).FirstOrDefault();

                //if(item == null)
                if (item is null)
                {
                    return NotFound("No Data Found");
                }

                Console.WriteLine(item.Id);
                Console.WriteLine(item.Title);
                Console.WriteLine(item.Author);
                Console.WriteLine(item.Content);

                return Ok(item);
            }
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogDataModel blog)
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
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new BlogDataModel
                {
                    BlogTitle = blog.BlogTitle,
                    BlogAuthor = blog.BlogAuthor,
                    BlogContent = blog.BlogContent
                });
                return Ok(result > 0 ? "CreateDapper successful" : "CreateDapper failed");
            }

        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogViewModel blog)
        {

            string query = @"UPDATE [dbo].[Tbl_Blog]
         SET [BlogTitle] = @BlogTitle
              ,[BlogAuthor] = @BlogAuthor
              ,[BlogContent] = @BlogContent
              ,[DeleteFlag] = 0
         WHERE BlogId = @Id";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new BlogDataModel
                {
                   Id  =id,
                    BlogTitle = blog.Title,
                    BlogAuthor = blog.Author,
                    BlogContent = blog.Content
                });

                return Ok(result > 0 ? "UpdateDapperPut successful" : "UpdateDapperPut Failed");
            }
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogViewModel blog)
        {
            string conditions = "";
            if (!string.IsNullOrEmpty(blog.Title))
            {
                conditions += " [BlogTitle] = @BlogTitle, ";
            }

            if (!string.IsNullOrEmpty(blog.Author))
            {
                conditions += " [BlogAuthor] = @BlogAuthor, ";
            }

            if (!string.IsNullOrEmpty(blog.Content))
            {
                conditions += " [BlogContent] = @BlogContent, ";
            }

            if (conditions.Length == 0)
            {
                return BadRequest("Invalid Parameters");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);

            string query = $@"UPDATE [dbo].[Tbl_Blog] SET {conditions} WHERE BlogId = @Id";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new BlogDataModel
                {
                    Id = id,
                    BlogTitle = blog.Title,
                    BlogAuthor = blog.Author,
                    BlogContent = blog.Content
                });

                return Ok(result == 1 ? "UpdateDapperPatch Successful." : "UpdateDapperPatch Failed.");
            }

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            string query = @"DELETE FROM [dbo].[Tbl_Blog] WHERE BlogId = @Id";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new BlogDataModel
                {
                    Id = id
                });

                return Ok(result == 1 ? "Delete Successful" : "Delete Failed");
            }
        }
    }
}
