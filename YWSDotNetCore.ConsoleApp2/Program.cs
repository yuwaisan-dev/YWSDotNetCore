// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using YWSDotNetCore.Database2.Models;

Console.WriteLine("Hello, World!");

//AppDbContext db = new AppDbContext();
//var list = db.TblBlogs.ToList();


var blog = new BlogModel
{
    Id = 1,
    Title = "Test Title",
    Author = "Test Author",
    Content = "Test Content",
};


//string jsonStr = JsonConvert.SerializeObject(blog,Formatting.Indented); // C# to Json
string jsonStr = blog.ToJson(); // C# to Json

Console.WriteLine(jsonStr);


string jonStr2 = """"
    {
        Id = 1,
        Title = "Test Title",
        Author = "Test Author",
        Content = "Test Content",
    } 
    """";
var blog2 = JsonConvert.DeserializeObject<BlogModel>(jonStr2);
Console.WriteLine(blog2.Title);

//System.Text.Json.JsonSerializer.Serialize(blog);
//System.Text.Json.JsonSerializer.Deserialize<BlogModel>(jonStr2);

Console.ReadLine(); 
public class BlogModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Content { get; set; }

}

public static class Extensions //DevCode
{
    public static string ToJson(this object obj)
    {
        string jsonStr = JsonConvert.SerializeObject(obj, Formatting.Indented);
        return jsonStr;
    }
}