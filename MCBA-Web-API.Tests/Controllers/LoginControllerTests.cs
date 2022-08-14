using Autofac;
using MCBA_Web_API.Controllers;
using MCBA_Web_API.Tests.Base;
using Microsoft.AspNetCore.Mvc;

namespace MCBA_Web_API.Tests.Controllers;

public class LoginControllerTests : McbaWebApiTest
{
    private readonly LoginController _controller;

    public LoginControllerTests()
    {
        _controller = Container.Resolve<LoginController>();
        _controller.ControllerContext = Container.Resolve<ControllerContext>();
    }
}