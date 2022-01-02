# SupportCLI Documentation
The SupportCLI console application has been designed inline with SOA and Domain Driven Design(DDD - https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/ddd-oriented-microservice) to provide end user a console to invoke various commands for creating and managing tickets. 

## Solution Architecture

DDD approch has been used for designing the architecture of the solution by clearly segregating the each responsibility with clear structure.
 - **SupportCli.Console** : It is user interface of our solution signifying starting of the application and further processing as per the command invoked. This is the entry block of our program.
 - **SupportCli.Domain** : Responsible for representing concepts of the business, information about the business situation, and business rules. State that reflects the business situation is controlled and used here, even though the technical details of storing it are delegated to the infrastructure. This layer is the heart of our solution.
 - **SupportCli.Infrastructure** : Responsible for how the data that is initially held in domain entities (in memory) or another persistent store. Here just reused the dictionary storage for the timebeing. It contains all our ticket processing logic along with validation. 
 - **SupportCli.Tests** : Responsible for mirroring the structure of the code under tests.
 - **.NET 6** has been used for creating the solution.
 
 ![alt text](https://github.com/bishwaranjans/SupportCli/blob/master/Documentation/ArchitectureDiagram.PNG)
 
## SOLID
SOLID principle has been used across all level of codes wherever possible.

## Logging
Console logging has been implemented with dependency injection.

