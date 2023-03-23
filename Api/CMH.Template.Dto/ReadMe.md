### DTO Package
The DTO assembly must be provided in a NuGet package to other domains. This assembly defines the 
contract between the API and its clients. Its classes should consist of POCOs with no logic.

Domain events are also defined within the DTO project. This allows the consuming domain to
deserialize events raised by the domain.