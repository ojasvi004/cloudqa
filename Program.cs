using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using FormTests.Helpers;

class Program
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

            Logger.Log("Starting First Name test");
            FormTestHelpers.EnterText(driver, By.Id("fname"), "Ojasvi");
            Logger.Log("First name entered successfully", "SUCCESS");

            Logger.Log("Starting Gender selection test");
            FormTestHelpers.ClickRadio(driver, By.CssSelector("input[value='Female']"));
            Logger.Log("Gender selected successfully", "SUCCESS");

            Logger.Log("Starting Country field test");
            var countryField = driver.FindElement(By.Id("country"));
            FormTestHelpers.ScrollToElement(driver, countryField);
            FormTestHelpers.EnterText(driver, By.Id("country"), "India");
            Logger.Log("Country entered successfully", "SUCCESS");

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
}