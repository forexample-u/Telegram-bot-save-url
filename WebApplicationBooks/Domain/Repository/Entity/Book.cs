using System.ComponentModel.DataAnnotations;

namespace WebApplicationBooks.Domain.Repository.Entity
{
    public class Book
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [Display(Name = "User Id")]
        public long UserId { get; set; }


        [Display(Name = "Categoria")]
        public string Categoria { get; set; }


        [Display(Name = "Url")]
        public string Url { get; set; }
    }
}
