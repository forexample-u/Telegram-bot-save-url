using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Chat;
using App.Storage;

namespace App.Command
{
    public interface ICommand
    {
        void Execute();
    }
}
