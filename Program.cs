using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using FormTests.Helpers;
class Program
{
    static void Main()
    {
        IWebDriver driver = new ChromeDriver();
        
        try 
        {
            driver.Navigate().GoToUrl("https://app.cloudqa.io/home/AutomationPracticeForm");
            driver.Manage().Window.Maximize();

            FormTestHelpers.EnterText(driver, By.Id("fname"), "Ojasvi");
            Console.WriteLine("First name test passed");

            FormTestHelpers.ClickRadio(driver, By.CssSelector("input[value='Female']"));
            Console.WriteLine("Gender test passed");

            var countryField = driver.FindElement(By.Id("country"));
            FormTestHelpers.ScrollToElement(driver, countryField);
            FormTestHelpers.EnterText(driver, By.Id("country"), "India");
            Console.WriteLine("Country test passed");

            Console.WriteLine("All tests passed!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Test failed: {ex.Message}");
        }
        finally 
        {
            driver.Quit();
        }
    }
}