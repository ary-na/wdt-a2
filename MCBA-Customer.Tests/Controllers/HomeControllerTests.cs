using Autofac;
using MCBA_Customer.Controllers;
using Xunit;
using MCBA_Customer.Tests.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MCBA_Customer.Tests.Controllers;

public class HomeControllerTests : McbaCustomerTest
{
    private readonly ILogger<HomeController> _logger;

    public HomeControllerTests() => _logger = Container.Resolve<ILogger<HomeController>>();
    
    [Fact]
    public void Index_ReturnsAViewResult()
    {
        // Arrange.
        var controller = new HomeController(_logger);

        // Act.
        var result = controller.Index();

        // Assert.
        Assert.IsType<ViewResult>(result);
    }
    
    [Fact]
    public void Privacy_ReturnsAViewResult()
    {
        // Arrange.
        var controller = new HomeController(_logger);

        // Act.
        var result = controller.Privacy();

        // Assert.
        Assert.IsType<ViewResult>(result);
    }
}