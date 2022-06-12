using System;

namespace App.Command
{
    public interface ICommand
    {
        Task ExecuteAsync();
    }
}
