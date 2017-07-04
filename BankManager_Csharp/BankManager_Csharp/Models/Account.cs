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
        public String lastName { get; }
        public String address { get; }
        public float balance { get; set; }
        public List<Transaction> history { get; }


        /// <summary>
        /// Instantiates Account object. This represents a bank account
        /// for the user.
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="address"></param>
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


        /// <summary>
        /// Withdraws money from this account
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public float withdraw(float amount)
        {
            balance -= amount;
            return balance;
        }

        /// <summary>
        /// Deposits money from this account
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public float deposit(float amount)
        {
            balance += amount;
            return balance;
        }

        /// <summary>
        /// Records previous transactions.
        /// </summary>
        /// <param name="transaction"></param>
        public void addTransaction(Transaction transaction)
        {
            history.Add(transaction);
        }


    }
}