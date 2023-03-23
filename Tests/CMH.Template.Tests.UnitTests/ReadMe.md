### Unit Tests
Unit testing will primarily be done against the Domain project. It's high degree of dependency
injection and primary concern for business logic make it the entry point for most unit tests.
In fact if there are other projects that require significant unit testing, this may be a sign 
that they are doing too much.

For the unit tests, keep a one-to-one relationship between the tested code and the tests:

* `Project -> Test Project`
* `Namespace -> Namespace`
* `Class -> Test Fixture`

For the individual tests, name on the following consistent format:

`[Test]`  
public void `[TestedMethod]_[Scenario]_ExpectedResult`

For example:

`[Test]`  
public void CalculateCircleArea_RadiusIsNegavie_Exception
