using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    class Client
    {
        public Client(string Name)
        {
            this.Name = Name;
            this.bankAccounts = new List<BankAccount>();
            DataBase.AddClient(this);
        }

        public Client() : this("Имя")
        {
            bankAccounts = new List<BankAccount>();
        }

        public string Name { get; set; }
        public List<BankAccount> bankAccounts { get; set; }
    }
}
