using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankManager_Csharp.Models
{
    public class Transaction
    {
        private int transactionId { get; }
        private int accountId { get; }
        private int balance { get; }
        private int amount { get; }
        private String status { get; }
        private DateTime date { get; }

        public Transaction(int transactionId, int accountId, int balance,
            int amount, String status, DateTime date)
        {
            this.transactionId = transactionId;
            this.accountId = accountId;
            this.balance = balance;
            this.amount = amount;
            this.status = status;
            this.date = date;

        }

        public String getReadableDate()
        {
            return date.ToString("dd/MM/yyyy hh:mm:ss");
        }

        public String prettyPrint()
        {
            return "Transaction id: " + transactionId + "\n"
                + "Account id: " + accountId + "\n"
                + "Balance: " + balance + "\n"
                + "Amount: " + amount + "\n"
                + "Status: " + status + "\n"
                + "Date: " + getReadableDate() + "\n";
        }
    }


}