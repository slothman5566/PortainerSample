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
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.MapGet("api/v1/test", () =>
{
    return "Hello World!!";
})
.WithOpenApi();


app.MapGet("api/v2/test", () =>
{
    return "Hello World!!!";
})
.WithOpenApi();


app.MapGet("api/v3/test", () =>
{
    return "Hello World!!!";
})
.WithOpenApi();
app.Run();

