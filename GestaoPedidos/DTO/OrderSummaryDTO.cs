namespace GestaoPedidos.DTO
{
    public class OrderSummaryDTO
    {
        public double TotalOrderValue { get; set; } 
        public string ClientName { get; set; } = string.Empty;
        public string ClientEmail { get; set; } = string.Empty;
        public string BillingAddress { get; set; } = string.Empty;
        public string DeliveryAddress { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public List<OrderDetailDTO> OrderDetails { get; set; }
    }
}
