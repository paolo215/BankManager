from flask import Flask
from flask import render_template
from flask import redirect
from flask import url_for
from flask import request
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

        # Retrive username and password when the user clicks the log in button
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

@app.route("/dashboard", methods=["GET", "POST"])
def dashboard():
    """
    Renders the dashboard. This is where the user can see their profile and be able to withdraw
    or deposit funds to their account.
    :return:
    """
    error = None
    username = request.args["username"]

    # Retrive account information
    account = bank_manager.get_user_account(username)

    try:
        # This is True when user clicks "Complete transaction" button
        if request.method == "POST":
            # Obtain option (withdraw or deposit) and amount
            option = request.form["option"]
            amount = int(request.form["amount"])
            response = None

            if option == "Withdraw":
                # Attempts to withdraw money to the user's account
                response = bank_manager.withdraw(username, amount)
            elif option == "Deposit":
                # Attemps to deposit money to the user's account
                response = bank_manager.deposit(username, amount)

            # Obtain output message
            message = response[1]
            return render_template("dashboard.html", user_data=account.get_data(), message=message)
    except Exception as e:
        error = e
    return render_template("dashboard.html", user_data=account.get_data(), message=error)


def main():
    server = WSGIServer(("", 5000), app)
    server.serve_forever()


if "__main__" == __name__:
    main()
