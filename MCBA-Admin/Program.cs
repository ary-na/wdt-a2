using System.Net.Http.Headers;
using MCBA_Admin.Filters;
using MCBA_Admin.Models.DataManagers;

// Code sourced and adapted from:
// Week 9 Lectorial - MvcMovie, Program.cs
// https://rmit.instructure.com/courses/102750/files/26419005?wrap=1

// Week 6 Tutorial - Program.cs
// https://rmit.instructure.com/courses/102750/files/24426594?wrap=1

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configure api client.
builder.Services.AddHttpClient("api", client =>
{
    client.BaseAddress = new Uri("https://localhost:7249");
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddScoped<LoginManager>();
builder.Services.AddScoped<AccountsManager>();
builder.Services.AddScoped<CustomersManager>();
builder.Services.AddScoped<LoginsManager>();
builder.Services.AddScoped<BillPaysManager>();

// Store session into Web-Server memory.
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    // Make the session cookie essential.
    options.Cookie.IsEssential = true;
});

builder.Services.AddControllersWithViews(options => options.Filters.Add(new AuthoriseAdminAttribute()));

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
app.UseSession();
app.MapDefaultControllerRoute();
app.Run();