using MCBA_Customer.Controllers;
using MCBA_Customer.Tests.Base;
using MCBA_Customer.ViewModels;
using MCBA_Model.Utilities;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MCBA_Customer.Tests.Controllers;

public class TransactionsControllerTests : McbaCustomerTest
{
    private readonly TransactionsController _controller;

    public TransactionsControllerTests()
    {
        _controller = Container.ResolveController<TransactionsController>();
    }

    [Theory]
    [InlineData(4100)]
    [InlineData(4101)]
    [InlineData(4200)]
    [InlineData(4300)]
    public async Task Deposit_ReturnsAViewResult_WithADepositViewModel(int accountNumber)
    {
        // Act.
        var result = await _controller.Deposit(accountNumber);

        // Assert.
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.IsAssignableFrom<DepositViewModel>(viewResult.ViewData.Model);
    }

    [Theory]
    [InlineData(4100)]
    [InlineData(4101)]
    [InlineData(4200)]
    [InlineData(4300)]
    public async Task Withdraw_ReturnsAViewResult_WithAWithdrawViewModel(int accountNumber)
    {
        // Act.
        var result = await _controller.Withdraw(accountNumber);

        // Assert.
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.IsAssignableFrom<WithdrawViewModel>(viewResult.ViewData.Model);
    }

    [Theory]
    [InlineData(4100)]
    [InlineData(4101)]
    [InlineData(4200)]
    [InlineData(4300)]
    public async Task Transfer_ReturnsAViewResult_WithATransferViewModel(int accountNumber)
    {
        // Act.
        var result = await _controller.Transfer(accountNumber);

        // Assert.
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.IsAssignableFrom<TransferViewModel>(viewResult.ViewData.Model);
    }

    [Theory]
    [InlineData(4100)]
    [InlineData(4101)]
    [InlineData(4200)]
    [InlineData(4300)]
    public async Task Transfer_ReturnsAViewResult_WithABillPayViewModel(int accountNumber)
    {
        // Act.
        var result = await _controller.BillPay(accountNumber);

        // Assert.
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.IsAssignableFrom<BillPayViewModel>(viewResult.ViewData.Model);
    }
}