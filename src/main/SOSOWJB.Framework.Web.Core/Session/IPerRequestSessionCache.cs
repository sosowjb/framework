using System.Threading.Tasks;
using SOSOWJB.Framework.Sessions.Dto;

namespace SOSOWJB.Framework.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}
