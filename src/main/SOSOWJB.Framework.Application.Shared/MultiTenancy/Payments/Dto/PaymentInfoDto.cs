using SOSOWJB.Framework.Editions.Dto;

namespace SOSOWJB.Framework.MultiTenancy.Payments.Dto
{
    public class PaymentInfoDto
    {
        public EditionSelectDto Edition { get; set; }

        public decimal AdditionalPrice { get; set; }
    }
}
