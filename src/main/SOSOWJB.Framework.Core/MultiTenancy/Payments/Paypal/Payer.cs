using Newtonsoft.Json;

namespace SOSOWJB.Framework.MultiTenancy.Payments.Paypal
{
    public class Payer
    {
        [JsonProperty("payment_method")]
        public string PaymentMethod { get; set; }
    }
}