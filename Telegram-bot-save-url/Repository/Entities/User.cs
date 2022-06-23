using System;
using System.ComponentModel.DataAnnotations;

namespace App.Repository.Entities
{
    //TODO: связать пользователя с книгами
    public class User
    {
        [Key]
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
    }
}
