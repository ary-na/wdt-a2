using MCBA_Customer.Controllers;
using MCBA_Customer.Tests.Base;
using MCBA_Customer.ViewModels;
using MCBA_Model.Models;
using MCBA_Model.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MCBA_Customer.Tests.Controllers;

// Code sourced and adapted from:
// Week 10 Lectorial - HomeControllerTests.cs

public class CustomerControllerTests : McbaCustomerTest
{
    private readonly CustomerController _controller;

    public CustomerControllerTests()
    {
        _controller = Container.ResolveController<CustomerController>();
    }

    [Theory]
    [InlineData(2100)]
    [InlineData(2200)]
    [InlineData(2300)]
    public async Task Index_ReturnsAViewResult_WithOneCustomer(int customerID)
    {
        // Arrange.
        _controller.HttpContext.Session.SetInt32(nameof(Customer.CustomerID), customerID);

        // Act.
        var result = await _controller.Index();

        // Assert.
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.IsAssignableFrom<Customer>(viewResult.ViewData.Model);
    }

    [Theory]
    [InlineData(2100)]
    [InlineData(2200)]
    [InlineData(2300)]
    public void EditProfile_ReturnsAViewResult_WithAEditProfileViewModel(int customerID)
    {
        // Act.
        var result = _controller.EditProfile(customerID);

        // Assert.
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.IsAssignableFrom<EditProfileViewModel>(viewResult.ViewData.Model);
    }

    [Theory]
    [InlineData(2100)]
    [InlineData(2200)]
    [InlineData(2300)]
    public void ChangePassword_ReturnsAViewResult_WithAChangePasswordViewModel(int customerID)
    {
        // Act.
        var result = _controller.ChangePassword(customerID);

        // Assert.
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.IsAssignableFrom<ChangePasswordViewModel>(viewResult.ViewData.Model);
    }
}