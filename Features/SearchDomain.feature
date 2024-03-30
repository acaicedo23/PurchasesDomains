Feature: SearchDomain

Search the availability of the following domain with the 'First year on sale!' and 'Popular'

Background:
Scenario Outline: Search Domain
Given User go to the URL page
And User enters the following domain <DomainName>
When User pulse search button
And User add to the shopping cart one domain <Characteristics1> and one domain <Characteristics2>
And Go to the shopping cart and Change the time for one domain for <Time>
Then Get the total in the purchases and reported it in the final test report and Compare with <ExpectedResult>
And Delete each domains one by one
And Verify cart is empty
#And Close browser

	Examples: 
	| DomainName   | Characteristics1    | Characteristics2 | Time    | ExpectedResult |
	| RTRC Testing | First year on sale! | Popular          | 5 Years | $205.82       |
