# SQL Password Upgrader

This is not of use to many people.

## What it does

Opens the file `C:\temp\users.txt` which is expected to be tab-separated file of usernames and passwords with no header row:

	user1	badpassword
	user2	loco1
	...

It then iterates over every password, and where it doesn't meet the Azure security criteria (see bottom of document), "upgrades" it. Yes, this requires you to own the user passwords in plaintext. The goal with upgrading is to cause as little friction to login users as possible (i.e. make subtle, easy-to-remember changes).
lly be an SQL script.

Any extra columns in your source data (`users.txt`) can be passed through into the output document (See **Template** section).

### Output 

In `C:\temp`, find the `create-logins-*` and `report-*` files and copy them to a safe place!

### Template

The `create-logins` file contains the usernames and new passwords formatted according to a user-specified template (`template.txt` in the same directory as the exe). The template works using string.format notation like so:

	{0} will be replaced with the username
	{1} will be replaced with the new password
	{2} the content of the 3rd column, this idea extends through {3}, {4}, etc.

## Azure password policy

 * At least 8 characters;
 * Must contain a choice of three from: uppercase letters, lowercase letters, numbers, special characters.

### Solution

One or both of these steps will be applied in order, if necessary:

**Password too short**
Take the first `x` characters of the username and add them to the end of the password, where `x` is the shortfall from 8 characters.

**Password not complex enough**
Add `+1` to the end of the password (all passwords already contain *some kind* of letter, so adding a number and a symbol brings us to three types).
