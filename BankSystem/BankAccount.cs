using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    class BankAccount
    {
        static Random random;

        static BankAccount()
        {
            random = new Random();
        }

        public BankAccount(Client Owner, int Money, bool IsDepsite)
        {
            this.Owner = Owner;
            this.Money = Money;
            this.IsOpen = true;
            this.IsDeposite = IsDepsite;

            GenerateNumber();

            foreach (var BankAccount in DataBase.BankAccountsCount)
            {
                if (BankAccount.Code == this.Code)
                    GenerateNumber();
            }

            this.Owner.bankAccounts.Add(this);
            DataBase.AddBankAccount(this);
        }

        private void GenerateNumber()
        {
            int s1 = random.Next(100, 999);
            int s2 = random.Next(100, 999);
            int s3 = random.Next(100, 999);
            this.Code = $"{s1}{s2}{s3}";
        }

        public void EndOfTheMonth(int Percent)
        {
            if (IsDeposite)
                this.Money = this.Money * (100 + Percent) / 100;
        }

        public string TranslationMoney(BankAccount Recipient, int Money)
        {
            if (Recipient != this)
            {
                if (Recipient.IsOpen)
                {
                    if (Money < this.Money)
                    {
                        Recipient.Money += Money;
                        this.Money -= Money;
                        return "Все прошло успешно";
                    }
                    else
                        return "Недостаточно средств";
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
                this.IsOpen = false;
            else
                this.IsOpen = true;
        }

        public string Code { get; private set; }
        public Client Owner { get; private set; }
        public int Money { get ; set; }
        public bool IsDeposite { get; set; }
        public bool IsOpen { get; set; }
    }
}
