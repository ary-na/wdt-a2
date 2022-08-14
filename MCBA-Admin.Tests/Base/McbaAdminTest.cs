using MCBA_Admin.Tests.Modules;
using Autofac;

namespace MCBA_Admin.Tests.Base;

public abstract class McbaAdminTest : BaseTest
{
    protected McbaAdminTest() => Builder.RegisterModule<McbaAdminModule>();
}