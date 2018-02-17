using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using SOSOWJB.Framework.Authorization.Users;

namespace SOSOWJB.Framework.KYP.Accounts
{
    [Table("KYP_Accounts")]
    public class KypAccount : Entity
    {
        public long UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public double Balance { get; set; }
    }
}