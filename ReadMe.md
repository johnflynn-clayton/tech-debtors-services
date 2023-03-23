### Solution Structure
This solution is set up to encourage building domains on DDD principles, a design pattern used to rapidly 
deliver quality products. Recommended reading includes "the big blue book" by Eric Evans and
"the big red book" by Vaughn Vernon.

It should be noted that there are probably as many interpretations of DDD as there are DDD-based solutions,
so it is not a goal to be dogmatic at the expense of productivity. Even when using a loose interpretation of DDD,
using a common solution structure is helpful when developers cross team boundaries.

### Too many layers or mapping?
In small and simple domains, especially those with anemic domain models, it may seem that the application(API) layer 
is just a pass-through layer to the domain services, but we must resist the urge to combine the two layers. 
The seams between the domain's bounded context and the world are instrumental to the success of the domain. 
This Anti-Corruption Layer (ACL) allows the domain to evolve at a different speed than its dependencies.

Data Transfer Objects (DTO) and Domain Models are a must. Database entities may be eliminated, especially when 
not using an ORM, like Dapper.
