using AyeDemo.Web;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();



//builder.Services.AddSwaggerGen();
builder.Services.AddAbpSwaggerGen(
    options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "AyeDemo", Version = "v1" });
        options.DocInclusionPredicate((docNmae, description) => true);
        options.CustomSchemaIds(type => type.FullName);
    }
);



builder.Host.UseAutofac();
await builder.Services.AddApplicationAsync<AyeDemoWebModule>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.InitializeApplicationAsync();


app.UseAuthorization();

app.MapControllers();

app.Run();
