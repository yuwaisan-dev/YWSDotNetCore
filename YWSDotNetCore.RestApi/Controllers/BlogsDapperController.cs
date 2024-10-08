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

        //[HttpGet("{id}")]
        //public IActionResult GetBlog(int id)
        //{

        //}

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

            //[HttpPut("{id}")]
            //public IActionResult UpdateBlog(int id, BlogViewModel blog)
            //{

            //}

            //[HttpPatch("{id}")]
            //public IActionResult PatchBlog(int id, BlogViewModel blog)
            //{

            //}

            //[HttpDelete("{id}")]
            //public IActionResult DeleteBlog(int id)
            //{

            //}
        }
}
