using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoPedidos.Models
{
    public class Employee : People
    {

        [Column("login")]
        public string Login { get; set; } = string.Empty;

        [Column("password")]
        public string Password { get; set; } = string.Empty;

        public Employee(string name, string email, string cpf, string login, string password)
        {
            Name = name;
            Email = email;
            Cpf = cpf;
            Login = login;
            Password = password;
        }

        public bool VerificarSenha(string password)
        {
            return Password == password;
        }
    }
}
