using Microsoft.Playwright;
using System;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace AutomationSearchDomain.PageObjects
{
    public class SearchDomainPageObject : BasePageObject
    {
        public override string PagePath => "https://www.domain.com/";
        private static readonly string SearchTextPathLocator = "//section[@id='hero']//form/div[@class='domainSearch__inputWrapper ']/input[@name='search' and @type='text']";
        private static readonly string SearchButtonPathLocator = "//section[@id='hero']//button[@type='submit']";
        private static readonly string ListDomainLocator = "//*[@id='suggestions-wrapper']/div[2]/ul/li";
        private static readonly string DomainPurchaseLocator = "//div[5]/li/div[2]/div/div/select";
        private static readonly string TotalPurchaseLocator = "//span[@id='cart-total']";
        private static readonly string RemoveAllPurchase = "//div/span[@class='remove-cart-text']/i[@title='Remove from cart']";
        private static readonly string AddDomainToContinue = "//div[@id='shopping-cart']/div/a[@role='button']";

        public override IPage Page { get ; set ; }

        public override IBrowser Browser { get; }
        
        public SearchDomainPageObject(IBrowser browser)
        {
            Browser = browser;
        }
        
        public async Task FillInSearchTextFieldAsync(string SeartchText)
        {
            await Page.FillAsync(SearchTextPathLocator, SeartchText);
        }
        
        public async Task ClickOnSearchButton()
        {
            await Page.ClickAsync(SearchButtonPathLocator);
        }

        public async Task ClickOnCharacteristics(string Characteristics)
        {
            string Answer = "";
            var Counter = 0;
            bool LoadPage = false;
            var PageUtils = new Utils.Utils(Page);
            while (LoadPage.Equals(false) && Counter <= 30)
            {
                await Page.WaitForTimeoutAsync(1000);
                Counter++;
                if (await PageUtils.ExistLocator(ListDomainLocator))
                {
                   
                    ILocator ListFile = Page.Locator(ListDomainLocator);
                    for (int i = 1; i <= await ListFile.CountAsync(); i++)
                    {
                        //Console.WriteLine(i);
                        string TextSaleLocator = "//*[@id='suggestions-wrapper']/div[@class='panel-body suggestions-body']/ul/li[" + i + "]/div[2]";
                        string TextDomainLocator = "//*[@id='suggestions-wrapper']/div[@class='panel-body suggestions-body']/ul/li[" + i + "]/div[1]";


                        //Console.WriteLine("Sugerencia1 esta en la posicion[" + i + "]: " + await Page.InnerTextAsync(TextSaleLocator));
                        string TextColum = await Page.InnerTextAsync(TextSaleLocator);
                        if (TextColum.Contains(Characteristics))
                        {
                            //Console.WriteLine("Sugerencia1 esta en la posicion: " + i);
                            String AddToCart= "//*[@id='suggestions-wrapper']/div[@class='panel-body suggestions-body']/ul/li[" + i + "]/div[4]";
                            await Page.ClickAsync(AddToCart);
                            Answer =Answer + await Page.InnerTextAsync(TextDomainLocator) + ";";
                            break;
                        }
                        

                    }
                    
                    LoadPage = true;
                    break;
                }
            }
            Console.WriteLine("Add Cart: " + Answer);
        }

        public async Task ChangeOneDomainFor(string Time)
        {
            await Page.Locator(DomainPurchaseLocator).SelectOptionAsync(new[] { new SelectOptionValue() { Label = Time } });  
        }

        public async Task<string> TotalPurcheses()
        {
            string Answer = "";
            var Counter = 0;
            bool LoadPage = false;
            var PageUtils = new Utils.Utils(Page);
            while (LoadPage.Equals(false) && Counter <= 30)
            {
                await Page.WaitForTimeoutAsync(1000);
                Counter++;
                if (await PageUtils.ExistLocator(TotalPurchaseLocator))
                {
                    Answer = await Page.InnerTextAsync(TotalPurchaseLocator);
                    LoadPage = true;
                    break;
                }
            }
            Console.WriteLine("Total: " + Answer); 
            
            return Answer;
        }

        public async Task DeleteEachDomainsOneByOne()
        { 
            var Counter = 0;
            bool LoadPage = false;
            var PageUtils = new Utils.Utils(Page);
            while (LoadPage.Equals(false) && Counter <= 30)
            {
                await Page.WaitForTimeoutAsync(1000);
                Counter++;
                if (await PageUtils.ExistLocator(RemoveAllPurchase))
                {
                    await Page.ClickAsync(RemoveAllPurchase);
                    LoadPage = true;
                    break;
                }
            }

            Console.WriteLine("Delete all domain ... " );
        }
        public async Task<string> ValidateShoppinCartIsEnty()
        {
            string Answer = "";
            var Counter = 0;
            bool LoadPage = false;
            var PageUtils = new Utils.Utils(Page);
            while (LoadPage.Equals(false) && Counter <= 30)
            {
                await Page.WaitForTimeoutAsync(1000);
                Counter++;
                if (await PageUtils.ExistLocator(AddDomainToContinue))
                {
                    Answer = "";
                }
                else { Answer = "Is not empty"; }
                LoadPage = true;
                break;
            }

            Console.WriteLine("Shoppin Cart domain "+Answer);

            return Answer;
        }
        
    }
}
