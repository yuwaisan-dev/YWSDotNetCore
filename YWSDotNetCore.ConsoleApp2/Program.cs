// See https://aka.ms/new-console-template for more information
using YWSDotNetCore.Database2.Models;

Console.WriteLine("Hello, World!");

AppDbContext db = new AppDbContext();
var list = db.TblBlogs.ToList();