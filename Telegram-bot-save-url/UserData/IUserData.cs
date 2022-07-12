using System;

namespace App.UserData
{
    public interface IUserData
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Username { get; set; }
        public string LastMessage { get; set; }
    }
}