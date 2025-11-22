using Microsoft.EntityFrameworkCore;
using System;
using TGA.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddCors();
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: "allowCors",
//        builder =>
//        {
//            builder.WithOrigins("https://localhost:4200", "http://localhost:4200")
//            .AllowCredentials()
//            .AllowAnyHeader()
//            .AllowAnyMethod();
//        });
//});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UserDbContext>(options =>
{ 
options.UseSqlServer(builder.Configuration.GetConnectionString("TGACon"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
//builder.Services.AddDbContext<AppDbContext>(options =>
//{
//    options.UseSqlServer(connectionString);
//    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
//});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseRouting();
//app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
