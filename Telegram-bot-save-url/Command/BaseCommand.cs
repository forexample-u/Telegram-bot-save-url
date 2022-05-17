using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Chat;
using App.Repository;

namespace App.Command
{
    public abstract class BaseCommand
    {
        protected IChat chat;
        protected IRepository<string, string> repository;

        public BaseCommand(IChat chat, IRepository<string, string> repository)
        {
            this.chat = chat;
            this.repository = repository;
        }
    }
}
