using Dapper.FluentMap;
using ReactProj;
using ReactProj.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("https://localhost:44424")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
var con = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped<IRepository>(sp => new Repository(con));
FluentMapper.Initialize(config =>
{
    config.AddMap(new APA8BallScoreMap());
    config.AddMap(new APAPlayerMap());
});
var app = builder.Build();
app.UseCors("AllowReactApp");
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

app.MapFallbackToFile("index.html");

app.Run();
