using System;
using System.Buffers.Text;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using OpenQA.Selenium;
using System.Collections.Generic;
using OpenQA.Selenium.Support.UI;
using System.Security.Cryptography.X509Certificates;

namespace DataGifting
{
    public static class LogicMethods
    {

        /// <summary>
        /// set WebDriver wait function to driver variable
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static WebDriverWait CreateWebDriverWait(IWebDriver driver)
        {
            return new WebDriverWait(driver, TimeSpan.FromMinutes(1));
        }

        /// <summary>
        /// checks for username field then inputs credentials and submits
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static bool waitForSignInElement(IWebDriver driver)
        {
            try
            {
                WebDriverWait wait = CreateWebDriverWait(driver);

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

                return wait.Until(waitForSignInElement);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// checks for password field then inputs credentials and submits
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static bool waitForPasswordElement(IWebDriver driver)
        {
            try
            {
                WebDriverWait wait = CreateWebDriverWait(driver);

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
                return wait.Until(waitForPasswordElement);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// checks for cookies pop-up then accepts cookies
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static bool waitForAcceptCookies(IWebDriver driver)
        {
            try
            {
                WebDriverWait wait = CreateWebDriverWait(driver);

                Func<IWebDriver, bool> waitForAcceptCookies = new Func<IWebDriver, bool>((IWebDriver web) =>
                {
                    // Find the sign in input element
                    IWebElement frameElement = web.FindElement(By.Name("trustarc_cm"));

                    // Switch to the iframe (cookies pop-up)
                    driver.SwitchTo().Frame(0);

                    // Store element inside the iframe by text it contains
                    string acceptCookiesElementText = "//a[contains(text(), 'Accept all cookies')]";

                    // Locate the accept cookies element inside the iframe and click on it
                    IWebElement acceptCookiesElement = driver.FindElement(By.XPath(acceptCookiesElementText));

                    acceptCookiesElement.Click();

                    // Switch back to the main content
                    driver.SwitchTo().DefaultContent();

                    return true;
                });

                return wait.Until(waitForAcceptCookies);
            }

            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// navigates to the data gifting page
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static bool waitForDataGiftPage(IWebDriver driver)
        {
            try
            {
                WebDriverWait wait = CreateWebDriverWait(driver);

                Func<IWebDriver, bool> waitForDataGiftPage = new Func<IWebDriver, bool>((IWebDriver web) =>
                {

                    // Load the data gifting page
                    driver.Navigate().GoToUrl("https://ee.co.uk/plans-subscriptions/mobile/data-gifting/");

                    if (driver.Url == "https://ee.co.uk/plans-subscriptions/mobile/data-gifting/")
                    {
                        Console.WriteLine("\nSuccessfully loaded data gifting page");
                    }
                    else
                    {
                        Console.WriteLine("\n Failed to load data gifting page");
                    }
                    return true;
                });

                return wait.Until(waitForDataGiftPage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// selects the numbers being used for data gifting
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static bool waitForDataGiftNumbers(IWebDriver driver)
        {
            try
            {
                WebDriverWait wait = CreateWebDriverWait(driver);

                Func<IWebDriver, bool> waitForDataGiftNumbers = new Func<IWebDriver, bool>((IWebDriver web) =>
                {
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

                    return true;
                });

                return wait.Until(waitForDataGiftNumbers);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// selects the amount of data to gift
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static bool waitForDataGiftAmount(IWebDriver driver)
        {
            try
            {
                WebDriverWait wait = CreateWebDriverWait(driver);

                Func<IWebDriver, bool> waitForDataGiftAmount = new Func<IWebDriver, bool>((IWebDriver web) =>
                {
                    // Find the 5GB gift amount button
                    var giftDataAmount = driver.FindElement(By.XPath("//*[@id=\"main\"]/div[1]/div/div[4]/div[3]/div[4]/a"));

                    // Click the gift amount button
                    giftDataAmount.Click();

                    return true;
                });

                return wait.Until(waitForDataGiftAmount);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// selects the data gift confirmation
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static bool waitForDataGiftConfirm(IWebDriver driver)
        {
            try
            {
                WebDriverWait wait = CreateWebDriverWait(driver);

                Func<IWebDriver, bool> waitForDataGiftConfirm = new Func<IWebDriver, bool>((IWebDriver web) =>
                {

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
                    return true;
                });

                return wait.Until(waitForDataGiftConfirm);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
    }
}