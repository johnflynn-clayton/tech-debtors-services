### Data Repositories
The Data namespace contains all I/O related objects and data repositories. The classes within are
essentially specialized adapters for retrieving data from SQL Server, DB2, document databases, 
flat files, etc. 

The data repositories should primarily be concrete implementations of the interfaces defined
within the Domain core. At runtime the application host will wire up the interfaces in the domain
to the repository and allow for the depency inversion we require for making the business logic
the most important code in the domain. This also allows for extensive unit testing and easily
switching from one data provider to another.

 Utilizing Dapper as our ORM tool of choice, we can even further abstract the implementation of
 the data retrieval from the entity definitions.