using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using POE_Part_2.Controllers;
using POE_Part_2.Models;
using POE_Part_2.Services;
using Xunit;

namespace POE_Part_2.Tests
{
    public class HomeControllerTests
    {
        private readonly DataService _dataService;
        private readonly HomeController _controller;
        private readonly DefaultHttpContext _context;

        public HomeControllerTests()
        {
            _dataService = new DataService();
            _controller = new HomeController(_dataService);

            _context = new DefaultHttpContext();
            _context.Session = new Mock<ISession>().Object;
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = _context
            };
        }

        [Fact]
        public void Index_ReturnsView()
        {
            var result = _controller.Index();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Register_InvalidData_ReturnsViewWithError()
        {
            var result = _controller.Register("", "", "", "", "") as ViewResult;
            Assert.Equal("Register", result?.ViewName);
        }

        [Fact]
        public void Register_ValidData_RedirectsToLogin()
        {
            var result = _controller.Register("John", "Lecturer", "john@test.com", "123", "123");
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public void Login_ValidUser_RedirectsToDashboard()
        {
            _dataService.Users.Add(new User { Id = 1, Username = "John", Email = "john@test.com", Password = "123", Role = "Lecturer" });

            var result = _controller.Login("john@test.com", "123");
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Dashboard", redirect.ActionName);
        }

        [Fact]
        public void Login_InvalidUser_ReturnsLoginView()
        {
            var result = _controller.Login("fake@test.com", "wrong");
            var view = Assert.IsType<ViewResult>(result);
            Assert.Equal("Index", view.ViewName);
        }

        [Fact]
        public void Dashboard_WithoutLogin_RedirectsToIndex()
        {
            var result = _controller.Dashboard();
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public void Error_ReturnsErrorView()
        {
            var result = _controller.Error(404);
            var view = Assert.IsType<ViewResult>(result);
            Assert.Equal("Error", view.ViewName);
        }
    }
}

