using System.Collections.Generic;
using SOSOWJB.Framework.Authorization.Users.Dto;
using SOSOWJB.Framework.Dto;

namespace SOSOWJB.Framework.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}