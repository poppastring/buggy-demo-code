# Buggy Demo Code 

Have you ever seen a killer demo that fizzles at the end because the root cause is a contrived _Thread.Wait()_ or _throw new Exception()_! Demo's are hard enough by themselves so coming up with an realistic or inventive bug can be challenging.   

The goal of this repository is to collect a list of subtle, possibly nontrivial, bugs that can be easily integrated into your .NET Core demos. These bugs have been curated from real world examples and have been categorized into symptoms that a customer might describe. These are the kind of bugs you might miss during development but would become obvious in production or with some nominal cutomer load.

This app is not intended to represent an end-to-end demo, it is simply designed to illustrate several classes of bugs and antipatterns that you can easily lift and shift into your own full demos.

### Errors/Exceptions/Crashes
- Async void 
- Out of range exception
- Parallel Tasks accessing an ussafe collection

### Memory Leak
- Static reference causing a leak

### App is Unresponsive (high CPU)
- String concatenation causes excessive Garbage Collections
- Poorly designed Regex cause high CPU

### App is Unresponsive (low CPU)
- Sync-over-async causing thread pool starvation
- Deadlocks with Tasks
- Deadlocks with Threads
