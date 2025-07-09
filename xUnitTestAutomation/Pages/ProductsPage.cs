using OpenQA.Selenium;

namespace xUnitTestAutomation.Pages
{
    public interface IProductsPage
    {
        bool IsShopingCartDisplayed();
    }

    public class ProductsPage(IDriverWait driver) : IProductsPage
    {
        private IWebElement shoppingCart => driver.FindElement(By.CssSelector("a[class='shopping_cart_link']"));

        public bool IsShopingCartDisplayed() => shoppingCart.Displayed;
    }
}
