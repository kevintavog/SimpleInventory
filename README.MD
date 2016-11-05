Running the application:
===
I used Xamarin Studio, though I expect it to work from Visual Studio as well. 

Load the SimpleInventory.sln, build everything, then run the Kekiri / NUnit tests in 
the 'Tests' project.


Design:
===
I'm using some form of Domain Driven Design, which is in four main components: Domain, 
Infrastructure, Api & Tests.

- The Domain folder contains model and services. The code in Domain doesn't have a build 
dependency on the other folders, though it clearly has runtime dependencies on the code 
in Infrastructure. Domain contains the business logic, such as returning an error when
an expired item is removed.

- Infrastructure is the interface between the Domain and other systems - databases or 
services, generally. In this case, I've used an in-memory repository rather than the
database that would normally be used for persistence between sessions. 

- Api is the exposed interface to other systems. In this case, I've used NancyFx to
expose a simple WebApi interface.

- Tests are what you expect - various tests. Typically, there'd be at least two
flavors of tests: end-to-end & domain. I've chosen to skip the end-to-end
tests for this simple exercise, so there aren't tests for the Web API layer
(which is clearly not acceptable for real production code).

Notes:
===
- I've skipped proper logging for this exercise; for any service in production, I'd 
want at least logging of failures to be available. And prefer more logging.

- I've skipped production'izing startup; it expects to run via the console rather than
as a Windows service.

- My repository implementation keeps things in order of when they were added, yet removes the 
oldest item - it could have kept the list sorted by expiration. If a database was used,
it would be easy enough to query for the oldest item via SQL, which means fewer items
would need to be accessed. Fortunately, such a change would be hidden from the Domain.

- I've ignored security completely. Ideally, there'd be a token based security and this
service could receive the token and validate it before allowing the operation. For further
control, permission for certain operations might be limited to groups/individuals, which
this service would enforce in the Domain.

- For an inventory system, I'd want the organization to consider auditing additions
& removals. (perhaps the notifications could be used?)

- I'm not sure I like the API endpoint usage I've chosen (GET/PUT/DELETE on /inventory).
Another approach I considered was GET/POST on /inventory and DELETE on /inventory/{id}.
I took the requirement verbatim (removing an item by Label), which I interpreted as not
removing via an id.
Perhaps: GET/POST on /inventory (to list/create labels), then GET/POST on 
/inventory/{label} (to list/create items in a Label). And either DELETE /inventory/{label}
(to delete the oldest one), or DELETE /inventory/{label}/{id} (which seems redundant, only
the id is really needed).
For real projects, I like to bounce this off others, something I didn't do in this case.

- Currently, when removing (by Label), if two or more Types having items matching the Label,
one of them will be removed.

- When an expired item is removed, I remove the item from the repository and return an
error (& add a notification).

- I check for expiring items when they are removed - for a longer running system, it
may be better to check inventory items periodically. My preference is to provide an
endpoint to check - the items would be removed and (probably) returned. This allows
an external entity to drive the schedule, but retains the encapsulation here.
Another approach is to check for expiration when the service is started and
periodically as it runs (either by taxing an existing service call, or running a separate
thread).

- In addition to the specified notification scenarios, this code also fires a notification
when an item is added.

- I chose to disallow expired items to be added to the inventory. They aren't returned,
it seems misleading to add them.