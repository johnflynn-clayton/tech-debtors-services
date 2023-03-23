### Adapters
The "secondary adapters", in Hexagonal Architecture terminology, provide implementations for interfaces
that are needed for the domain to interact with upstream domains and other third party components.

Typically this means that the Domain Core will contain an adapter interface, for which this assembly provides 
an implementation. The adapter normally is a wrapper around the API client of the upstream domain, and
maps incoming/outgoing objects between the domain model and the upstream DTOs. By placing the interfaces within
the Domain Core, it forces all dependencies to point inward to the Domain, preventing direct project references
from the Domain Core outward.