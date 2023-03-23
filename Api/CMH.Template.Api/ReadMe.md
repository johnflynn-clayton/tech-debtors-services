### Private API
The API for the domain is considered "private" since it is the interface for which CRUD operations
take place from other domains within the same business unit and through the API gateway. This API
is typically a REST API, following Clayton's published standards and best practices.

### Responsibilities
As a "primary adapter" of the domain, the API should protect the domain from outside contract
changes and contradicting domain language by mapping the DTOs (public models) into internal models,
while protecting from bad input data. In DDD, this is another example of the "Anti-Corruption Layer". 
Similarly, returned data in responses is mapped back to a DTO to keep contracts in-tact.

The API should *not* contain any business logic.