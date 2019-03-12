# CheckIp

A program that once every 30 minutes check if You have a new external IP.

I use it on my Linux server to tell me when my ISP have changed my external IP.

The program uses Google SMTP server to send email.

When the programs starts, it will send You an email with Your current Ip adress to confirm that it's working.

## To use the program You have to start the program with 3 arguments: 
### 1: The email adress You want to send the mail to.
### 2: The google account You want to send from.
### 3: The password to the google account.

Example: Dotnet run CheckIp.dll target@email.net from@email.com yourpassword
