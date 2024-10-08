﻿using System;
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
            driver.Navigate().GoToUrl(Options.URLs.loginURL);

			LogicMethods.signInAndStoreData(driver, Options.URLs.dataUsageURL, Options.URLs.jsonDataUsageURL);

			//GitfingInfoResponse = LogicMethods.parseReceivererJSONDataUsage(driver);

			//LogicMethods.giftData(driver);

			//driver.Quit();
		}
	}


}
