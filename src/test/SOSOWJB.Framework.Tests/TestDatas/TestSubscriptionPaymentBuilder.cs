using System.Linq;
using Abp.Application.Editions;
using SOSOWJB.Framework.Editions;
using SOSOWJB.Framework.EntityFrameworkCore;
using SOSOWJB.Framework.MultiTenancy.Payments;

namespace SOSOWJB.Framework.Tests.TestDatas
{

    public class TestSubscriptionPaymentBuilder
    {
        private readonly FrameworkDbContext _context;
        private readonly int _tenantId;

        public TestSubscriptionPaymentBuilder(FrameworkDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreatePayments();
        }

        private void CreatePayments()
        {
            var defaultEdition = Queryable.FirstOrDefault<Edition>(_context.Editions, e => e.Name == EditionManager.DefaultEditionName);

            CreatePayment(1, defaultEdition.Id, _tenantId, 2, "147741");
            CreatePayment(19, defaultEdition.Id, _tenantId, 29, "1477419");
        }

        private SubscriptionPayment CreatePayment(decimal amount, int editionId, int tenantId, int dayCount, string paymentId)
        {
            var payment = _context.SubscriptionPayments.Add(new SubscriptionPayment
            {
                Amount = amount,
                EditionId = editionId,
                TenantId = tenantId,
                DayCount = dayCount,
                PaymentId = paymentId
            }).Entity;
            _context.SaveChanges();

            return payment;
        }
    }

}
