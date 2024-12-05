using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YWSDotNetCore.ConsoleApp3
{
    public interface IBlogApi
    {
        [Get("/api/blogs")]
        Task<List<BlogModel>> GetBlogs();

        [Get("/api/blogs/{id}")]
        Task<BlogModel> GetBlog(int id);

        [Post("/api/blogs")]
        Task<List<BlogModel>> CreateBlog(BlogModel blogModel);

        [Patch("/api/blogs")]
        Task<List<BlogModel>> UpdateBlog(BlogModel blogModel);

        [Delete("/api/blogs/{id}")]
        Task<BlogModel> DeleteBlog(int id);
    }
    public class BlogModel
    {
        public int BlogId { get; set; }

        public string BlogTitle { get; set; } = null!;

        public string BlogAuthor { get; set; } = null!;

        public string BlogContent { get; set; } = null!;

        public bool DeleteFlag { get; set; }
    }

}
