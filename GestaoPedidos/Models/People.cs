using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace GestaoPedidos.Models
{
    [Table("people")]
    public abstract class People
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O Nome é obrigatório")]
        [MaxLength(50, ErrorMessage ="O tamanho máximo é de 50 caracteres")]
        [Column("name", TypeName ="varchar(50)")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "O E-mail é obrigatório")]
        [EmailAddress(ErrorMessage ="E-mail Inválido")]
        [MaxLength(255, ErrorMessage ="O tamanho máximo é de 255 caracteres")]
        [Column("email",TypeName ="varchar(255)")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CPF é obrigatório")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "CPF no formato inválido")]
        [Column("cpf",TypeName ="varchar(14)")]
        public string Cpf { get; set; } = string.Empty;

        [Column("date_delete")]
        public DateTime? DateDelete { get; set; }

        public Boolean CPFIsValid()
        {
            if (string.IsNullOrWhiteSpace(Cpf))
                return false;

            var cpfWhitoutMask = Regex.Replace(Cpf, @"\D", "");

            if (cpfWhitoutMask.Length != 11)
                return false;

            if (cpfWhitoutMask.All(c => c == cpfWhitoutMask[0]))
                return false;

            int sum1 = 0;
            for (int i = 0; i < 9; i++)
            {
                sum1 += int.Parse(cpfWhitoutMask[i].ToString()) * (10 - i);
            }

            int firstDigit = (sum1 * 10) % 11;
            if (firstDigit == 10 || firstDigit == 11)
                firstDigit = 0;

            int sum2 = 0;
            for (int i = 0; i < 9; i++)
            {
                sum2 += int.Parse(cpfWhitoutMask[i].ToString()) * (11 - i);
            }
            sum2 += firstDigit * 2;

            int secondDigit = (sum2 * 10) % 11;
            if (secondDigit == 10 || secondDigit == 11)
                secondDigit = 0;

            return cpfWhitoutMask[9] == firstDigit.ToString()[0] && cpfWhitoutMask[10] == secondDigit.ToString()[0];

        }

    }
}
