﻿// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Data.SqlClient;
using YWSDotNetCore.ConsoleApp;

Console.WriteLine("Hello World");
Console.ReadKey();

//md =>markdown

//C# <==> Database

//ADO.Net
//Drapper(ORM)
//EFCore /Entity Framework(ORM)


//C# => sql query => 

//nuget





//AdoDotNet=>CRUD
//AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();
//adoDotNetExample.Create();
//adoDotNetExample.Edit();
//adoDotNetExample.Update();
//adoDotNetExample.Delete();

//Dapper => CRUD
DapperExample dapperExample = new DapperExample();
//dapperExample.Read();
//dapperExample.Create("dapperTitle", "dapperAuthor", "dapperContent");
dapperExample.Edit(11);
dapperExample.Edit(13);
//dapperExample.Update(13, "testing", "testing", "testing");
//dapperExample.Delete(11);


Console.ReadKey();
