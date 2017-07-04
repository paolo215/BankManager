using System;
using System.Collections;
using System.Collections.Generic;

namespace BankManager_Csharp_CLI
{
    public class Account
    {
        public int accountId { get; }
        public String username { get; }
        public String password { get; }
        public String firstName { get; }
        public String lastName { get;  }
        public String address { get;  }
        public int balance { get; set; }
        public List<Transaction> history { get; }

        public Account(int accountId, String username, String password,
            String firstName, String lastName, String address)
        {
            this.accountId = accountId;
            this.username = username;
            this.password = password;
            this.firstName = firstName;
            this.lastName = lastName;
            this.address = address;
            this.balance = 0;
            this.history = new List<Transaction>();
        }

        public int withdraw(int amount)
        {
            balance -= amount;
            return balance;
        }

        public int deposit(int amount)
        {
            balance += amount;
            return balance;
        }

        public void addTransaction(Transaction transaction)
        {
            history.Add(transaction);
        }



        

    }
}