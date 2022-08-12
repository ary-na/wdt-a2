using MCBA_Customer.Data;
using MCBA_Web_API.Models.DataManagers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<McbaContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(McbaContext)) ??
                         throw new InvalidOperationException());
});

builder.Services.AddScoped<AccountManager>();
builder.Services.AddScoped<BillPayManager>();
builder.Services.AddScoped<CustomerManager>();
builder.Services.AddScoped<LoginManager>();
builder.Services.AddScoped<TransactionManger>();

builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();