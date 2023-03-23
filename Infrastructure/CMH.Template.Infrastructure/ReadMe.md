### Infrastructure
It is recommended to put the bootstrapping and dependency injection container registration within a separate assembly, away from 
any hosting-specific concerns. Over time the API framework, deployment strategy, .Net version (Core vs. Framework), OS target,
etc. may change and evolve, while the container setup and configuration typically does not change.  This seam allows the two to
evolve on different timelines without affeting each other and allows us to create various application types using the same
bootstrapping logic.

The infrastructure project has the most dependencies since it connects the domain interfaces to the adapter and data concrete
implementations. It is common to have factory methods within this project that construct and configure any cross-cutting concerns
such as configuration loading, logging, and security for injection into the DI container.