from Account import Account
from Transaction import Transaction
import datetime

class BankManager(object):
    def __init__(self):
        self.account_data = {}
        self.transaction_data = {}
        self.next_account_id = 1
        self.next_transaction_id = 1

    def create_account(self, username, password, first_name, last_name, address):
        if not username in self.account_data:
            self.account_data[username] = Account(self.next_account_id, username, password, first_name, last_name, address)
            self.next_account_id += 1
            return (True, "Account has been successfully created")

        return (False, "Username already exists. Please enter a different username.")

    def authenticate_user(self, username, password):
        if not username in self.account_data:
            return (False, "Username does not exists", None)

        account = self.account_data[username]
        if account.username == username and account.password == password:
            return (True, "Success!", account)

        return (False, "Incorrect credentials. Try again.", None)


    def get_user_account(self, username):
        return self.account_data[username]


    def create_transaction(self, account, amount, status):
        balance = account.get_balance()
        account_id = account.get_account_id()
        datetime_now = datetime.datetime.now()
        transaction = Transaction(self.next_transaction_id, account_id, balance, amount, status, datetime_now)
        self.transaction_data[self.next_transaction_id] = transaction
        account.add_transaction(transaction)
        self.next_transaction_id += 1
        return True

    def withdraw(self, username, amount):
        account = self.account_data[username]
        balance = account.get_balance()
        if balance >= amount and amount > 0:
            self.create_transaction(account, amount, "WITHDRAW")
            account.withdraw(amount)
            return (True, "Success! Your balance is now: %s" % str(balance))

        return (False, "Insufficient amount of funds. Your balance is currently %s" % str(balance))


    def deposit(self, username, amount):
        account = self.account_data[username]
        balance = account.get_balance()
        if amount > 0:
            self.create_transaction(account, amount, "DEPOSIT")
            account.deposit(amount)
            return (True, "Success! Your balance is now %s" % str(balance))

        return (False, "Invalid amount of funds. Try again.")


