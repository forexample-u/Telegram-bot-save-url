using System;
using System.ComponentModel.DataAnnotations;

namespace App.Repository.Entities
{
    public class Book
    {
        [Key]
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Categoria { get; set; }
        public string Url { get; set; }
    }
}