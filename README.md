[![Build Status](https://travis-ci.com/mrhollen/dependency-resolver.svg?branch=master)](https://travis-ci.com/mrhollen/dependency-resolver)

# Dependency Resolver
This is my solution to the dependency resolution problem. It recurses through a list of dependencies to check for conflict. The list is pulled from a text file and as of right now, building the data structure in memeory and checking the dependencies are two separate actions, however they could potentially become one action at the cost of future flexability.

### Technologies
For this project I used C# .Net Core and xUnit to tests

### Testing
I included some unit tests, but more could be added for additional coverage. In addition, integration tests to test getting data out of a file could also be implemented.
