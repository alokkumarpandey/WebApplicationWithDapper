﻿using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApplicationWithDapper.Data;
using WebApplicationWithDapper.DataFactory;
using WebApplicationWithDapper.Interfaces;
using WebApplicationWithDapper.Models.StudentDataAccessLayer;
using WebApplicationWithDapper.Repositories;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<WebApplicationWithDapperContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebApplicationWithDapperContext") ?? throw new InvalidOperationException("Connection string 'WebApplicationWithDapperContext' not found.")));


builder.Services.AddTransient<DapperDbContext, DapperDbContext>();
builder.Services.AddTransient<StudentDataAccessLayer, StudentDataAccessLayer>();

builder.Services.AddTransient<IMovie, MovieRepository>();
builder.Services.Configure<FormOptions>(options => options.MultipartBodyLengthLimit = 10485760); // 10MB
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
