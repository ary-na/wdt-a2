using Autofac;
using MCBA_Customer.Tests.Modules;

namespace MCBA_Customer.Tests.Base;

public abstract class McbaCustomerTest : BaseTest
{
    protected McbaCustomerTest() => Builder.RegisterModule<McbaCustomerModule>();
}