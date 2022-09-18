using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;

namespace JobCount
{
    class Program
    {
        static void Main(string[] args)
        {
            CompareJobCount();
        }

        static void CompareJobCount()
        {
            // input number of expected jobs
            Console.WriteLine("Enter the number of expected jobs:");
            int expectedJobCount = Convert.ToInt32(Console.ReadLine());

            // start web driver
            IWebDriver webDriver = new ChromeDriver(@"C:\Users\innam");
            webDriver.Url = "https://cz.careers.veeam.com/vacancies";

            // maximize window
            webDriver.Manage().Window.Maximize();

            // go to Research & Development page
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            webDriver.FindElement(By.XPath("//a[text()='Departments']")).Click();
            webDriver.FindElement(By.XPath("//*[@id=\"root\"]/div/header/div[1]/div/div/div/nav/div/div[2]/div/div/div/div/div/ul[1]/a[3]")).Click();

            // scroll down to all jobs part
            Actions actions = new(webDriver);
            actions.MoveToElement(webDriver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[1]/div[3]/section/div/div[2]/a/div[1]/div[2]/a")));
            actions.Perform();

            // count all the number of jobs
            webDriver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[1]/div[3]/section/div/div[2]/a/div[1]/div[2]/a")).Click();
            int jobCount = webDriver.FindElements(By.XPath("//*[@id=\"root\"]/div/div[1]/div/div/div[2]/div/a")).Count;

            // close the web driver
            webDriver.Close();

            if (jobCount.CompareTo(expectedJobCount) == 0)
                Console.WriteLine("THE NUMBER OF JOBS IS VALID");
            else
                Console.WriteLine("THE NUMBER OF JOBS IS NOT VALID. There are found " + jobCount + " jobs.");
        }
    }
}