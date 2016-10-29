using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System.Threading;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private IWebDriver driver = new FirefoxDriver();
        private string baseURL = "http://testwisely.com/demo";
        

        [TestMethod]
        public void Helloworld()
        {
            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.LinkText("Hello World!")).Click();  
            Assert.AreEqual("Demo - TestWisely", driver.Title);
            driver.Quit();
        }

        [TestMethod]
        public void netbank()
        {
            driver.Navigate().GoToUrl(baseURL + "/netbank");
            Assert.AreEqual("NetBank", driver.Title);
            IWebElement list = driver.FindElement(By.Name("account"));
            SelectElement select = new SelectElement(list);
            select.SelectByText("Savings");
            driver.FindElement(By.Id("rcptAmount")).SendKeys("" + RandomNumber(0 , 1000));
            driver.FindElement(By.Id("transfer_btn")).Click();            
            for (int second = 0; ; second++)
            {
                 if (second >= 11) Assert.Fail("timeout");
                 try
                 {
                    if (IsElementPresent(By.Id("receipt"))) break;
                 }
                 catch (Exception)
                 { }
                 Thread.Sleep(1000);
             }
           /* driver.FindElement(By.Id("receiptNo"));         dobavi pisane v fail/baza
            driver.FindElement(By.Id("date"));           
            */
            }

        [TestMethod]
        public void survey()
        {
            driver.Navigate().GoToUrl(baseURL + "/survey");
            Assert.AreEqual("Demo - Survey - TestWisely", driver.Title);
            IWebElement list = driver.FindElement(By.Id("your_role"));
            SelectElement select = new SelectElement(list);
            select.SelectByText("Tester");                                 
            driver.FindElement(By.XPath("(//input[@name='role'])[2]")).Click();
            driver.FindElement(By.XPath("(//input[@name='run_regression_tests'])[3]")).Click();
            driver.FindElement(By.XPath("(//input[@name='manager_support'])[1]")).Click();
            driver.FindElement(By.CssSelector("input[type='button']")).Click();                     //proverka jscript kakvo pyska ???
            driver.FindElement(By.PartialLinkText("TestWise Recorder")).Click();
            driver.FindElement(By.XPath("//a[contains(@href, '/')]")).Click();                      // ne izpulnqva "/"
            
            //Assert.AreEqual("Test software  wisely with AgileWay", driver.Title);
            driver.Quit();
        }
        

        [TestMethod]
        public void popup()
        {
            driver.Navigate().GoToUrl(baseURL + "/popups");
            Assert.AreEqual("Demo - Popups - TestWisely", driver.Title);
            driver.FindElement(By.Id("buy_now_btn")).Click();
            IAlert a = driver.SwitchTo().Alert();
            if (a.Text.Equals("Are you sure?"))
            {
                a.Accept();
            }
            Thread.Sleep(1000);
            Assert.AreEqual(baseURL, driver.Url);
            driver.Quit();
        }

        [TestMethod]
        public void table()
        {
            driver.Navigate().GoToUrl(baseURL + "/event-table");
            Assert.AreEqual("Demo - Event Table - TestWisely", driver.Title);
            driver.FindElement(By.CssSelector("tr.gridLayoutOddRow > td.tdFieldText > a.tdFieldText")).Click();
            Thread.Sleep(1000);
            Assert.AreEqual("The page you were looking for doesn't exist (404)", driver.Title);
            driver.Quit();
        }

        public int RandomNumber(int min, int max)
        {
            Random x = new Random();
            return x.Next(min, max);
        }

        private bool IsElementPresent(By byX)
                {
                    try
                    {
                      driver.FindElement(byX);
                      return true;
                    }
                    catch (NoSuchElementException)
                    {
                     return false;
                    }
            
                 }


    }

      /*  [TestMethod]
        public void testIE()
        {
            IWebDriver driver = new InternetExplorerDriver();
            driver.Navigate().GoToUrl("http://testwisely.com/demo");
            System.Threading.Thread.Sleep(1000);
            driver.Quit();
        }

        [TestMethod]
        public void testChrome()
        {
            IWebDriver driver = new EdgeDriver();
            driver.Navigate().GoToUrl("http://testwisely.com/demo");
            System.Threading.Thread.Sleep(1000);
           // driver.Quit();
        }
        */
    }

