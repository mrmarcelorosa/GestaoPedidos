using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoPedidos.Models
{
    public class Client : People
    {
        [Column("phone")]
        public string Phone { get; set; } = String.Empty;
        public List<Address> Addresses { get; set; }

        public Client(string name, string email, string cpf, string phone)
        {
            Name = name;
            Email = email;
            Cpf = cpf;
            Phone = phone;
            Addresses = new List<Address>();
        }
    }
}
