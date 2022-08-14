using Autofac;
using MCBA_Admin.Controllers;
using MCBA_Admin.Tests.Base;

namespace MCBA_Admin.Tests.Controllers;

public class AccountsControllerTests : McbaAdminTest
{
    private readonly AccountsController _controller;

    public AccountsControllerTests()
    {
        _controller = Container.Resolve<AccountsController>();
    }

    // [Fact]
    // public async Task Index_ReturnsAViewResult_WithAListOfAccounts()
    // {
    //     // Act.
    //     var result = await _controller.Index();
    //
    //     // Assert.
    //     var viewResult = Assert.IsType<ViewResult>(result);
    //     Assert.IsAssignableFrom<IEnumerable<Account>>(viewResult.ViewData.Model);
    //      Assert.Equal(10, model.Count());
    // }
}