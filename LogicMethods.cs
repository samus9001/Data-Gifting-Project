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
		public static bool signInAndStoreData(IWebDriver driver, string dataUsageURL, string jsonDataUsageURL)
		{
			try
			{
				WebDriverWait wait = CreateWebDriverWait(driver);

				Func<IWebDriver, bool> signInAndStoreData = new Func<IWebDriver, bool>((IWebDriver web) =>
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
						//elements[1].SendKeys(""); input username

						// Find the button element then click the button
						driver.FindElement(By.XPath("//button")).Click();
					}
					catch
					{
						Console.WriteLine("\nSignInName input element is not present");
						return false;
					}

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
						elements[2].SendKeys(""); //input password

						// Find the button element then click the button
						driver.FindElement(By.XPath("//button")).Click();
					}
					catch
					{
						Console.WriteLine("\nPassword input element is not present");
						return false;
					}

					try
					{
						// Find the cookies pop-up element
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
					}
					catch
					{
						Console.WriteLine("\nCookies pop-up is not present");
						return false;
					}

					try
					{
						// Navigate to the data usage page URL
						driver.Navigate().GoToUrl(dataUsageURL);

						wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlMatches(dataUsageURL));

						if (driver.Url == (Options.URLs.dataUsageURL))
						{
							Console.WriteLine("\nSuccessfully loaded data usage page");
						}
						else
						{
							Console.WriteLine("\nFailed to load data usage page");
						}
					}
					catch
					{
						Console.WriteLine("\nData usage URL could not be loaded");
						return false;
					}

					try
					{
						// Navigate to the plain text URL
						driver.Navigate().GoToUrl(jsonDataUsageURL);

						wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlMatches(jsonDataUsageURL));
					}
					catch
					{
						Console.WriteLine("\nData usage plain text URL could not be loaded");
						return false;
					}

					try
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
					}
					catch
					{
						Console.WriteLine($"\nJSON data could not be parsed");
						return false;
					}

					try
					{
						// Navigate to the data usage page URL
						driver.Navigate().GoToUrl(dataUsageURL);

						wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlMatches(dataUsageURL));

						if (driver.Url == "https://ee.co.uk/plans-subscriptions/mobile")
						{
							Console.WriteLine("\nSuccessfully loaded data usage page");
						}
						else
						{
							Console.WriteLine("\nFailed to load data usage page");
						}
					}
					catch
					{
						Console.WriteLine("\nData usage URL could not be loaded");
						return false;
					}

					try
					{
						// Find the Choose a device dropdown element
						var selectDeviceDropdownButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("lxp-DropdownOption__wrapper__text-wrapper")));

						// Click the choose a device dropdown element button 
						selectDeviceDropdownButton.Click();

						var selectNumberButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"react-select-2-option-1\"]")));

						// Click the phone number button
						selectNumberButton.Click();
					}
					catch
					{
						Console.WriteLine("\nCould not change device selection");
						return false;
					}

					try
					{
						var deviceDropdownElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("lxp-DropdownOption__wrapper__text-wrapper")));

						// Navigate to the plain text URL
						driver.Navigate().GoToUrl(jsonDataUsageURL);

						wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlMatches(jsonDataUsageURL));
					}
					catch
					{
						Console.WriteLine("\nData usage plain text URL could not be loaded");
						return false;
					}

					try
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
					}
					catch
					{
						Console.WriteLine("\nJSON data could not be parsed");
						return false;
					}

					// Return true to indicate that the condition is met
					return true;
				});

				return wait.Until(signInAndStoreData);
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
		public static bool giftData(IWebDriver driver, string dataGiftingURL)
		{
			try
			{
				WebDriverWait wait = CreateWebDriverWait(driver);

				Func<IWebDriver, bool> giftData = new Func<IWebDriver, bool>((IWebDriver web) =>
				{
					try
					{

						// Load the data gifting page
						driver.Navigate().GoToUrl(dataGiftingURL);

						wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlMatches(dataGiftingURL));

						if (driver.Url == (Options.URLs.dataGiftingURL))
						{
							Console.WriteLine("\nSuccessfully loaded data gifting page");
						}
						else
						{
							Console.WriteLine("\nFailed to load data gifting page");
						}
					}
					catch
					{
						Console.WriteLine("\nCould not load data gifting page");
						return false;
					}

					try
					{
						// Find the send From dropdown button
						var findFromButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("select2-supplierMsisdn-container")));

						// Click the send From dropdown button
						findFromButton.Click();

						// Find the send from phone number element button
						//var selectFromNumberButton = driver.FindElement(By.CssSelector("li.select2-results__option[title='']")); input phone number

						// Click the phone number button
						//selectFromNumberButton.Click();

						// Find the send To dropdown button
						var findToButton = driver.FindElement(By.Id("select2-consumerMsisdn-container"));

						// Click the send To dropdown button
						findToButton.Click();

						// Find the send to phone number element button
						//var selectToNumberButton = driver.FindElement(By.CssSelector("li.select2-results__option[title='']")); input phone number

						// Click the phone number button
						//selectToNumberButton.Click();
					}
					catch
					{
						Console.WriteLine("\nCould not select sender/receiver number}");
						return false;
					}

					try
					{
						// Find the 5GB gift amount button
						var giftDataAmount = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"main\"]/div[1]/div/div[4]/div[3]/div[4]/a")));

						// Click the gift amount button
						giftDataAmount.Click();
					}
					catch
					{
						Console.WriteLine("\nCould not select data amount to gift");
						return false;
					}

					try
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
							Console.WriteLine("Successfully gifted data!");
						}
					}
					catch
					{
						Console.WriteLine("\nCould nould select Gift Data button");
						return false;
					}
					// Return true to indicate that the condition is met
					return true;
				});

				return wait.Until(giftData);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred: {ex.Message}");
				return false;
			}

			//public static bool selectSenderDataDropdown(IWebDriver driver)
			//{
			//	try
			//	{
			//		WebDriverWait wait = CreateWebDriverWait(driver);

			//		Func<IWebDriver, bool> selectSenderDataDropdown = new Func<IWebDriver, bool>((IWebDriver web) =>
			//		{
			//			// Find the Choose a device dropdown element
			//			var selectDeviceDropdownButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("lxp-DropdownOption__wrapper__text-wrapper")));

			//			// Click the choose a device dropdown element button 
			//			selectDeviceDropdownButton.Click();

			//			var selectNumberButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"react-select-2-option-0\"]")));

			//			// Click the phone number button
			//			selectNumberButton.Click();

			//			return true;
			//		});

			//		return wait.Until(selectSenderDataDropdown);
			//	}
			//	catch (Exception ex)
			//	{
			//		Console.WriteLine($"An error occurred: {ex.Message}");
			//		return false;
			//	}
			//}

		}
	}
}