using System.ComponentModel.DataAnnotations;

namespace WebApplicationBooks.Domain.Repository.Entity
{
    public class User
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [Display(Name = "User Id")]
        public long UserId { get; set; }


        [Display(Name = "First Name")]
        public string FirstName { get; private set; }


        [Display(Name = "Second Name")]
        public string SecondName { get; private set; }


        [Display(Name = "Username")]
        public string Username { get; private set; }
    }
}
