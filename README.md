# Scheduler_back
This version of shift maker API for user who is a business owner or a schedule manager to make a schedule of employees. 

## Background
This shift maker app is for small business struggling with making working schedule. Most of small businesses are used to make a shift by hand or excel instead of scheduler platforms in major websites, even if they make mistakes to build schedules. The reviews from users using scheduler platforms showed that make user difficult to use with accessibility barriers, so this is what we need to have.

## User interaction and design
This is [version 1.3 of prototype](https://xd.adobe.com/view/630c5cec-0eee-46fc-beb8-0c575496b8bb-365f/) by adobe XD.

## Set up
Run **Shiftmaker_v.1.2.sql** in mysql workbench.
If there is error with **'utf8mb4_0900_ai_ci'**, change **'utf8mb4_0900_ai_ci'** to **'utf8mb4'.**

Change the DbConnectionString in appsettings.json:
```
Server=localhost;Database=dayoff;UserId=**yourid**;Password=**yourpassword**;respectbinaryflags=false;
```

## How to test
Using the swagger page to test it:
https://localhost:5001/swagger/index.html

How to use API without Auth API in Swagger
1. Login the website.
2. Get a user id and token.
3. Click the authorize.
4. Type ‘Bearer’ [space] and then your token in the value and authorize it. Example: Bearer 12345abcdef
5. If you need userId to use the API, get it from No.2.
