### Integration Tests
Integration tests are useful when testing the adapters which hold the concrete implementation of 
the domain interfaces.  They test the boundaries of the domain without needing to bootstrap and 
execute the entire solution.

When scripting integration tests, you must put additional thought into their long-term validity 
since they often depend on infrastructure that may reside outside of the domain, such as a database 
or 3rd party API. Often this can be resolved by not assuming the state of any outside resources and
by following these steps to setup and tear down the scaffoldoing for the test:

* TearDown - do not assume any prior tear-down was successful
* Setup - create the environment to run the test within
* Test - execute the tests within the expected environment, miniminzing the number of variables
* TearDown - clean up any remnants of the test on completion