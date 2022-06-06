using System;
using App.Repository;

namespace App.UserData
{
    public class UserTelegramData : IUserData
    {
        public UserTelegramData(long id, string firstName, string secondName, string username)
        {
            Id = id;
            FirstName = firstName;
            SecondName = secondName;
            Username = username;
        }

        public void SetMessage(string message)
        {
            LastMessage = message;
        }

        public string FirstName { get; private set; }
        public string SecondName { get; private set; }
        public string Username { get; private set; }
        public long Id { get; private set; }
        public string LastMessage { get; private set; }
    }
}