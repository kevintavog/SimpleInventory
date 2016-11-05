Questions:
===

I suspect some of my questions and my approach is due to treating this system as a reflection
of physical inventory - where the virtual system reflects what's expected. This causes a 
disconnect with expired items, as the items would still exist in the physical inventory.


When removing by Label, what if two different Types match?
---
I'm not a fan of removing by Label because of the possibility of having two items with
different Types but the same Label. My implmementation removes one of them, but that
seems a bit dodgy.


What should happen when an expired item is removed?
---
I remove it and return an error (ItemExpired), which is different than when the item can't
be found (ItemNotInStock). 

Another approach would be to discard expired items (with a notification for each expired item), 
and return any unexpired items (also with a notification that an item was removed).



Should adding an item be able to create a new Type?
---
I'm mildly uncomfortable that adding an item for a non-existent Type creates the 
type and adds the item. I'm interested in whether it's reasonable to require a separate
operation to create a type, so that adding an item requires the type to exist already.
(For this exercise, I'm allowing add item to create a new Type).

We had an email exchange about this, leading me to this:

For the client, they'd likely end up creating the Type each time, then create the
Label - because it's no more expensive than querying for the Type and going from there.
I guess I've answered my own question - creating the Type when adding a new item is
likely fine, depending on the requirements from the client. Also, if we cared,
we could get a notification when a new Type is created.

