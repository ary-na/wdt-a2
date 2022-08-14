using Autofac;
using MCBA_Model.Models;
using MCBA_Model.Utilities;
using MCBA_Web_API.Controllers;
using MCBA_Web_API.Tests.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MCBA_Web_API.Tests.Controllers;

public class AccountControllerTests : McbaWebApiTest
{
    private readonly AccountController _controller;

    public AccountControllerTests()
    {
        _controller = Container.Resolve<AccountController>();
        // _controller.ControllerContext = Container.Resolve<ControllerContext>();
    }
    
    // [Fact]
    // public void GetAccounts_ReturnsIEnumerable_Accounts()
    // {
    //
    //     // Act.
    //     var result = _controller.GetAccounts();
    //
    //     // Assert.
    //     Assert.IsAssignableFrom<IEnumerable<Account>>(result);
    // }

}