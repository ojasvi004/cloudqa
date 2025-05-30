using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

class Program
{
    static void Main()
    {
        IWebDriver driver = new ChromeDriver();
        
        try 
        {
            driver.Navigate().GoToUrl("https://app.cloudqa.io/home/AutomationPracticeForm");
            driver.Manage().Window.Maximize();
            
            TestFirstName(driver);
            
            TestGender(driver);
            
            TestCountry(driver);
            
            Console.WriteLine("All tests passed successfully!");
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

    static void TestFirstName(IWebDriver driver)
    {
        var firstName = driver.FindElement(By.Id("fname"));
        firstName.Clear();
        firstName.SendKeys("Ojasvi");
        
        if(firstName.GetAttribute("value") != "Ojasvi")
            throw new Exception("First name test failed");
        
        Console.WriteLine("First name test passed");
    }

    static void TestGender(IWebDriver driver)
    {
        var femaleRadio = driver.FindElement(By.CssSelector("input[type='radio'][value='Female']"));
        femaleRadio.Click();
        
        if(!femaleRadio.Selected)
            throw new Exception("Gender test failed");
            
        Console.WriteLine("Gender test passed");
    }

    static void TestCountry(IWebDriver driver)
    {
        var country = driver.FindElement(By.Id("country"));
        country.Clear();
        country.SendKeys("India");
        
        if(country.GetAttribute("value") != "India")
            throw new Exception("Country test failed");
            
        Console.WriteLine("Country test passed");
    }
}