import unittest
from BankManagement_Python import Account

class TestAccount(unittest.TestCase):

    def setUp(self):
        self.account = Account(1, "admin", "password", "Paolo", "Villanueva", "My Address")

    def test_withdrawing_money_with_no_enough_balance(self):
        self.account.balance = 0
        response = self.account.withdraw(100)
        is_successful = response[0]

        self.assertFalse(is_successful)


if "__main__" == __name__:
    unittest.main()
