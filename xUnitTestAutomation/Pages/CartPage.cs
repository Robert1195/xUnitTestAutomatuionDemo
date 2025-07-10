using OpenQA.Selenium;

namespace xUnitTestAutomation.Pages
{
    public interface ICartPage
    {
        bool VerifyIfItemIsAddedToCart();
    }

    public class CartPage(IDriverWait driver) : ICartPage
    {
        //private IWebElement cartItem => driver.FindElement(By.CssSelector(".cart_item"));

        public bool VerifyIfItemIsAddedToCart() 
        {
            IWebElement cartItem = driver.FindElement(By.CssSelector(".cart_item"));
            return cartItem.Displayed;
        } 
    }
}
