using AGDATAAssessment.Data;
using AGDATAAssessment.Framework;
using AGDATAAssessment.Interfaces;
using AGDATAAssessment.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

// Add In Memory database
var dbconn = new SqliteConnection("Filename=:memory:");
dbconn.Open();

builder.Services.AddDbContext<AgDataDbContext>(db => db.UseSqlite(dbconn));


// Initialize caching
builder.Services.AddMemoryCache();


// Add scoped service for dependency injection
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IApplicationCache, ApplicationCache>();


// Remove default model state behavior so that custom exception filter can manage exceptions
builder.Services.Configure<ApiBehaviorOptions>(options
    => options.SuppressModelStateInvalidFilter = true);


// Add custom exception handling for validation
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationExceptionFilter>();
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => {
    x.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "AGDATA Web API",
        Version = "v1"
    });

    var filePath = Path.Combine(System.AppContext.BaseDirectory, "AGDATAAssessment.xml");
    x.IncludeXmlComments(filePath);
});


var app = builder.Build();


// Initialize In-Memory database
var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetService<AgDataDbContext>();

db.Database.EnsureDeleted();
db.Database.EnsureCreated();


// API controller to direct exceptions to
app.UseExceptionHandler("/error");


// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AGDATA Web API");
});


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
