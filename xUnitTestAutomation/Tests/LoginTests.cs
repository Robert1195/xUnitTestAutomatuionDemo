using xUnitTestAutomation.Pages;
using EaFramework.Reporting;

namespace xUnitTestAutomation.Tests
{
    [Collection("SharedReport")]
    public class LoginTests(IHomePage homePage, IProductsPage productsPage, SharedReportFixture fixture)
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
                productsPage.IsShopingCartDisplayed();

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
    }
}
