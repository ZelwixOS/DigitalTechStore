namespace Domain.Models
{
    public enum PurchaseStatus
    {
        WaitsForPaymentAprroval,
        WaitsInOulet,
        TransportingToOutlet,
        WaitsForDelivery,
        InDelivering,
        Refused,
        CanceledByClient,
        Finished,
    }
}
