using Microsoft.EntityFrameworkCore;
using s3910902_a2.Data;
using s3910902_a2.Filters;
using s3910902_a2.Models.DataManagers;
using s3910902_a2.Services;

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

builder.Services.AddScoped<AccountManager>();

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
builder.Services.AddHostedService(sp => new NpmWatchHostedService(
    enabled: sp.GetRequiredService<IWebHostEnvironment>().IsDevelopment(),
    logger: sp.GetRequiredService<ILogger<NpmWatchHostedService>>()));
#endif

var app = builder.Build();

// Load data from customer web-service and insert into database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await CustomerWebService.GetAndSaveCustomersAsync(services);
        await SeedPayeesData.InitializePayees(services);
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