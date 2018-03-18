using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace FigureSearch
{
	class SeleniumOperation
	{
		public static IWebDriver CreateInstance(SeleniumBrowers.Name browserName, bool headless = false)
		{
			switch (browserName)
			{
				case SeleniumBrowers.Name.Chrome:

                    // ヘッドレス(ブラウザの非表示)モードで起動するかどうか
                    if (headless)
                    {
                        ChromeOptions chromeOptions = new ChromeOptions();
                        chromeOptions.AddArguments(new List<string>() { "headless" });
                        return new ChromeDriver(chromeOptions);
                    }
                    else
                        return new ChromeDriver();

                case SeleniumBrowers.Name.Firefox:
					FirefoxDriverService driverService = FirefoxDriverService.CreateDefaultService();
					driverService.FirefoxBinaryPath = @"D:\Softs\Mozilla Firefox\firefox.exe";
					driverService.HideCommandPromptWindow = true;
					driverService.SuppressInitialDiagnosticInformation = true;
					return new FirefoxDriver(driverService);

				case SeleniumBrowers.Name.InternetExplorer:
					return new InternetExplorerDriver();

				default:
					throw new ArgumentException(string.Format("Please Definition BrowserName:{0}", browserName));
			}
		}


	}
}
