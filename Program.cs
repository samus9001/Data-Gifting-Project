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
using System.Linq.Expressions;

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

            LogicMethods.waitForSignInElement(driver);

            await Task.Delay(1000);

            LogicMethods.waitForPasswordElement(driver);

            await Task.Delay(3000);

            LogicMethods.waitForAcceptCookies(driver);

            await Task.Delay(1000);

            LogicMethods.waitForDataGiftPage(driver);

            await Task.Delay(3000);

            LogicMethods.waitForDataGiftNumbers(driver);

            await Task.Delay(1000);

            LogicMethods.waitForDataGiftAmount(driver);

            await Task.Delay(1000);

            LogicMethods.waitForDataGiftConfirm(driver);

            driver.Quit();
        }
    }
}