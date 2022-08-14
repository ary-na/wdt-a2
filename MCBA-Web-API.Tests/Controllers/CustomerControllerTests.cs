using Autofac;
using MCBA_Web_API.Controllers;
using MCBA_Web_API.Tests.Base;
using Microsoft.AspNetCore.Mvc;

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