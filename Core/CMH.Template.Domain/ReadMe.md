### Domain Core
The core of the domain contains the domain models that make up the Aggregate Roots, with their
Entities and Value Objects. In a pure DDD setup the Aggregate Roots contain the business
logic of the domain where possible. The rest of the business logic goes in the Domain
Services.

DDD prescribes an architecture where the domain models are protected at the core of the
domain, and the integration points toward other domains are at the boundaries of the domain. 
It follows naturally that all dependencies point "inward" toward the core. This style of
dependency management can also be seen in Onion Architecture and Hexagonal Architecture.