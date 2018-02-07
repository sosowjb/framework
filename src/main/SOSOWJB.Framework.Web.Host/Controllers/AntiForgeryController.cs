using Microsoft.AspNetCore.Antiforgery;

namespace SOSOWJB.Framework.Web.Controllers
{
    public class AntiForgeryController : FrameworkControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
