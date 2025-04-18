using Microsoft.EntityFrameworkCore;
using Cumilative1.Data;
using Cumulative1.Models;
using Cumilative1.Controllers;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<Cumulative1Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Cumulative1Context") ?? throw new InvalidOperationException("Connection string 'Cumulative1Context' not found.")));


builder.Services.AddScoped<TeacherAPIController>();


builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<Cumulative1.Models.SchoolDbContext>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
