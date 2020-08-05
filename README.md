# Buggy Demo Code 

Have you ever seen a killer demo that fizzles at the ends up because the root cause  ends up being a _Thread.Wait()_ or _throw new Exception()_! The problem with this approach is that it lacks  imagination becuase we all know "X never, ever, marks the spot".

The goal of this repository is to collect a list of subtle, possibly nontrivial,  bugs that can be easily integrated into .NET Core demos.  This app is not an end-to-end demo, it is simply designed to illustrate a classes of bugs and antipatterns that you can easily lift and shift into your own full demos.

These bugs have been curated from real world examples and have been categorized into symptoms that a customer might describe when troubleshooting as follows:

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
