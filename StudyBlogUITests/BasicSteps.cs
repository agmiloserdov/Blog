using System;
using OpenQA.Selenium;

namespace StudyBlogUITests
{
    public class BasicSteps : IDisposable
    {
        private readonly IWebDriver _driver;
        private const string MainPageUrl = "http://localhost:5000";
        private const string LoginPageUrl = MainPageUrl + "/Account/Login";
        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        public BasicSteps(IWebDriver driver)
        {
            _driver = driver;
        }

        public void GoToUrl(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public void GoToMainPage()
        {
            GoToUrl(MainPageUrl);
        }

        public void GoToLoginPage()
        {
            GoToUrl(LoginPageUrl);
        }

        public void ClickLink(string linkText)
        {
            _driver.FindElement(By.LinkText(linkText))
                .Click();
        }

        public void ClickButton(string caption)
        {
            _driver.FindElement(By.XPath($"//button[contains(text(), '{caption}')]"))
                .Click();
        }

        public void ClickById(string id)
        {
            _driver.FindElement(By.Id(id)).Click();
        }

        public void FillTextField(string fieldId, string inputText)
        {
            _driver.FindElement(By.Id(fieldId))
                .SendKeys(inputText);
        }

        public bool IsElementFound(string text)
        {
            var element = _driver.FindElement(By.XPath($"//*[contains(text(), '{text}')]"));
            return element != null;
        }

        public void LoadTestImgFile()
        {
            _driver.FindElement(By.Id("File"))
                .SendKeys(@"D:\CSharp\CSharp_ESDP\Lesson_5\studyblog\StudyBlogUITests\test_cat.jpg");
        }

        public bool IsImageFound(string imageName)
        {
            return _driver.PageSource.Contains(imageName);
        }
    }
}