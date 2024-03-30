using AutomationSearchDomain.PageObjects;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SearchDomain.Steps
{
    [Binding]
    public class SearchDomainSteps
    {
        private readonly SearchDomainPageObject _pageObject;

        public SearchDomainSteps(SearchDomainPageObject pageObject)
        {
            _pageObject = pageObject;
        }

        [Given(@"User go to the URL page")]
        public async Task GivenUserGoToTheUrlPageIsAsync()
        {
            await _pageObject.NavigateAsync();
        }

        [Given(@"User enters the following domain (.*)")]
        public async Task GivenUserEntersTheFollowingDomainIsAsync(string DomainNama)
        {
            await _pageObject.FillInSearchTextFieldAsync(DomainNama);
        }

        [When(@"User pulse search button")]
        public async Task WhenPulseLoginButtonIsAsync()
        {
            await _pageObject.ClickOnSearchButton();
        }

        [When(@"User add to the shopping cart one domain (.*) and one domain (.*)")]
        public async Task WhenUseraAddToTheShoppingCartOneDomineAndOneDomainIsAsync(string Characteristics1, string Characteristics2)
        {
            await _pageObject.ClickOnCharacteristics(Characteristics1);
            await _pageObject.ClickOnCharacteristics(Characteristics2);
        }

        [When(@"Go to the shopping cart and Change the time for one domain for (.*)")]
        public async Task WhenGoToTheShoppingCartAndChangeTheTimeForOneDomainFor(string Time)
        {
            await _pageObject.ChangeOneDomainFor(Time);
        }

        [Then(@"Get the total in the purchases and reported it in the final test report and Compare with (.*)")]
        public async Task ThenGetTheTotalInThePurchasesAndReportedItInTheFinalTestReportAndCompareWith(string expectedResult)
        {
            string ObtainedResult = await _pageObject.TotalPurcheses();
            Assert.IsTrue(ObtainedResult.ToLower().Contains(expectedResult.ToLower()));
        }

        [Then(@"Delete each domains one by one")]
        public async Task ThenDeleteEachDomainsOneByOne()
        {
            await _pageObject.DeleteEachDomainsOneByOne();
        }

        [Then(@"Verify cart is empty")]
        public async Task ThenVerifyCartIsEmpty()
        {
            string ObtainedResult = await _pageObject.ValidateShoppinCartIsEnty();
            Assert.IsEmpty(ObtainedResult); 
            
        }

        [Then(@"Close browser")]
        public async Task ThenCloseBrowser()
        {
           // await _pageObject.quit
        }


    }
}
