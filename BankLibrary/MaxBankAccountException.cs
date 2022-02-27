using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    public class MaxBankAccountException : Exception
    {
        public string Name { get; set; }

        public MaxBankAccountException(string Message, string Name)
            : base(Message)
        {
            this.Name = Name;
        }
    }
}
