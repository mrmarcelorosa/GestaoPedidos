using GestaoPedidos.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoPedidos.Models
{
    [Table("address")]
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("address_type")]
        public AddressType AddressType { get; set; }

        [Required(ErrorMessage = "O Logradouro é obrigatório")]
        [Column("logradouro")]
        public string Logradouro { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Número do endereço é obrigatório")]
        [Column("number_address")]
        public string NumberAddres { get; set; } = string.Empty;

        [Column("complement")]
        public string Complement { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Bairro é obrigatório")]
        [MaxLength(100, ErrorMessage ="Tamanho máximo permitido: 100 caracteres")]
        [Column("district")]
        public string District { get; set; } = string.Empty;

        [Required(ErrorMessage = "A cidade é obrigatória")]
        [MaxLength(100, ErrorMessage ="Tamanho máximo permitido: 100 caracteres")]
        [Column("city")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Estado é obrigatório")]
        [MaxLength(100, ErrorMessage ="Tamanho máximo permitido: 100 caracteres")]
        [Column("state")]
        public string State { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CEP é obrigatório")]
        [MaxLength(100, ErrorMessage = "Tamanho máximo permitido: 100 caracteres")]
        [Column("cep")]
        public string CEP { get; set; } = string.Empty;

        public Client? Client { get; set; }

        [Column("client_id")]
        public int ClientId { get; set; }

        [Column("date_delete")]
        public DateTime? DateDelete { get; set; }


        public Address() { }

        public Address(AddressType addressType, string logradouro, string numberAddres,
                  string district, string city, string state, string cep, string complement)
        {
            AddressType = addressType;
            Logradouro = logradouro;
            NumberAddres = numberAddres;
            District = district;
            City = city;
            State = state;
            CEP = cep;
            Complement = complement;
        }
    }
}
