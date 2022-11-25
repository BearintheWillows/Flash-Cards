# FlashCard App
  

Console App using:

- SQL Server
- ADO.Net
- C#
- Spectre.Console 
- Serilog

# Requriements:

  

- [x] This is an application where the users will create Stacks of Flashcards.
- (x) Atleast two different tables linked by Foreign keys.
- (x) Stack Names should be Unique.
- (x) Every flashcard needs to be part of a stack. If a stack is deleted, the same should happen with the flashcard.
- (x) Must use DTOs to show the flashcards to the user without the Id of the stack it belongs to.
- (x) When showing a stack to the user, the flashcard Ids should always start with 1 without gaps between them. If you have 10 cards and number 5 is deleted, the table should show Ids from 1 to 9.
- (x) After creating the flashcards functionalities, create a "Study Session" area, where the users will study the stacks. All study sessions should be stored, with date and score.
- (x) The study and stack tables should be linked. If a stack is deleted, it's study sessions should be deleted.
- (x) The project should contain a call to the study table so the users can see all their study sessions. This table receives insert calls upon each study session, but there shouldn't be update and delete calls to it.


# Features:

### Microsoft SQL Server Database

- The App connects to a Microsoft SQL server LocalDb.

### CRUD

- The user is able to Create, Read, Update and Delete both Stacks and Flashcards.
- The user is able to Read "Play" session data but NOT update or Delete the data.
- When a Stack is Delete the Flashcards attached to the Stack is also deleted.
- When a Stack is Deleted all Sessions are also deleted.

  
## Comments

 Flashcards was the longest project yet that I've attempted and seen to fruition. No doubt, certain areas could still be improved upon. However, it is in a fully functional and working state with all required features implemented. I have attempted to distribute the code into a sensible class/folder structure and have had the Single responsability principle in mind.

 ## How Would I update it

 If I came back to FlashCards then I would start with the following to improve it:

 - Re-factor Menu system to be more streamlined. Card Inspect and Stack Inspect currently have differing layouts.
 - Implement a report system to show how well a user has done over time.
 - Implement the ability to write an answer rather than confirm whether a user was correct or incorrect.
 - Re-factor the code base to apply the SOLID priciples more
  

## Resources Used:

- ReadMe file based on [thags](https://github.com/thags/ConsoleTimeLogger/blob/master/README.md)

- Project based on the Flashcards project by [thecsharpacademy](https://www.thecsharpacademy.com/project/14)

- Discord community for bug finding

- Serilog NuGet Package - [NuGet Gallery | Serilog 2.12.1-dev-01587](https://www.nuget.org/packages/Serilog/2.12.1-dev-01587)

- Spectre Console - [Spectre.Console - Welcome! (spectreconsole.net)](https://spectreconsole.net/)

- SQL Server Management Studio [SSMS](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)

- Microsoft SQL Server "Express" [MSSQL](https://www.microsoft.com/en-GB/sql-server/sql-server-downloads)