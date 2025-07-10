using OpenQA.Selenium;

namespace xUnitTestAutomation.Pages
{
    public interface IProductsPage
    {
        bool IsShopingCartDisplayed();
        void AddProductToCart(string productName);
        void GoToCart();
    }

    public class ProductsPage(IDriverWait driver) : IProductsPage
    {
        private IWebElement shoppingCart => driver.FindElement(By.CssSelector("a[class='shopping_cart_link']"));
        private IWebElement shoppingCartLink => driver.FindElement(By.CssSelector(".shopping_cart_link"));

        public bool IsShopingCartDisplayed() => shoppingCart.Displayed;

        public void AddProductToCart(string productName) 
        {
            driver.FindElement(By.Id($"add-to-cart-{productName}")).Click();
        }

        public void GoToCart() => shoppingCartLink.Click();

    }
}
