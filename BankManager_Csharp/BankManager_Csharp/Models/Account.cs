using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankManager_Csharp.Models
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
        public ArrayList history { get; }

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
            this.history = new ArrayList();
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


        public String getPassword()
        {
            return password;
        }


        public ArrayList getHistory()
        {
            return history;
        }


        

    }
}