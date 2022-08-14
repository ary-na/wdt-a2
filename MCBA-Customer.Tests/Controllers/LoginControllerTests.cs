using MCBA_Customer.Controllers;
using MCBA_Customer.Tests.Base;
using MCBA_Model.Models;
using MCBA_Model.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MCBA_Customer.Tests.Controllers;

public class LoginControllerTests : McbaCustomerTest
{
    private readonly LoginController _controller;

    public LoginControllerTests()
    {
        _controller = Container.ResolveController<LoginController>();
    }

    [Fact]
    public void Login_ReturnsAViewResult()
    {
        // Act.
        var result = _controller.Login();

        // Assert.
        Assert.IsType<ViewResult>(result);
    }

    [Theory]
    [InlineData(2100, null)]
    [InlineData(2200, null)]
    [InlineData(2300, null)]
    public void Logout_ReturnsARedirectActionResult_WithClearedSession(int customerID, object expected)
    {
        // Arrange.
        _controller.HttpContext.Session.SetInt32(nameof(Customer.CustomerID), 2100);

        // Act.
        var result = _controller.Logout();

        // Assert.
        Assert.IsType<RedirectToActionResult>(result);
        Assert.Equivalent(expected, _controller.HttpContext.Session.GetInt32(nameof(Customer.CustomerID)));
    }
}