using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Chat;
using App.Storage;

namespace App.Command
{
    public class CommandFactory
    {
        public void CreateCommand(string input, string name, ICommand run)
        {
            if (input == name)
            {
                run.Execute();
            }
        }
    }
}
