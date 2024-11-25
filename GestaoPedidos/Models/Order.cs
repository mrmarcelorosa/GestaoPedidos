using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoPedidos.Models
{
    [Table("order")]
    public class Order
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "É necessário informar um cliente")]
        public Client? Client { get; set; }

        [Column("client_id")]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "É necessário informar um endereço de cobrança")]
        public Address? BillingAddress { get; set; }

        [Column("billing_address_id")]
        public int BillingAddressId { get; set; }

        [Required(ErrorMessage = "É necessário informar um endereço de entrega")]
        public Address? DeliveryAddress { get; set; }

        [Column("delivery_address_id")]
        public int DeliveryAddressId { get; set; }

        [Column("order_date")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "É necessário informar produtos")]
        public List<OrderItem> OrderItems { get; set; }

        [Column("date_delete")]
        public DateTime? DateDelete { get; set; }


        public Order() 
        {
        }  
        public Order(int id, Client? client, Address? billingAddress, Address? deliveryAddress, DateTime orderDate)
        {
            Id = id;
            Client = client;
            BillingAddress = billingAddress;
            DeliveryAddress = deliveryAddress;
            OrderDate = orderDate;
            OrderItems = new List<OrderItem>();
        }
    }
}
