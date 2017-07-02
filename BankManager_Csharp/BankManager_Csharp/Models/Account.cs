using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankManager_Csharp.Models
{
    public class Account
    {
        private int accountId { get; }
        private String username { get; }
        private String password { get; }
        private String firstName { get; }
        private String lastName { get;  }
        private String address { get;  }
        private int balance { get; set; }
        private ArrayList history { get; }

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

        public int getAccountId()
        {
            return accountId;
        }

        public String getUsername()
        {
            return username;
        }

        public String getPassword()
        {
            return password;
        }

        public String getFirstName()
        {
            return firstName;
        }

        public String getLastName()
        {
            return lastName;
        }

        public String getAddress()
        {
            return address;
        }

    }
}