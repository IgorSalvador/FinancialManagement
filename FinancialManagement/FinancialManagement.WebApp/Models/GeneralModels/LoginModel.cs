using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinancialManagement.WebApp.Models.GeneralModels
{
    public class LoginModel
    {
        [DisplayName("Nome de usuário")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Username { get; set; }

        [DisplayName("Senha")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Password { get; set; }
    }
}
