using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    public class BankAccount
    {
        static public event Action<BankAccount> CreateBankAccount;
        static public event Action<BankAccount> OpenBankAccountEvent;
        static public event Action<BankAccount> CloseBankAccountEvent;
        static public event Action<BankAccount, BankAccount, float> TranslationMoneyEvent;
        static Random random;

        static BankAccount()
        {
            random = new Random();
        }

        public BankAccount(Client Owner, float Money, bool IsDepsite, string Code)
        {
            this.Owner = Owner;
            this.Money = Money;
            this.IsOpen = true;
            this.IsDeposite = IsDepsite;
            this.Code = Code;

            this.Owner.bankAccounts.Add(this);
            CreateBankAccount?.Invoke(this);
        }

        public void EndOfTheMonth(int Percent)
        {
            if (IsDeposite && IsOpen)
                this.Money = this.Money * (100 + Percent) / 100;
        }

        public string TranslationMoney(BankAccount Recipient, float Money)
        {
            if (Recipient != this)
            {
                if (Recipient.IsOpen)
                {
                    if (Money < this.Money && Money >= 0)
                    {
                        Recipient.Money += Money;
                        this.Money -= Money;
                        TranslationMoneyEvent?.Invoke(this, Recipient, Money);
                        return "Все прошло успешно";
                    }
                    else
                        return "Недостаточно средств или нельзя переводить в минус";
                }
                else
                    return "Нельзя перевести деньги на закрытый счет";
            }
            else
                return "Нельзя перевести деньги самому себе";
        }   

        public void CloseOpenBankAccount()
        {
            if (this.IsOpen)
            {
                this.IsOpen = false;
                CloseBankAccountEvent?.Invoke(this);
            }
            else
            {
                this.IsOpen = true;
                OpenBankAccountEvent?.Invoke(this);
            }
        }

        public string Code { get; private set; }
        public Client Owner { get; private set; }
        public float Money { get ; set; }
        public bool IsDeposite { get; set; }
        public bool IsOpen { get; set; }
    }
}
