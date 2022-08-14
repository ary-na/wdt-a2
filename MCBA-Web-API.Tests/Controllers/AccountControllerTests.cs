using Autofac;
using MCBA_Web_API.Controllers;
using MCBA_Web_API.Tests.Base;

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