using System.Threading.Tasks;

namespace SOSOWJB.Framework.Identity
{
    public interface ISmsSender
    {
        Task SendAsync(string number, string message);
    }
}