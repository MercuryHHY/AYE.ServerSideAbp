//#define DEBUG

#undef DEBUG

using AyeDemo.Web;
using Microsoft.OpenApi.Models;


#if DEBUG

#region

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

#endregion


#else



//var builder = WebApplication.CreateBuilder(args);
//builder.WebHost.UseUrls(builder.Configuration["App:SelfUrl"]);
//builder.Host.UseAutofac();
////builder.Host.UseSerilog();
//await builder.Services.AddApplicationAsync<AyeDemoWebModule>();
//var app = builder.Build();
//await app.InitializeApplicationAsync();
//await app.RunAsync();





var builder = WebApplication.CreateBuilder(args);
builder.Host.UseAutofac();
//builder.Host.UseSerilog();
await builder.Services.AddApplicationAsync<AyeDemoWebModule>(it =>
{
    it.ApplicationName = "HHY";//测试  指定应用的唯一名称
    it.Environment= Environments.Staging;//测试 指定环境
} );
var app = builder.Build();
await app.InitializeApplicationAsync();
app.MapControllers();
await app.RunAsync();






#endif