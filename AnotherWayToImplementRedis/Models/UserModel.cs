using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AnotherWayToImplementRedis.Models
{
    public class UserModel
    {
        [Required]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter numbers only in Cpf/Cnpj")]
        [MaxLength(14, ErrorMessage = "Cpf/Cnpj must not be longer than 14 characters")]
        public string CpfCnpj { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public string Datetime { get; set; } = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(CpfCnpj) && !string.IsNullOrEmpty(Name);
        }
    }
}
