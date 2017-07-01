import datetime

class Transaction(object):
    def __init__(self, transaction_id, account_id, balance, amount, status, date):
        """
        Creates a transaction
        :param transaction_id: transaction_id. Generated by BankManager.
        :param account_id:  account id
        :param balance: overall balance before this balance
        :param amount: amount of money being added/withdrawn
        :param status: WITHDRAW or DEPOSIT
        :param date: Datetime of when this transaction was done
        """
        self.transaction_id = transaction_id
        self.account_id = account_id
        self.balance = balance
        self.amount = amount
        self.status = status
        self.date = date

    def get_readable_date(self):
        """
        Returns human readate format date
        :return:
        """
        return self.date.strftime("%B %d, %Y %H:%M:%S")

    def __str__(self):
        """
        Overloads str and returns more visually appealing message for print out
        :return:
        """
        return "Transaction id: " + str(self.transaction_id) + "\n" \
            + "Account id: " + str(self.account_id) + "\n" \
            + "Balance: " + str(self.balance) + "\n" \
            + "Amount: " + str(self.amount) + "\n" \
            + "Status: " + self.status + "\n" \
            + "Date: " + str(self.date) + "\n" \

    def get_transaction_info(self):
        """
        Returns relevant transaction information
        :return:
        """
        return {
                "transaction_id": str(self.transaction_id),
                "account_id": str(self.account_id),
                "balance": str(self.balance),
                "amount": str(self.amount),
                "status": self.status,
                "date": self.date
                }



