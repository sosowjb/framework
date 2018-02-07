using System.Threading.Tasks;
using Abp.Dependency;

namespace SOSOWJB.Framework.MultiTenancy.Accounting
{
    public interface IInvoiceNumberGenerator : ITransientDependency
    {
        Task<string> GetNewInvoiceNumber();
    }
}