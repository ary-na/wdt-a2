using MCBA_Customer.BackgroundServices;
using MCBA_Customer.Data;
using MCBA_Customer.DataManagers;
using MCBA_Customer.Filters;
using MCBA_Customer.Services;
using Microsoft.EntityFrameworkCore;

// Code sourced and adapted from:
// https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/projects?tabs=dotnet-core-cli
// https://mycodingtips.com/2021/9/21/your-target-project-projectname-doesnt-match-your-migrations-assembly-assemblyname-either-chang

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<McbaContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(McbaContext)) ??
                         throw new InvalidOperationException());
    builder.Services.AddControllersWithViews();

    // Enable lazy loading.
    options.UseLazyLoadingProxies();
});

builder.Services.AddScoped<CustomerManager>();
builder.Services.AddScoped<LoginManager>();
builder.Services.AddScoped<AccountManager>();
builder.Services.AddScoped<PayeeManager>();

builder.Services.AddDistributedSqlServerCache(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString(nameof(McbaContext));
    options.SchemaName = "dotnet";
    options.TableName = "SessionCache";
});

builder.Services.AddSession(options =>
{
    // Make the session cookie essential.
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromMinutes(20);
});

builder.Services.AddControllersWithViews(options => options.Filters.Add(new AuthoriseCustomerAttribute()));

// Add Npm service to the container
#if DEBUG
builder.Services.AddHostedService(sp => new NpmWatchHostedBackgroundService(
    enabled: sp.GetRequiredService<IWebHostEnvironment>().IsDevelopment(),
    logger: sp.GetRequiredService<ILogger<NpmWatchHostedBackgroundService>>()));
#endif

builder.Services.AddHostedService<BillPayBackgroundService>();

var app = builder.Build();

// Load data from customer web-service and insert into database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await CustomerWebService.GetAndSaveCustomersAsync(services);
        await SeedPayeesData.InitializePayeesAsync(services);
    }
    catch (Exception e)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(e, "An error occurred seeding data into database");
    }
}

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