using System;

namespace BankManager_Csharp_CLI
{
    public class Transaction
    {
        public int transactionId { get; }
        public int accountId { get; }
        public int balance { get; }
        public int amount { get; }
        public String status { get; }
        public DateTime date { get; }

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