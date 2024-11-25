namespace GestaoPedidos.DTO
{
    public class OrderHeaderDTO
    {
        public int OrderId { get; set; }
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        public string BillingAddress { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalPrice { get; set; }
    }
}
