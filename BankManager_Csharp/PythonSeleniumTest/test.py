from selenium import webdriver


url = "http://localhost:54875"
chrome_driver_path = "chromedriver.exe"
chrome_driver = webdriver.Chrome(chrome_driver_path)

def register_test():
    # Homepage
    chrome_driver.get(url)

    # Click register
    register = chrome_driver.find_element_by_link_text("Register")
    register.click()

    register_account("admin", "password", "Paolo", "Villanueva", "My address")

def register_account(username, password, first_name, last_name, address):
    # Homepage
    chrome_driver.get(url)

    # Click register
    register = chrome_driver.find_element_by_link_text("Register")
    register.click()

    username_input = chrome_driver.find_element_by_id("username")
    password_input = chrome_driver.find_element_by_id("password")
    first_name_input = chrome_driver.find_element_by_id("first-name")
    last_name_input = chrome_driver.find_element_by_id("last-name")
    address_input = chrome_driver.find_element_by_id("address")
    submit = chrome_driver.find_element_by_xpath("//input[@type='submit']")

    username_input.send_keys(username)
    password_input.send_keys(password)
    first_name_input.send_keys(first_name)
    last_name_input.send_keys(last_name)
    address_input.send_keys(address)
    submit.click()


def main():
    register_test()


if "__main__" == __name__:
    main()