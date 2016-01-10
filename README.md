# Budget Analyzer

This project helps analyzing your budget by aggregating 
bank account operations that one can upload with a bank
account statement.

There is no intergration with any banks API planned 
because most of banks don't have one. Your bank account
operations is sensitive data and no one except you have
right to expose it.

That's why you have to get bank statement and upload it
to your account in BudgetAnalyzer so it can process it
and provide you with some analitics on the data.

**Disclaimer** *There is no guarantee that your information
will be absolutely safe with BudgetAnalyzer. This project
is intended for personal use and should not be used as a
public service.* 

There are 2 reasons this project appeared: 

- I (Alexander Pavlyuk) need such a tool for myself
- I wanted to have some experience in brand new ASP.NET 5.

## Technology Stack

- **ASP.NET 5 MVC 6** with classic server-side MVC (almost no js)
- **PostgreSQL** with **Entity Framework 7**
- **DocumentFormat.OpenXML** for working with excel files
- **XUnit** for unit testing
- Not tested, but should work on Linux and OS X