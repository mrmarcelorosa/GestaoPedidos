using System.ComponentModel.DataAnnotations;

namespace GestaoPedidos.Request
{
    public class OrderCreateRequest
    {

        [Required(ErrorMessage = "É necessário informar um cliente")]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "É necessário informar um endereço de cobrança")]
        public int BillingAddressId { get; set; }

        [Required(ErrorMessage = "É necessário informar um endereço de entrega")]
        public int DeliveryAddressId { get; set; }

        [Required(ErrorMessage = "É necessário informar produtos")]
        public List<OrderItemCreateRequest> OrderItems { get; set; }

        public DateTime? DateDelete { get; set; }

        public OrderCreateRequest()
        {
            OrderItems = new List<OrderItemCreateRequest>();
        }

    }
}
