using System.Threading.Tasks;
using Abp.EntityFrameworkCore;
using SOSOWJB.Framework.EntityFrameworkCore;
using SOSOWJB.Framework.EntityFrameworkCore.Repositories;

namespace SOSOWJB.Framework.MultiTenancy.Payments
{
    public class SubscriptionPaymentRepository : FrameworkRepositoryBase<SubscriptionPayment, long>, ISubscriptionPaymentRepository
    {
        public SubscriptionPaymentRepository(IDbContextProvider<FrameworkDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<SubscriptionPayment> UpdateByGatewayAndPaymentIdAsync(SubscriptionPaymentGatewayType gateway, string paymentId, int? tenantId, SubscriptionPaymentStatus status)
        {
            var payment = await SingleAsync(p => p.PaymentId == paymentId && p.Gateway == gateway);

            payment.Status = status;

            if (tenantId.HasValue)
            {
                payment.TenantId = tenantId.Value;
            }

            return payment;
        }

        public async Task<SubscriptionPayment> GetByGatewayAndPaymentIdAsync(SubscriptionPaymentGatewayType gateway, string paymentId)
        {
            return await SingleAsync(p => p.PaymentId == paymentId && p.Gateway == gateway);
        }
    }
}
