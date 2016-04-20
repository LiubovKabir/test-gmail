# test-gmail
This application provides two tests for Google Mail service (gmail.com) functional testing.

Set up TestMail.exe.conf file - modify TestMail.exe.conf.sample and save as TestMail.exe.conf. Configuration file must be stored in the same folder where .exe file is.

Parameters (key values) to be set:
<driverPath> = Path to chromedriver.exe
<browser> = one of two predefined names of browsers - Firefox or Chrome (Default browser to be run is Firefox.)
<username> = email address for Gmail
<password> = password
<test_to_run> = name of test to run or ALL
<logging> = type 'on' to enable logging or leave empty to disable
<test_message> = the last received message subject

Available tests to run:
- send_email:
	Login to the email account using predefined user credentials
	Send a new email to the same account
	Logout from the account;
- receive_email:
	Login to the email account using predefined user credentials
	Verify the delivery of the message (the newest one) - message specified with key <test_message> or received from "send_email" test run
    Logout from the account;

To run tests via Chrome browser key <driverPath> should be specified.

To run tests - use CLI:
E.g.: C:\Users\Liubov\Documents\TestRun>TestMail.exe TestMail.exe.config

If logging is enabled test run is logged to file test-log.txt

Known issues: when both tests are run sequentially in one Test Run and "send_email" fails - we are unable to run "receive_email" (do not know which email to expect).
