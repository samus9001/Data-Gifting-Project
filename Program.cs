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

                        if (inputElement != null)
                        {
                            Console.WriteLine("\nSignInName Element is present");
                        }

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

            await Task.Delay(5000);

            try
            {
                Func<IWebDriver, bool> waitForPasswordElement = new Func<IWebDriver, bool>((IWebDriver web) =>
                {
                    // Find the password input element
                    try
                    {
                        IWebElement inputElement = web.FindElement(By.Id("password"));

                        if (inputElement != null)
                        {
                            Console.WriteLine("\nPassword Element is present");
                        }

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
                    // Find the sign in input element
                    IWebElement frameElement = web.FindElement(By.Name("trustarc_cm"));

                    // Switch to the iframe (cookies pop-up)
                    driver.SwitchTo().Frame(0);

                    // Store element inside the iframe by text it contains
                    string acceptCookiesElementText = "//a[contains(text(), 'Accept all cookies')]";

                    // Locate the element inside the iframe and click on it
                    IWebElement acceptCookiesElement = driver.FindElement(By.XPath(acceptCookiesElementText));

                    acceptCookiesElement.Click();

                    // Switch back to the main content
                    driver.SwitchTo().DefaultContent();

                    // Load the data gifting page
                    driver.Navigate().GoToUrl("https://ee.co.uk/plans-subscriptions/mobile/data-gifting/");

                    if (driver.Url == "https://ee.co.uk/plans-subscriptions/mobile/data-gifting/")
                    {
                        Console.WriteLine("\nSuccessfully navigated to data gifting page");
                    }

                    // Find the send From dropdown button
                    var findFromButton = driver.FindElement(By.Id("select2-supplierMsisdn-container"));

                    // Click the send From dropdown button
                    findFromButton.Click();

                    // Find the send from phone number element button
                    var selectFromNumberButton = driver.FindElement(By.CssSelector("li.select2-results__option[title='07725917672']"));

                    // Click the phone number button
                    selectFromNumberButton.Click();

                    // Find the send To dropdown button
                    var findToButton = driver.FindElement(By.Id("select2-consumerMsisdn-container"));

                    // Click the send To dropdown button
                    findToButton.Click();

                    // Find the send to phone number element button
                    var selectToNumberButton = driver.FindElement(By.CssSelector("li.select2-results__option[title='07753261456']"));

                    // Click the phone number button
                    selectToNumberButton.Click();

                    // Find the 5GB gift amount button
                    var giftDataAmount = driver.FindElement(By.XPath("//*[@id=\"main\"]/div[1]/div/div[4]/div[3]/div[4]/a"));

                    // Click the gift amount button
                    giftDataAmount.Click();

                    // Find the Gift Data button
                    var giftData = driver.FindElement(By.XPath("//*[@id=\"main\"]/div[1]/div/div[4]/div[4]/div[2]/button"));

                    // Click the Gift Data button
                    giftData.Click();

                    // Find the Gift Data confirmation button
                    var confirmGiftDataButton = driver.FindElement(By.XPath("//*[@id=\"main\"]/div[1]/div/div[5]/div[2]/form/button[1]"));

                    // Click the Gift Data confirmation button
                    //confirmGiftDataButton.Click();
                    if (confirmGiftDataButton != null)
                    {
                        Console.WriteLine("successfully gifted data!");
                    }

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
                driver.Quit();
            }
        }
    }
}