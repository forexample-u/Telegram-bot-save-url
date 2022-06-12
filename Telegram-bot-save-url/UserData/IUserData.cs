using System;

namespace App.UserData
{
    public interface IUserData
    {
        public string FirstName { get; }
        public string SecondName { get; }
        public string Username { get; }
        public long Id { get; }
        public string LastMessage { get; set; }
    }
}