using BlazorAppDubraskaAG.API.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext <BdbibliotecaContext> (option =>
{
    option.UseSqlServer("Server=JHAEL\\SQLPROGRAWEB;DataBase=BDBIBLIOTECA;Integrated Security=True;Encrypt=False");
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//activacion del middleware
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//middleware de cors
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();



