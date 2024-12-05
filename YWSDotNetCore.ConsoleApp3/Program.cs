// See https://aka.ms/new-console-template for more information
using YWSDotNetCore.ConsoleApp3;

Console.WriteLine("Hello, World!");
//get
//edit
//post
//update
//delete

//endpont
//resource


//HttpClientExample httpClientExample = new HttpClientExample();
//await httpClientExample.Read();
//await httpClientExample.Edit(1);
//await httpClientExample.Edit(100);
//await httpClientExample.Create("test title", "test body", 1);
//await httpClientExample.Update(1, "testing", "testing", 1);
//await httpClientExample.Delete(1);

//RestClientExample restClientExample = new RestClientExample();
//await restClientExample.Read();
//await restClientExample.Edit(1);
//await restClientExample.Create("test title", "test body", 1);
//await restClientExample.Update(2, "testing", "testing", 2);
//await restClientExample.Delete(2);

RefitExample refitExample = new RefitExample();
 await refitExample.Run();