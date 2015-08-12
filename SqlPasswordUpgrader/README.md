# SQL Password Upgrader

This is not of use to many people.

## What it does

Opens the file `C:\temp\users.txt` which is expected to be tab-separated file of usernames and passwords with no header row:

	user1	badpassword
	user2	loco1
	...

It then iterates over every password, and where it doesn't meet the Azure security criteria (see bottom of document), "upgrades" it. Yes, this requires you to own the user passwords in plaintext. The goal with upgrading is to cause as little friction to login users as possible (i.e. make subtle, easy-to-remember changes).

### Output 

In `C:\temp`, find the `create-logins-*` and `report-*` files and copy them to a safe place!

## Azure password policy

 * At least 8 characters;
 * Must contain a choice of three from: uppercase letters, lowercase letters, numbers, special characters.

### Solution

One or both of these steps will be applied in order, if necessary:

**Password too short**
Take the first `x` characters of the username and add them to the end of the password, where `x` is the shortfall from 8 characters.

**Password not complex enough**
Add `+1` to the end of the password (all passwords already contain *some kind* of letter, so adding a number and a symbol brings us to three types).
