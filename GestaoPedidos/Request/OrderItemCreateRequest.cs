using System.ComponentModel.DataAnnotations;

namespace GestaoPedidos.Request
{
    public class OrderItemCreateRequest
    {
        [Required(ErrorMessage = "É necessário informar o produto")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "É necessário informar a quantidade")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior ou igual a 1.")]
        public int Amount { get; set; }

        public DateTime? DateDelete { get; set; }
    }
}
