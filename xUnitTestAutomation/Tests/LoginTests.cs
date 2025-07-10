using xUnitTestAutomation.Pages;
using EaFramework.Reporting;
using FluentAssertions;

namespace xUnitTestAutomation.Tests
{
    [Collection("SharedReport")]
    public class LoginTests(IHomePage homePage, IProductsPage productsPage, ICartPage cartPage, SharedReportFixture fixture)
    {
        private readonly IReportManager _reportManager = fixture.ReportManager;

        [Theory]
        [InlineData("standard_user", "secret_sauce")]
        public void Test1(string username, string password)
        {
            _reportManager.StartTest("Test1", "Login test with valid credentials");

            try
            {
                homePage.Login(username, password);
                productsPage.IsShopingCartDisplayed().Should().BeTrue();

                _reportManager.Pass("Test completed successfully");
            }
            catch (Exception ex)
            {
                _reportManager.Fail("Test failed");
                Assert.Fail(ex.ToString());
            }

        }

        [Theory]
        [InlineData("standard_user", "Invalid_password")]
        public void Test2(string username, string password)
        {
            _reportManager.StartTest("Test2", "Login test with invalid password");

            try
            {
                homePage.Login(username, password);
                Assert.Multiple(() =>
                {
                    Assert.True(homePage.isErrorMsgDisplayed(), "Error message is not displayed.");
                    Assert.True(homePage.VerifyErrorMsgTxt(), "Error message text is incorrect.");
                });

                _reportManager.Pass("Test completed successfully");
            }
            catch (Exception ex)
            {
                _reportManager.Fail("Test failed");
                Assert.Fail(ex.ToString());
            }

        }

        [Theory]
        [InlineData("standard_user", "secret_sauce", "sauce-labs-backpack")]
        public void Test3(string username, string password, string productName)
        {
            _reportManager.StartTest("Test3", "Verify if item has been added to the cart");

            try
            {
                homePage.Login(username, password);
                productsPage.AddProductToCart(productName);
                productsPage.GoToCart();
                cartPage.VerifyIfItemIsAddedToCart().Should().BeTrue();
                Thread.Sleep(3000);

                _reportManager.Pass("Test completed successfully");
            }
            catch (Exception ex)
            {
                _reportManager.Fail("Test failed");
                Assert.Fail(ex.ToString());
            }

        }
    }
}
