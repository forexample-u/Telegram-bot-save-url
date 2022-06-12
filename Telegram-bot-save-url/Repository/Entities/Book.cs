using System;

namespace App.Repository.Entities
{
    public class Book
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Categoria { get; set; }
        public string Url { get; set; }
    }
}