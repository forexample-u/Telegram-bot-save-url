using System;
using System.ComponentModel.DataAnnotations;

namespace App.Repository.Entities
{
    public class User
    {
        [Key]
        public long Id { get; set; }
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Username { get; set; }
    }
}
