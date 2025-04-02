using Microsoft.EntityFrameworkCore;
using MYVCApp.Contexts;
using System.Reflection;

// FALSE FOR LOCAL, TRUE FOR CONCORDIA DATABASE
bool isProd = false;
//

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var configuration = builder.Configuration;

var appDataConnectionString = "";

if (isProd)
{
    appDataConnectionString = builder.Configuration.GetConnectionString("AppDataConnection");
}
else
{
    appDataConnectionString = builder.Configuration.GetConnectionString("LocalAppDataConnection");
}

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseMySQL(appDataConnectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MYVC API V1");
        c.RoutePrefix = "docs";
    });
}

app.Run();
