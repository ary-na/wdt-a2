using Autofac;
using MCBA_Web_API.Controllers;
using MCBA_Web_API.Tests.Base;
using Microsoft.AspNetCore.Mvc;

namespace MCBA_Web_API.Tests.Controllers;

public class BillPayControllerTest : McbaWebApiTest
{
    private readonly BillPayController _controller;

    public BillPayControllerTest()
    {
        _controller = Container.Resolve<BillPayController>();
        _controller.ControllerContext = Container.Resolve<ControllerContext>();
    }
}