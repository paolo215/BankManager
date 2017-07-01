
class Account(object):
    def __init__(self, account_id, username, password, first_name, last_name, address):
        self.username = username
        self.account_id = account_id
        self.password = password
        self.first_name = first_name
        self.last_name = last_name
        self.address = address
        self.balance = 0
        self.history = []


    def get_username(self):
        return self.username

    def get_password(self):
        return self.password

    def withdraw(self, amount):
        self.balance -= amount
        return self.get_balance()


    def deposit(self, amount):
        self.balance += amount
        return self.get_balance()

    def get_account_id(self):
        return self.account_id

    def add_transaction(self, transaction):
        self.history.append(transaction)

    def get_balance(self):
        return self.balance

    def get_data(self):
        return {
                "username" : self.username,
                "first_name": self.first_name,
                "last_name" : self.last_name,
                "address": self.address,
                "balance": self.balance,
                "history": self.history
                }
