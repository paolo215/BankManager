using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankManager_Csharp.Models
{
    public partial class Account
    {
        
 
        public List<Transaction> history { get; }


        /// <summary>
        /// Instantiates Account object. This represents a bank account
        /// for the user.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="address"></param>
        public Account(String username, String password,
            String firstName, String lastName, String address)
        {
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
        public double withdraw(double amount)
        {
            balance -= amount;
            return balance;
        }

        /// <summary>
        /// Deposits money from this account
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public double deposit(double amount)
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