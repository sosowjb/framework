using Abp.Auditing;
using Microsoft.AspNetCore.Mvc;

namespace SOSOWJB.Framework.Web.Controllers
{
    public class HomeController : FrameworkControllerBase
    {
        [DisableAuditing]
        public IActionResult Index()
        {
            return Redirect("/swagger");
        }
    }
}
