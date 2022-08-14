using Autofac;
using Autofac.Extensions.DependencyInjection;
using MCBA_Customer.Controllers;
using MCBA_Customer.Data;
using MCBA_Customer.Models.DataManagers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MCBA_Customer.Tests.Modules;


// Code sourced and adapted from:
// Week 10 Lectorial - BackendModule.cs
// Week 10 Tutorial - BackendModule.cs

// https://mookiefumi.com/2019-10-29-unit-testing-the-way-i-test-my-viewmodels
// https://stackoverflow.com/questions/72928549/error-while-add-migration-to-create-database-in-entity-framework-6-method-not-f
// https://autofac.readthedocs.io/en/latest/register/registration.html
// https://docs.autofac.org/en/latest/faq/select-by-context.html
// https://chsamii.medium.com/register-ef-core-with-autofac-2c8cb76d52d6


public class McbaCustomerModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

         var services = new ServiceCollection();
         services.AddControllersWithViews().AddApplicationPart(typeof(HomeController).Assembly)
             .AddControllersAsServices();
        
        builder.Populate(services);

        // Substitute all ILogger types.
        builder.RegisterInstance(new LoggerFactory()).As<ILoggerFactory>().SingleInstance();
        builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>)).SingleInstance();

        builder.Register(_ =>
        {
            var cacheOptions = new MemoryDistributedCacheOptions();
            var options = Options.Create(cacheOptions);
            var memoryDistributedCache = new MemoryDistributedCache(options);
            var factory = LoggerFactory.Create(b => b.AddConsole());
            var distributedSession = new DistributedSession(memoryDistributedCache, Guid.NewGuid().ToString(),
                TimeSpan.FromMinutes(20), TimeSpan.FromMinutes(1), () => true, factory, true);

            return new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    Session = distributedSession
                }
            };
        });

        // Register types for DI (Dependency Injection).
        builder.Register(_ =>
        {
            var context = new McbaContext(new DbContextOptionsBuilder<McbaContext>()
                .UseInMemoryDatabase(nameof(McbaContext)).Options);

            SeedCustomerData.InitializeCustomers(context);
            return context;
        });

        builder.RegisterType<CustomerManager>().AsSelf();
        builder.RegisterType<LoginManager>().AsSelf();
        builder.RegisterType<AccountManager>().AsSelf();
        builder.RegisterType<PayeeManager>().AsSelf();
    }
}