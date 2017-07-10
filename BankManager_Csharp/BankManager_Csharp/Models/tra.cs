﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankManager_Csharp.Models
{
    public partial class Transaction
    {


        /// <summary>
        /// Instantiates Transaction object. This is used to record successful transactions.
        /// </summary>
        /// <param name="transactionId"></param>
        /// <param name="accountId"></param>
        /// <param name="balance"></param>
        /// <param name="amount"></param>
        /// <param name="status"></param>
        /// <param name="date"></param>
        public Transaction(int accountId, double balance,
            int amount, String status, DateTime date)
        {
            this.accountId = accountId;
            this.balance = balance;
            this.amount = amount;
            this.status = status;
            this.date = date;

        }


        /// <summary>
        /// Returns human-readable datetime
        /// </summary>
        /// <returns></returns>
        public String getReadableDate()
        {
            return date.ToString("dd/MM/yyyy hh:mm:ss");
        }


        /// <summary>
        /// Pretty prints transaction information
        /// </summary>
        /// <returns></returns>
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