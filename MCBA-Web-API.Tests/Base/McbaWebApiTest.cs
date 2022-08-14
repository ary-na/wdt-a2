using Autofac;
using MCBA_Web_API.Tests.Modules;

namespace MCBA_Web_API.Tests.Base;

public abstract class McbaWebApiTest : BaseTest
{
    protected McbaWebApiTest() => Builder.RegisterModule<McbaWebApiModule>();
}