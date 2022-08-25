using System.ComponentModel.DataAnnotations;

namespace WebApplicationBooks.Models
{
    public class SignUpUser
    {
        [Required(ErrorMessage = " - это обязательное поле")]
        [Display(Name = "Имя пользователя")]
        [MinLength(6, ErrorMessage = " - должно быть больше 6 символов")]
        public string Username { get; set; }

        [Required(ErrorMessage = " - это обязательное поле")]
        [Display(Name = "Пароль")]
        [MinLength(6, ErrorMessage = " - должен быть больше 6 символов")]
        public string Password { get; set; }

        [Required(ErrorMessage = " - это обязательное поле")]
        [Display(Name = "Имя в месседжере")]
        public string MessagerUsername { get; set; }
    }
}