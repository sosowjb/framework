using SOSOWJB.Framework.Dto;

namespace SOSOWJB.Framework.Common.Dto
{
    public class FindUsersInput : PagedAndFilteredInputDto
    {
        public int? TenantId { get; set; }
    }
}