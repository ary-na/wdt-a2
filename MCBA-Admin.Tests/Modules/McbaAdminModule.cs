using System.Net.Http.Headers;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MCBA_Admin.Controllers;
using MCBA_Admin.Models.DataManagers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using LoginManager = MCBA_Admin.Models.DataManagers.LoginManager;

namespace MCBA_Admin.Tests.Modules;

public class McbaAdminModule : Module
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
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7249/api");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        });

        builder.RegisterType<CustomersManager>().AsSelf();
        builder.RegisterType<LoginsManager>().AsSelf();
        builder.RegisterType<LoginManager>().AsSelf();
        builder.RegisterType<AccountsManager>().AsSelf();
        builder.RegisterType<BillPaysManager>().AsSelf();
    }
}