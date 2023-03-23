### Client Package
The API client is a NuGet package provided for the convenience of consumption by downstream domains.

Recommendations:
* Should only depend on the DTO project. Too many dependencies is disruptive to consumers.
* Should only wrap the REST API and not contain business logic.
* Caching and other richer behavior is generally better left to the consumer.
