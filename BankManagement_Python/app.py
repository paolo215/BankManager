from flask import Flask
from flask import render_template
from flask import redirect
from flask import url_for
from flask import request
from argparse import ArgumentParser
from BankManager import BankManager
from gevent.wsgi import WSGIServer

# Instantiate Flask
app = Flask(__name__)

# Instantiate BankManager
bank_manager = BankManager()
bank_manager.create_account("admin", "password", "Paolo", "Villanueva", "My address")



@app.route("/")
def index():
    """
    Renders index.html when the user accesses main website or index.html
    :return:
    """
    return render_template("index.html")


@app.route("/login", methods=["GET", "POST"])
def login():
    """
    Renders the login page so the user can access their bank account
    :return:
    """

    error = None
    try:

        # Obtain username and password when the user clicks the log in button
        if request.method == "POST":
            username = request.form["username"]
            password = request.form["password"]

            # Authenticate the user credentials
            response = bank_manager.authenticate_user(username, password)
            is_successful = response[0]
            error = response[1]

            # If username and password does not match, go back and post the error
            if is_successful == False:
                return render_template("login.html", error=error)


            # Redirect to the dashboard if login is successful
            return redirect(url_for("dashboard", username=username))

    except Exception as e:
        error = e

    # Go back to login page if error found
    return render_template("login.html", error=error)

@app.route("/register", methods=["GET", "POST"])
def register():
    error = None
    try:
        if request.method == "POST":
            username = request.form["username"]
            password = request.form["password"]
            first_name = request.form["first_name"]
            last_name = request.form["last_name"]
            address = request.form["address"]

            response = bank_manager.create_account(username, password, first_name, last_name, address)

            is_successful = response[0]
            message = response[1]

            if is_successful:
                return redirect(url_for("dashboard", username=username))
            else:
                return render_template("register.html", message=message)
    except Exception as e:
        error = e

    return render_template("register.html", error=error)

@app.route("/dashboard", methods=["GET", "POST"])
def dashboard():
    """
    Renders the dashboard. This is where the user can see their profile and be able to withdraw
    or deposit funds to their account.
    :return:
    """
    error = None
    username = request.args["username"]

    # Obtain account information
    account = bank_manager.get_user_account(username)

    try:
        # This is true when user clicks "Complete transaction" button
        if request.method == "POST":
            # Obtain option (withdraw or deposit) and amount
            option = request.form["option"]
            amount = int(request.form["amount"])
            response = None

            if option == "Withdraw":
                # Attempts to withdraw money from the user's account
                response = bank_manager.withdraw(username, amount)
            elif option == "Deposit":
                # Attempts to deposit money to the user's account
                response = bank_manager.deposit(username, amount)

            # Obtain output message
            message = response[1]
            return render_template("dashboard.html", user_data=account.get_data(), message=message)
    except Exception as e:
        error = e
    return render_template("dashboard.html", user_data=account.get_data(), message=error)

def dashboard_cli(username):
    account = bank_manager.get_user_account(username)
    account_info = account.get_data()
    print("==== Dashboard ====")
    print("Username: " + account_info["username"])
    print("First name: " + account_info["first_name"])
    print("Last name: " + account_info["last_name"])
    print("Address: " + account_info["address"])

    answer = dashboard_cli_menu()
    if answer == 1 or answer == 2:
        if answer == 1:
            print("\n==== Withdraw ====")
        elif answer == 2:
            print("\n==== Deposit ====")
        amount = required_number_cli("Enter a number: ")

        response = None
        if answer == 1:
            response = bank_manager.withdraw(username, amount)
        elif answer == 2:
            response = bank_manager.deposit(username, amount)

        is_successful = response[0]
        message = response[1]
        print(message)
    elif answer == 3:
        history = account_info["history"]
        print("\n\n ==== Transaction history ====\n")
        for transaction in history:
            print(transaction)

        print("\n")
    elif answer == 4:
        return None

    return dashboard_cli(username)

def dashboard_cli_menu():
    answer = None
    while not answer and (answer <= 0 or answer > 4):
        print("""Choose what to do:
        1.) Withdraw
        2.) Deposit
        3.) See history
        4.) Exit
        """)
        answer = int(input("Enter a number: "))
    return answer

def run_cli():

    answer = main_menu_cli()
    username = None
    if answer == 1:
        username = required_input_cli("Enter your username: ")
        while bank_manager.check_account_exists(username):
            print("Account already exists. Please choose a different username.")
            username = required_input_cli("Enter your username: ")

        password = required_input_cli("Enter your password: ")
        first_name = required_input_cli("Enter your first name: ")
        last_name = required_input_cli("Enter your last name: ")
        address = required_input_cli("Enter your address: ")

        response = bank_manager.create_account(username, password, first_name, last_name, address)
        is_successful = response[0]
        message = response[1]

        print(message + "\n")
        if not is_successful:
            return run_cli()
    elif answer == 2:
        username = required_input_cli("Enter your username: ")
        password = required_input_cli("Enter your password: ")

        response = bank_manager.authenticate_user(username, password)
        is_successful = response[0]
        message = response[1]

        print(message + "\n")
        if not is_successful:
            return run_cli()


    elif answer == 3:
        print("Have a great day! :)")
        return None

    return dashboard_cli(username)

def required_input_cli(message):
    user_input = None
    while not user_input:
        user_input = raw_input(message)

    return user_input

def required_number_cli(message):
    user_input = ""
    while not user_input.isdigit():
        user_input = raw_input(message)

    return float(user_input)

def main_menu_cli():
    answer = None
    while not answer and (answer <= 0 or answer > 3):
        print("""Choose what to do:
        1.) Register
        2.) Log in
        3.) Exit
        """)
        answer = int(input("Enter a number: "))

    return answer

def main():
    option_parser = ArgumentParser()
    option_parser.add_argument("-g", "--gui", action="store_true")

    args = option_parser.parse_args()

    if args.gui:
        print("Starting up GUI. Navigate to: localhost:5000")
        server = WSGIServer(("", 5000), app)
        server.serve_forever()
    else:
        print("Starting up CLI.")
        run_cli()


if "__main__" == __name__:
    main()
