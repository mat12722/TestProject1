using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using OpenQA.Selenium.Interactions;

namespace TestProject1
{
    public class Tests
    {
        private WebDriver WebDriver { get; set; } = null!;
        private string DriverPath { get; set; } = @"C:\Users\mat12\source\chromedriver_win32";
        private string BaseUrl { get; set; } = "https://trytestingthis.netlify.app";
        private WebDriver GetChromeDriver()
        {
            var options = new ChromeOptions();
            return new ChromeDriver(DriverPath, options, TimeSpan.FromSeconds(300));
        }
        [SetUp]
        public void Setup()
        {
            WebDriver = GetChromeDriver();
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        }
        [TearDown]
        public void TearDown()
        {
            WebDriver.Quit();
        }

        [Test]
        public void Test1()
        {
            //sprawdzenie czy znajdujemy siê na odpowiedniej stronie
            WebDriver.Navigate().GoToUrl(BaseUrl);
            Assert.AreEqual("Try Testing This", WebDriver.Title);
        }
        [Test]
        public void Test2()
        {
            //sprawdzenie czy imiê i nazwisko zosta³o poprawnie wczytane
            WebDriver.Navigate().GoToUrl(BaseUrl);
            //Thread.Sleep(5000);
            var inputFname = WebDriver.FindElement(By.Id("fname"));
            inputFname.Clear();
            inputFname.SendKeys("Imie");

            //Thread.Sleep(5000);
            var inputLname = WebDriver.FindElement(By.Id("lname"));
            inputLname.Clear();
            inputLname.SendKeys("Nazwisko");

            //Thread.Sleep(5000);
            var testF = WebDriver.FindElement(By.Id("fname"));
            String testFValue=testF.GetAttribute("value");
            var testL = WebDriver.FindElement(By.Id("lname"));
            String testLValue = testL.GetAttribute("value");
            Assert.AreEqual("Imie", testFValue);
            Assert.AreEqual("Nazwisko", testLValue);
        }
        [Test]
        public void Test3()
        {
            //radio button test
            WebDriver.Navigate().GoToUrl(BaseUrl);
            //Thread.Sleep(5000);
            var radioBMale = WebDriver.FindElement(By.Id("male"));
            radioBMale.Click();
            //Thread.Sleep(5000);
            var radioBFemale = WebDriver.FindElement(By.Id("female"));
            radioBFemale.Click();
            //Thread.Sleep(5000);
            var radioBOther = WebDriver.FindElement(By.Id("other"));
            radioBOther.Click();
            //Thread.Sleep(5000);
            String radioChecked = WebDriver.FindElement(By.Id("other")).GetAttribute("checked");
            Assert.AreEqual("true", radioChecked);
        }
        [Test]
        public void Test4()
        {
            //scrolable option test
            WebDriver.Navigate().GoToUrl(BaseUrl);
            //Thread.Sleep(5000);
            SelectElement optionCheck = new SelectElement(WebDriver.FindElement(By.Id("owc")));
            optionCheck.SelectByValue("option 1");
            //Thread.Sleep(5000);
            SelectElement optionCheckd = new SelectElement(WebDriver.FindElement(By.Id("owc")));
            string selectedOptionList = optionCheckd.SelectedOption.Text;
            Assert.AreEqual("Option 1", selectedOptionList);
        }
        [Test]
        public void Test5()
        {
            //checkbox option test
            WebDriver.Navigate().GoToUrl(BaseUrl);
            //Thread.Sleep(5000);
            IWebElement optionCheck1 = WebDriver.FindElement(By.Name("option1"));
            optionCheck1.Click();
            IWebElement optionCheck2 = WebDriver.FindElement(By.Name("option2"));
            optionCheck2.Click();
            IWebElement optionCheck3 = WebDriver.FindElement(By.Name("option3"));
            //Thread.Sleep(5000);
            if(optionCheck1.Selected==true && optionCheck2.Selected==true && optionCheck3.Selected==false)
            {
                Assert.IsTrue(true);
            }else
            {
                Assert.IsTrue(false);
            }
        }
        [Test]
        public void Test6()
        {
            //checkbox option test
            WebDriver.Navigate().GoToUrl(BaseUrl);
            //Thread.Sleep(5000);
            IWebElement optionCheck1 = WebDriver.FindElement(By.Name("Options"));
            optionCheck1.SendKeys("Chocolate");
            //Thread.Sleep(5000);
            var testL = WebDriver.FindElement(By.Name("Options"));
            String testValue = testL.GetAttribute("value");
            Assert.AreEqual("Chocolate", testValue);
        }
        [Test]
        public void Test7()
        {
            //datalist input test
            WebDriver.Navigate().GoToUrl(BaseUrl);
            //Thread.Sleep(5000);
            IWebElement optionCheck1 = WebDriver.FindElement(By.Name("favcolor"));
            optionCheck1.SendKeys("#FF0000");
            //Thread.Sleep(5000);
            
        }
        [Test]
        public void Test8()
        {
            //date input test
            WebDriver.Navigate().GoToUrl(BaseUrl);
            //Thread.Sleep(5000);
            IWebElement date = WebDriver.FindElement(By.Name("day"));
            date.SendKeys("01032023");
            //Thread.Sleep(5000);
            String LocalDate = DateTime.Today.ToString("yyyy/MM/dd");
            String date2 = date.GetAttribute("value").Replace('-', '.');
            
            Console.WriteLine(LocalDate);
            Assert.AreEqual(LocalDate, date2);

        }
        [Test]
        public void Test9()
        {
            //range input test
            WebDriver.Navigate().GoToUrl(BaseUrl);
            //Thread.Sleep(5000);
            IWebElement rangeCheck = WebDriver.FindElement(By.Id("a"));
            Actions action = new Actions(WebDriver);
            action.DragAndDropToOffset(rangeCheck, 0,1).Build().Perform();
            //Thread.Sleep(5000);
            Assert.AreEqual("49", rangeCheck.GetAttribute("value"));
        }
        [Test]
        public void Test10()
        {
            //file input test
            WebDriver.Navigate().GoToUrl(BaseUrl);
            //Thread.Sleep(5000);
            IWebElement fileCheck = WebDriver.FindElement(By.Id("myfile"));
            fileCheck.SendKeys("C:/Users/mat12/OneDrive/Pulpit/test.txt");
            //Thread.Sleep(50000);
        }
        [Test]
        public void Test11()
        {
            //number input test
            WebDriver.Navigate().GoToUrl(BaseUrl);
            //Thread.Sleep(5000);
            IWebElement numberCheck = WebDriver.FindElement(By.Id("quantity"));
            numberCheck.SendKeys("4");
            //Thread.Sleep(5000);
            Assert.AreEqual("4", numberCheck.GetAttribute("value"));
        }
        [Test]
        public void Test12()
        {
            //number input test
            WebDriver.Navigate().GoToUrl(BaseUrl);
            //Thread.Sleep(5000);
            var messageCheck = WebDriver.FindElement(By.Name("message"));
            messageCheck.Clear();
            messageCheck.SendKeys("message is rly long");
            Assert.AreEqual("message is rly long", messageCheck.GetAttribute("value"));
        }
        [Test]
        public void Test13()
        {
            //number input test
            WebDriver.Navigate().GoToUrl(BaseUrl);
            //Thread.Sleep(5000);
            var messageCheck = WebDriver.FindElement(By.Name("message"));
            messageCheck.Clear();
            messageCheck.SendKeys("message is rly long");
            Assert.AreEqual("message is rly long", messageCheck.GetAttribute("value"));

        }
    }
}