using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using SOSOWJB.Framework.MultiTenancy.Accounting.Dto;

namespace SOSOWJB.Framework.MultiTenancy.Accounting
{
    public interface IInvoiceAppService
    {
        Task<InvoiceDto> GetInvoiceInfo(EntityDto<long> input);

        Task CreateInvoice(CreateInvoiceDto input);
    }
}
