using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SOSOWJB.Framework.MultiTenancy.Dto;
using SOSOWJB.Framework.MultiTenancy.Payments.Dto;

namespace SOSOWJB.Framework.MultiTenancy.Payments
{
    public interface IPaymentAppService : IApplicationService
    {
        Task<PaymentInfoDto> GetPaymentInfo(PaymentInfoInput input);

        Task<CreatePaymentResponse> CreatePayment(CreatePaymentDto input);

        Task<ExecutePaymentResponse> ExecutePayment(ExecutePaymentDto input);

        Task<PagedResultDto<SubscriptionPaymentListDto>> GetPaymentHistory(GetPaymentHistoryInput input);
    }
}
