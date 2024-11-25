using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoPedidos.Models
{
    [Table("product")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="O Nome é obrigatório")]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("description")]
        public string Description { get; set; } = string.Empty ;

        [Required(ErrorMessage = "O Preço é obrigatório")]
        [Column("price")]
        public double Price { get; set; }

        [Required(ErrorMessage = "O Código SKU é obrigatório")]
        [Column("sku")]
        public string SKU { get; set; } = string.Empty;

        [Column("date_delete")]
        public DateTime? DateDelete { get; set; }

        public Product(int id, string name, string description, double price, string sKU)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            SKU = sKU;
        }
    }
}
