using Autofac;
using MCBA_Customer.ViewModels;
using MCBA_Model.Models;
using MCBA_Model.Utilities;
using MCBA_Web_API.Controllers;
using MCBA_Web_API.Tests.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MCBA_Web_API.Tests.Controllers;

public class CustomerControllerTests : McbaWebApiTest
{
    private readonly CustomerController _controller;

    public CustomerControllerTests()
    {
        _controller = Container.Resolve<CustomerController>();
        _controller.ControllerContext = Container.Resolve<ControllerContext>();
    }
}