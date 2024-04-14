﻿using System;
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
using Newtonsoft.Json.Linq;
using SeleniumExtras.WaitHelpers;
using Newtonsoft.Json;

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
						IWebElement inputElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("signInName")));

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
						IWebElement inputElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("password")));

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
					IWebElement frameElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Name("trustarc_cm")));

					// Switch to the iframe (cookies pop-up)
					driver.SwitchTo().Frame(0);

					// Store element inside the iframe by text it contains
					string acceptCookiesElementText = "//a[contains(text(), 'Accept all cookies')]";

					// Locate the accept cookies element inside the iframe and click on it
					IWebElement acceptCookiesElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(acceptCookiesElementText)));

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
		/// navigates to the data usage page and extracts JSON data
		/// </summary>
		/// <param name="driver"></param>
		/// <returns></returns>
		public static bool waitForDataUsagePage(IWebDriver driver)
		{
			try
			{
				WebDriverWait wait = CreateWebDriverWait(driver);

				Func<IWebDriver, bool> waitForDataUsagePage = new Func<IWebDriver, bool>((IWebDriver web) =>
				{
					// Navigate to the URL
					driver.Navigate().GoToUrl("https://ee.co.uk/plans-subscriptions/mobile");

					wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlMatches("https://ee.co.uk/plans-subscriptions/mobile"));

					if (driver.Url == "https://ee.co.uk/plans-subscriptions/mobile")
					{
						Console.WriteLine("\nSuccessfully loaded data usage page");
					}
					else
					{
						Console.WriteLine("\nFailed to load data usage page");
					}
					return true;
				});

				return wait.Until(waitForDataUsagePage);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred: {ex.Message}");
				return false;
			}
		}

		public static bool waitForSenderDataUsagePage(IWebDriver driver)
		{
			try
			{
				WebDriverWait wait = CreateWebDriverWait(driver);

				Func<IWebDriver, bool> waitForSenderDataUsagePage = new Func<IWebDriver, bool>((IWebDriver web) =>
				{
					// Find the Choose a device dropdown element
					var selectDeviceDropdownButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("lxp-DropdownOption__wrapper__text-wrapper")));

					// Click the choose a device dropdown element button 
					selectDeviceDropdownButton.Click();

					var selectNumberButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"react-select-2-option-0\"]")));

					// Click the phone number button
					selectNumberButton.Click();

					return true;
				});

				return wait.Until(waitForSenderDataUsagePage);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred: {ex.Message}");
				return false;
			}
		}

		public static bool waitForSenderDataUsageJSONPage(IWebDriver driver)
		{
			try
			{
				WebDriverWait wait = CreateWebDriverWait(driver);

				Func<IWebDriver, bool> waitForSenderDataUsageJSONPage = new Func<IWebDriver, bool>((IWebDriver web) =>
				{
					// Navigate to the plain text URL
					driver.Navigate().GoToUrl("https://ee.co.uk/app/api/usage-details");

					wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlMatches("https://ee.co.uk/app/api/usage-details"));

					return true;
				});

				return wait.Until(waitForSenderDataUsageJSONPage);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred: {ex.Message}");
				return false;
			}
		}

		/// <summary>
		/// Store the sender usage data into a JObject
		/// </summary>
		/// <param name="driver"></param>
		/// <returns></returns>
		public static bool parseSenderJSONDataUsage(IWebDriver driver)
		{
			try
			{
				WebDriverWait wait = CreateWebDriverWait(driver);

				Func<IWebDriver, bool> ParseJSONDataUsage = new Func<IWebDriver, bool>((IWebDriver web) =>
				{
					IWebElement jsonElement = driver.FindElement(By.TagName("pre"));

					string jsonString = jsonElement.Text;

					Console.WriteLine(jsonString);

					// Parse the JSON string into a JObject
					JObject jsonUsageDataObject = JObject.Parse(jsonString);

					// Deserialized function
					RemoteSenderPlanDetails deserialized = JsonConvert.DeserializeObject<RemoteSenderPlanDetails>(jsonString);

					// Create a PlanDetails object to store the desired values
					SenderPlanDetails senderPlanDetails = deserialized.ExtractSenderPlanDetails();

					Console.WriteLine($"Allowance Used: {senderPlanDetails.AllowanceUsed}");

					// Create a PhoneDetails object to store the desired values
					SenderPhoneDetails senderPhoneDetails = deserialized.ExtractSenderPhoneDetails();

					Console.WriteLine($"Device name: {senderPhoneDetails.DeviceName}");

					return true;
				});

				return wait.Until(ParseJSONDataUsage);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred: {ex.Message}");
				return false;
			}
		}

		public static bool waitForReceiverDataUsagePage(IWebDriver driver)
		{
			try
			{
				WebDriverWait wait = CreateWebDriverWait(driver);

				Func<IWebDriver, bool> waitForReceiverDataUsagePage = new Func<IWebDriver, bool>((IWebDriver web) =>
				{
					// Find the Choose a device dropdown element
					var selectDeviceDropdownButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("lxp-DropdownOption__wrapper__text-wrapper")));

					// Click the choose a device dropdown element button 
					selectDeviceDropdownButton.Click();

					var selectNumberButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"react-select-2-option-1\"]")));

					// Click the phone number button
					selectNumberButton.Click();

					return true;
				});

				return wait.Until(waitForSenderDataUsagePage);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred: {ex.Message}");
				return false;
			}
		}

		public static bool waitForReceiverDataUsageJSONPage(IWebDriver driver)
		{
			try
			{
				WebDriverWait wait = CreateWebDriverWait(driver);

				Func<IWebDriver, bool> waitForReceiverDataUsageJSONPage = new Func<IWebDriver, bool>((IWebDriver web) =>
				{
					// Navigate to the plain text URL
					driver.Navigate().GoToUrl("https://ee.co.uk/app/api/usage-details");

					wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlMatches("https://ee.co.uk/app/api/usage-details"));

					return true;
				});

				return wait.Until(waitForReceiverDataUsageJSONPage);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred: {ex.Message}");
				return false;
			}
		}

		public static bool parseReceivererJSONDataUsage(IWebDriver driver)
		{
			try
			{
				WebDriverWait wait = CreateWebDriverWait(driver);

				Func<IWebDriver, bool> ParseJSONDataUsage = new Func<IWebDriver, bool>((IWebDriver web) =>
				{
					IWebElement jsonElement = driver.FindElement(By.TagName("pre"));

					string jsonString = jsonElement.Text;

					Console.WriteLine(jsonString);

					// Parse the JSON string into a JObject
					JObject jsonUsageDataObject = JObject.Parse(jsonString);

					// Deserialized function
					RemoteReceiverPlanDetails deserialized = JsonConvert.DeserializeObject<RemoteReceiverPlanDetails>(jsonString);

					// Create a PlanDetails object to store the desired values
					ReceiverPlanDetails receiverPlanDetails = deserialized.ExtractReceiverPlanDetails();

					Console.WriteLine($"Allowance Used: {receiverPlanDetails.AllowanceUsed}");

					// Create a PhoneDetails object to store the desired values
					ReceiverPhoneDetails receiverPhoneDetails = deserialized.ExtractReceiverPhoneDetails();

					Console.WriteLine($"Device name: {receiverPhoneDetails.DeviceName}");

					return true;
				});

				return wait.Until(ParseJSONDataUsage);
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
					driver.Navigate().GoToUrl("https://ee.co.uk/plans-subscriptions/mobile/data-gifting");

					wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlMatches("https://ee.co.uk/plans-subscriptions/mobile/data-gifting"));

					if (driver.Url == "https://ee.co.uk/plans-subscriptions/mobile/data-gifting")
					{
						Console.WriteLine("\nSuccessfully loaded data gifting page");
						return true;
					}
					else
					{
						Console.WriteLine("\n Failed to load data gifting page");
						return false;
					}
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
					var findFromButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("select2-supplierMsisdn-container")));

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
					var giftDataAmount = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"main\"]/div[1]/div/div[4]/div[3]/div[4]/a")));

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
					var giftData = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"main\"]/div[1]/div/div[4]/div[4]/div[2]/button")));

					// Click the Gift Data button
					giftData.Click();

					// Find the Gift Data confirmation button
					var confirmGiftDataButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"main\"]/div[1]/div/div[5]/div[2]/form/button[1]")));

					// Click the Gift Data confirmation button
					confirmGiftDataButton.Click();

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