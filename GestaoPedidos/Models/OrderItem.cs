using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoPedidos.Models
{
    [Table("order_item")]
    public class OrderItem
    {

        [Key]
        public int Id { get; set; }

        [Column("unit_price")]
        public double UnitPrice { get; set; }

        [Column("amount")]
        public int Amount { get; set; }

        public Order? Order { get; set; }

        [Column("order_id")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "É necessário informar um produto")]
        public Product? Product { get; set; }

        [Column("product_id")]
        public int ProductId { get; set; }

        [Column("date_delete")]
        public DateTime? DateDelete { get; set; }


        public OrderItem() 
        {
        }
        public OrderItem(int id, double unitPrice, int amount, Order? order, Product? product)
        {
            Id = id;
            UnitPrice = unitPrice;
            Amount = amount;
            Order = order;
            Product = product;
        }
    }
}
