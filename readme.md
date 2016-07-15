# Csharp Week 3 Code Review

#### By Charlie Ewel, 07/15/2016

## Description

This program will model a hair salon with different stylists and clients and allow the user to edit the relationships therein

## Setup/Installation Requirements

This program can only be accessed on a PC with Windows 10, and with git and atom installed.

* Clone this repository
* Enter into the cloned directory in PowerShell
* Type following command into the Windows PowerShell > sqlcmd -S "(localdb)\mssqllocaldb" -i database_backups\hair_salon.sql
* Type following command into the Windows PowerShell > sqlcmd -S "(localdb)\mssqllocaldb" -i database_backups\hair_salon_test.sql
* Type following command into the Windows PowerShell > dnu restore
* Type following command into PowerShell > dnx kestrel
* Open Chrome and type in the following address: "localhost:5004"

## Known Bugs

No known bugs.

## Specifications

The program should ... | Example Input | Example Output | Why'd we choose this?
----- | ----- | ----- | ------
Allow user to add stylist| User fills out a form with stylist information | That stylist is added to a list of stylists | This is the basic functionality for save and get all for our stylist class
Allow user to add stylist|User fills out a form with client information, including which stylist they see | Client is added to a list of clients, with stylist information displayed therein | This allows us to test save and get all for client, along with find functionality for stylist
Allow user to click individual stylist from list of stylists and see all clients serviced by that stylist | Clicks "Tracy" on /stylists page | is taken to /stylist/Tracy page that has list of clients serviced by Tracy | Allow us to test our GetClients methods for our stylist object, and explemifies the link between the two tables
Allow user to edit and delete individual clients |Edits "Jasper" to change his hair color from red to brown| Jasper's hair is now listed as brown | Allows us to test CRUD for client objects
Allow users to edit and delete stylists, deleting the clients associated with said stylist in the process | User deletes stylist Tracy who is servicing clients Jasper and Wendy| Tracy is removed from list of stylists, Jasper and Wendy removed from list of clients | Allows us to test CRUD for all objects

## Support and Contact Details

Contact Epicodus for support in running this program.

## Technologies Used

* HTML
* C#

## License

*This software is licensed under the Microsoft ASP.NET license.*

Copyright (c) 2016 Charles Ewel
