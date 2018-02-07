using Shouldly;
using SOSOWJB.Framework.Auditing;
using Xunit;

namespace SOSOWJB.Framework.Tests.Auditing
{
    public class NamespaceStripper_Tests: AppTestBase
    {
        private readonly INamespaceStripper _namespaceStripper;

        public NamespaceStripper_Tests()
        {
            _namespaceStripper = Resolve<INamespaceStripper>();
        }

        [Fact]
        public void Should_Stripe_Namespace()
        {
            var controllerName = _namespaceStripper.StripNameSpace("SOSOWJB.Framework.Web.Controllers.HomeController");
            ShouldBeStringTestExtensions.ShouldBe(controllerName, "HomeController");
        }

        [Theory]
        [InlineData("SOSOWJB.Framework.Auditing.GenericEntityService`1[[SOSOWJB.Framework.Storage.BinaryObject, SOSOWJB.Framework.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null]]", "GenericEntityService<BinaryObject>")]
        [InlineData("CompanyName.ProductName.Services.Base.EntityService`6[[CompanyName.ProductName.Entity.Book, CompanyName.ProductName.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null],[CompanyName.ProductName.Services.Dto.Book.CreateInput, N...", "EntityService<Book, CreateInput>")]
        [InlineData("SOSOWJB.Framework.Auditing.XEntityService`1[SOSOWJB.Framework.Auditing.AService`5[[SOSOWJB.Framework.Storage.BinaryObject, SOSOWJB.Framework.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null],[SOSOWJB.Framework.Storage.TestObject, SOSOWJB.Framework.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null],]]", "XEntityService<AService<BinaryObject, TestObject>>")]
        public void Should_Stripe_Generic_Namespace(string serviceName, string result)
        {
            var genericServiceName = _namespaceStripper.StripNameSpace(serviceName);
            ShouldBeStringTestExtensions.ShouldBe(genericServiceName, result);
        }
    }
}
