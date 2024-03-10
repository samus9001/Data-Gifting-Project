using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Xml.Linq;
using System.Web;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Security.Cryptography.X509Certificates;
using OpenQA.Selenium.Support.UI;
using DataGifting;

namespace DataGifting
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Instantiate a driver instance to control Chrome in headless mode
            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArguments("--headless=new");
            var driver = new ChromeDriver(chromeOptions);
            driver.Navigate().GoToUrl("https://id.ee.co.uk/");

            // Set WebDriver wait function to variable
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));

            try
            {

                Func<IWebDriver, bool> waitForSignInElement = new Func<IWebDriver, bool>((IWebDriver web) =>
                {
                    try
                    {
                        // Find the sign in input element
                        IWebElement inputElement = web.FindElement(By.Id("signInName"));

                        // Continue if the element is present
                        Console.WriteLine("\nSignInName Element is present, continue with further actions");

                        // Find the input elements
                        var elements = driver.FindElements(By.XPath("//input"));

                        // Input the provided keys for the first input element
                        elements[1].SendKeys("sameer99@outlook.com");


                        // Find the button element then click the button
                        driver.FindElement(By.XPath("//button")).Click();
                    }
                    catch
                    {
                        Console.WriteLine("\nSignInName input element is not present");
                        return false;
                    }

                    // Return true to indicate that the condition is met
                    return true;
                });

                // Wait until the element is present or timeout occurs
                wait.Until(waitForSignInElement);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            try
            {
                Func<IWebDriver, bool> waitForPasswordElement = new Func<IWebDriver, bool>((IWebDriver web) =>
                {
                    // Find the password input element
                    try
                    {
                        IWebElement inputElement = web.FindElement(By.Id("password"));

                        // Continue if the element is present
                        Console.WriteLine("\nPassword Element is present, continue with further actions");

                        // Find the input elements
                        var elements = driver.FindElements(By.XPath("//input"));

                        // Input the provided keys for the third input element
                        elements[2].SendKeys("D@tagifting2113");

                        // Find the button element then click the button
                        driver.FindElement(By.XPath("//button")).Click();
                    }
                    catch
                    {
                        Console.WriteLine("\nPassword input element is not present");
                        return false;
                    }

                    // Return true to indicate that the condition is met
                    return true;
                });

                wait.Until(waitForPasswordElement);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            await Task.Delay(5000);

            try
            {
                Func<IWebDriver, bool> waitForDataGift = new Func<IWebDriver, bool>((IWebDriver web) =>
                {

                    // Switch to the iframe (pop-up) only if the implicit wait has been set
                    driver.SwitchTo().Frame(0);

                    // Define the XPath of the element inside the iframe
                    string elementXPath = "//a[contains(text(), 'Accept all cookies')]";

                    // Locate the element inside the iframe and click on it
                    IWebElement element = driver.FindElement(By.XPath(elementXPath));
                    element.Click();

                    // Switch back to the main content
                    driver.SwitchTo().DefaultContent();

                    Thread.Sleep(2000);

                    var giftDataButton = driver.FindElement(By.XPath("//html/body/div[1]/div/div[2]/div[1]/div[1]/div/section/main/div[2]/div/div[1]/div/div[1]/div/div/div[2]/div[1]/a"));

                    giftDataButton.Click();

                    Thread.Sleep(2000);

                    var selectFromButton = driver.FindElement(By.XPath("/html/body/div[2]/div[5]/div/div[2]/div[2]/div/section[3]/section/div/main/div[1]/div/div[2]/section/div/div[1]/div/div/div[1]/span/span/span[1]/span/span[1]"));

                    selectFromButton.Click();

                    Thread.Sleep(2000);

                    var selectFromNumberButton = driver.FindElement(By.CssSelector("li.select2-results__option[title='07725917672']"));

                    selectFromNumberButton.Click();

                    Thread.Sleep(2000);

                    var selectToButton = driver.FindElement(By.XPath("/html/body/div[2]/div[5]/div/div[2]/div[2]/div/section[3]/section/div/main/div[1]/div/div[2]/section/div/div[2]/div/div/div[1]/span/span/span[1]/span/span[1]"));

                    selectFromButton.Click();

                    Thread.Sleep(2000);

                    var selectToNumberButton = driver.FindElement(By.CssSelector("li.select2-results__option.select2-results__option--highlighted[title='07753261456']"));

                    selectFromNumberButton.Click();

                    // Return true to indicate that the condition is met
                    return true;
                });

                wait.Until(waitForDataGift);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                //driver.Quit();
            }
        }
    }
}