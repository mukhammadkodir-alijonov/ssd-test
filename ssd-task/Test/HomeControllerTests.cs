using Moq;
using Xunit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmployeeImportApp.Controllers;
using ssd_task.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeImportApp.Tests
{
    public class HomeControllerTests
    {
        private readonly HomeController _controller;
        private readonly Mock<EmployeeDbContext> _mockContext;

        public HomeControllerTests()
        {
            var options = new DbContextOptionsBuilder<EmployeeDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _mockContext = new Mock<EmployeeDbContext>(options);
            _controller = new HomeController(_mockContext.Object);
        }

        [Fact]
        public async Task Import_FileUploadIsNull_ReturnsErrorMessage()
        {
            // Act
            var result = await _controller.Import(null) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Please select a file.", result.ViewData["Feedback"]);
        }

        [Fact]
        public async Task Import_ValidFileUpload_AddsRowsAndReturnsSuccessMessage()
        {
            // Arrange
            var fileMock = new Mock<IFormFile>();
            var content = "Personnel_Records.Forenames,Personnel_Records.Surname,Personnel_Records.Date_of_Birth\nJohn,Doe,1990-01-01";
            var fileName = "test.csv";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(f => f.OpenReadStream()).Returns(ms);
            fileMock.Setup(f => f.FileName).Returns(fileName);
            fileMock.Setup(f => f.Length).Returns(ms.Length);

            // Prepare in-memory database with no initial data
            var context = new EmployeeDbContext(new DbContextOptionsBuilder<EmployeeDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options);
            var controller = new HomeController(context);

            // Act
            var result = await controller.Import(fileMock.Object) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("1 rows successfully added.", result.ViewData["Feedback"]);
        }

        [Fact]
        public async Task Delete_EmployeeExists_DeletesEmployeeAndReturnsSuccessMessage()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<EmployeeDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new EmployeeDbContext(options))
            {
                context.Employees.Add(new Employee { Forenames = "John", Surname = "Doe" });
                context.SaveChanges();
            }

            using (var context = new EmployeeDbContext(options))
            {
                var controller = new HomeController(context);

                // Act
                var result = await controller.Delete(1) as ViewResult;

                // Assert
                Assert.NotNull(result);
                Assert.Equal("Employee deleted successfully.", result.ViewData["Feedback"]);

                // Verify employee was deleted
                var employee = context.Employees.Find(1);
                Assert.Null(employee);
            }
        }

        // Add more unit tests as needed
    }
}
