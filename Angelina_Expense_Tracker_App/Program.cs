using Angelina_Expense_Tracker_App_Data_Access.Context;
using Angelina_Expense_Tracker_App_Data_Access.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


IConfiguration configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables().Build();
builder.Services.AddDbContext<Angelina_Expense_Tracker_App_Context>(
    optionsAction =>
    {
        optionsAction.UseSqlServer(configuration.GetConnectionString(name: "MyConnectionString"));
    }
);



// Add scoped service in Program.cs
builder.Services.AddScoped<ExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<Angelina_Expense_Tracker_App_Context, Angelina_Expense_Tracker_App_Context>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


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

app.MapControllers();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();



