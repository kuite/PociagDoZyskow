using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Support.UI;

namespace PociagDoZyskow.ExternalDataReader.Helpers
{
    public static class SeleniumHelper
    {
        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }
            return driver.FindElement(by);
        }
    }
}
