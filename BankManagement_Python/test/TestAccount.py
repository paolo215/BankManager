import unittest
import datetime
from Account import Account
from Transaction import Transaction

class TestAccount(unittest.TestCase):

    def setUp(self):
        self.account = Account(1, "admin", "password", "Paolo", "Villanueva", "My Address")

    def test_withdrawing_money(self):
        self.account.balance = 100
        balance = self.account.withdraw(100)
        self.assertTrue(balance == 0)

    def test_deposit_money(self):
        self.account.balance = 0
        balance = self.account.deposit(100)
        self.assertTrue(balance == 100)

    def test_add_transaction(self):
        now = datetime.datetime.now()
        transaction = Transaction(1, self.account.account_id, 100, 100, "WITHDRAW", now)

        self.account.add_transaction(transaction)

        self.assertTrue(len(self.account.history) == 1)

        added_transaction = self.account.history[0]

        self.assertTrue(added_transaction.transaction_id == 1)
        self.assertTrue(added_transaction.account_id == self.account.account_id)
        self.assertTrue(added_transaction.balance == 100)
        self.assertTrue(added_transaction.amount == 100)
        self.assertTrue(added_transaction.status == "WITHDRAW")
        self.assertTrue(added_transaction.date == now)

    def test_add_one_more_transaction(self):
        now1 = datetime.datetime.now()
        transaction1 = Transaction(1, self.account.account_id, 100, 100, "WITHDRAW", now1)
        self.account.add_transaction(transaction1)
        now2 = datetime.datetime.now()
        transaction2 = Transaction(2, self.account.account_id, 100, 100, "DEPOSIT", now2)
        self.account.add_transaction(transaction2)


        self.assertTrue(len(self.account.history) == 2)

        added_transaction = self.account.history[0]
        self.assertTrue(added_transaction.transaction_id == 1)
        self.assertTrue(added_transaction.account_id == self.account.account_id)
        self.assertTrue(added_transaction.balance == 100)
        self.assertTrue(added_transaction.amount == 100)
        self.assertTrue(added_transaction.status == "WITHDRAW")
        self.assertTrue(added_transaction.date == now1)

        added_transaction = self.account.history[1]
        self.assertTrue(added_transaction.transaction_id == 2)
        self.assertTrue(added_transaction.account_id == self.account.account_id)
        self.assertTrue(added_transaction.balance == 100)
        self.assertTrue(added_transaction.amount == 100)
        self.assertTrue(added_transaction.status == "DEPOSIT")
        self.assertTrue(added_transaction.date == now2)

        



if "__main__" == __name__:
    unittest.main()
