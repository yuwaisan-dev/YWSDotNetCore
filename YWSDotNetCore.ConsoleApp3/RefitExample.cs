using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YWSDotNetCore.ConsoleApp3
{
    internal class RefitExample
    {
        public async Task Run()
        {
            var blogApi = RestService.For<IBlogApi>("https://localhost:7288");
            var lst = await blogApi.GetBlogs();
            foreach (var blog in lst)
            {
                Console.WriteLine(blog.BlogTitle);
            }
            var item2 = await blogApi.GetBlog(1);
            try
            {
                var item3 = await blogApi.GetBlog(100);

            }
            catch (ApiException ex)
            {
              if(ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("No Data Found");
                }
            }

            var item4 = await blogApi.CreateBlog(new BlogModel
            {
                BlogTitle = "test",
                BlogAuthor = "test",
                BlogContent = "test",
            });

            var item5 = await blogApi.DeleteBlog(1);

            var item6 = await blogApi.UpdateBlog(new BlogModel
            {
                    BlogId = 1,
                    BlogTitle = "update",
                    BlogAuthor = "update",
                    BlogContent = "update",
            });

        }
    }
}
