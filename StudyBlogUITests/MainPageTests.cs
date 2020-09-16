using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace StudyBlogUITests
{
    public class MainPageTests : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly BasicSteps _basicSteps;
        public MainPageTests()
        {
            _driver = new ChromeDriver();
            _basicSteps = new BasicSteps(_driver);
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        [Fact]
        public void CheckMainPageTitleAndHeadingTest()
        {
            _basicSteps.GoToMainPage();
            Assert.True(_basicSteps
                .IsElementFound("Публикации пользователей"));
            Assert.True(_basicSteps
                .IsElementFound("Все записи"));
        }

        [Fact]
        public void LoginWrongModelDataReturnsErrorMessageTest()
        {
            _basicSteps.GoToLoginPage();
            _basicSteps.FillTextField("Email", "wrong@email.asd");
            _basicSteps.FillTextField("Password", "wrongPassword");
            _basicSteps.ClickById("submit");
            Assert.True(_basicSteps.IsElementFound("Неправильный логин или пароль"));
        }
        
        [Fact]
        public void LoginEmptyEmailDataReturnsErrorMessageTest()
        {
            _basicSteps.GoToLoginPage();
            _basicSteps.FillTextField("Email", String.Empty);
            _basicSteps.FillTextField("Password", "wrongPassword");
            _basicSteps.ClickById("submit");
            Assert.True(_basicSteps.IsElementFound("Это поле обязательно"));
        }

        [Fact]
        public void LoginCorrectDataReturnsSuccessAuthTest()
        {
            _driver.Navigate()
                .GoToUrl("http://localhost:5000/Account/Login");
            _driver.FindElement(By.Id("Email"))
                .SendKeys("admin@admin.com");
            _driver.FindElement(By.Id("Password"))
                .SendKeys("1qaz@WSX29");
            _driver.FindElement(By.Id("submit"))
                .Click();
            var userListButtonLink = _driver.FindElement(By.LinkText("Список пользователей"))
                .GetAttribute("href");
            var userSearchButtonLink = _driver.FindElement(By.LinkText("Поиск пользователей"))
                .GetAttribute("href");
            Assert.Contains("Выход", _driver.PageSource);
            Assert.Equal("http://localhost:5000/Users", userListButtonLink);
            Assert.Equal("http://localhost:5000/Users/SearchAjax", userSearchButtonLink);
        }

        [Fact]
        public void CreateNewPostTest()
        {
            _basicSteps.GoToLoginPage();
            _basicSteps.FillTextField("Email", "admin@admin.com");
            _basicSteps.FillTextField("Password", "1qaz@WSX29");
            _basicSteps.ClickById("submit");
            _basicSteps.ClickLink("Новая публикация");
            _basicSteps.FillTextField("Description", "Проверочное описание");
            _basicSteps.LoadTestImgFile();
            _basicSteps.ClickById("submit");
            Assert.True(_basicSteps.IsElementFound("Проверочное описание"));
            Assert.True(_basicSteps.IsImageFound("test_cat.jpg"));
        }
    }
}