using OpenQA.Selenium;
using System;

namespace FormTests.Helpers

{
    public static class FormTestHelpers
    {
        public static void EnterText(IWebDriver driver, By locator, string text)
        {
            var element = driver.FindElement(locator);
            element.Clear();
            element.SendKeys(text);
            
            if (element.GetAttribute("value") != text)
                throw new Exception($"Failed to enter text in element located by {locator}");
        }

        public static void ClickRadio(IWebDriver driver, By locator)
        {
            var radio = driver.FindElement(locator);
            radio.Click();
            
            if (!radio.Selected)
                throw new Exception($"Radio button not selected: {locator}");
        }

        public static void WaitForElement(IWebDriver driver, By locator, int seconds = 5)
        {
            var wait = new System.TimeSpan(0, 0, seconds);
            var now = DateTime.Now;
            
            while (DateTime.Now - now < wait)
            {
                try 
                {
                    if (driver.FindElement(locator).Displayed)
                        return;
                }
                catch {}
                
                System.Threading.Thread.Sleep(500);
            }
            
            throw new Exception($"Element not found: {locator}");
        }

        public static void ScrollToElement(IWebDriver driver, IWebElement element)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].scrollIntoView(true);", element);
            System.Threading.Thread.Sleep(500);
        }
    }
}