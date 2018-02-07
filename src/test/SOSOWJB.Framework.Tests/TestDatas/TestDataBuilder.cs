using SOSOWJB.Framework.EntityFrameworkCore;

namespace SOSOWJB.Framework.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly FrameworkDbContext _context;
        private readonly int _tenantId;

        public TestDataBuilder(FrameworkDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            new TestOrganizationUnitsBuilder(_context, _tenantId).Create();
            new TestSubscriptionPaymentBuilder(_context, _tenantId).Create();

            _context.SaveChanges();
        }
    }
}
