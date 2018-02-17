namespace SOSOWJB.Framework.KYP.Orders
{
    public enum OrderStatus
    {
        WaitingForPayment = 1,
        WaitingForDelivery = 2,
        WaitingForConfirmation = 3,
        WaitingForApprising = 4,
        Completed = 5
    }
}