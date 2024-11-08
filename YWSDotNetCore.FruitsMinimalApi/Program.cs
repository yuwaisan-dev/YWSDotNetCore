using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/fruits",() =>
{
    string folderPath = "Data/fruits.json";
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<FruitsResponseModel>(jsonStr);

    return Results.Ok(result);
})
.WithName("GetFruits")
.WithOpenApi();

app.MapGet("/fruits/{id}", (int id) =>
{
    string folderPath = "Data/fruits.json";
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<FruitsResponseModel>(jsonStr);

    var item = result.fruits.FirstOrDefault(x => x.id == id);

    if (item is null) return Results.BadRequest("No Data Found");

    return Results.Ok(item);
})
.WithName("GetFruit")
.WithOpenApi();


app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


public class FruitsResponseModel
{
    public Fruit[] fruits { get; set; }
}

public class Fruit
{
    public int id { get; set; }
    public string name { get; set; }
    public string image { get; set; }
    public int price { get; set; }
    public float weight { get; set; }
}

