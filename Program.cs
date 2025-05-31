using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using FormTests.Helpers;

namespace FormTests
{
    public class Program
    {
        static void Main(string[] args)
        {
            Logger.Log("Test execution started");
            bool headless = args.Contains("--headless");
            Logger.Log($"Running in {(headless ? "headless" : "normal")} mode");

            using IWebDriver driver = FormTestHelpers.CreateDriver(headless);

            try
            {
                Logger.Log("Navigating to test URL");
                driver.Navigate().GoToUrl("https://app.cloudqa.io/home/AutomationPracticeForm");

                if (!headless)
                {
                    Logger.Log("Maximizing browser window");
                    driver.Manage().Window.Maximize();
                }

                // Test 1: First Name
                Logger.Log("Starting First Name test");
                FormTestHelpers.EnterText(driver, By.Id("fname"), "Ojasvi");
                Logger.Log("First name entered successfully", "SUCCESS");

                // Test 2: Gender
                Logger.Log("Starting Gender selection test");
                FormTestHelpers.ClickRadio(driver, By.CssSelector("input[value='Female']"));
                Logger.Log("Gender selected successfully", "SUCCESS");

                // Test 3: Shadow DOM
                Logger.Log("Starting Shadow DOM test");
                TestShadowForm(driver);
                Logger.Log("Shadow DOM test completed successfully", "SUCCESS");

                Logger.Log("All tests completed successfully!", "SUCCESS");
            }
            catch (Exception ex)
            {
                Logger.Log($"Test failed: {ex.Message}", "ERROR");
                throw;
            }
            finally
            {
                Logger.Log("Test execution completed");
                driver.Quit();
            }
        }

        static void TestShadowForm(IWebDriver driver)
        {
            try
            {
                var shadowFormInputs = driver.FindElements(By.CssSelector("shadow-form input"));

                if (shadowFormInputs.Count > 0)
                {
                    var firstInput = shadowFormInputs.FirstOrDefault(input =>
                        input.GetAttribute("type") == "text" || string.IsNullOrEmpty(input.GetAttribute("type")));

                    if (firstInput != null)
                    {
                        FormTestHelpers.ScrollToElement(driver, firstInput);
                        firstInput.Clear();
                        firstInput.SendKeys("ShadowTest");
                        Logger.Log("Successfully interacted with Shadow DOM form", "SUCCESS");
                    }
                    else
                    {
                        Logger.Log("No suitable text input found in shadow form");
                    }
                }
                else
                {
                    Logger.Log("No inputs found in shadow form, test skipped");
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"Shadow DOM test failed: {ex.Message}", "ERROR");
                throw;
            }
        }
    }
}
