using OrderingSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Application.Utils;
using Moq;
using OrderingSystem.Application.Repositories;
using OrderingSystem.Application.Services;
using OrderingSystem.Infrastructure.Databases.OrderingSystem;

namespace OrderingSystem.UnitTest
{
    public class DemoUnitTest
    {
        [Fact]
        public void IsCustomException_ShouldReturnTrue()
        {
            //Arrange
            var exception = new RecordNotFoundException("Testing");

            //Act
            var result = exception.IsCustomException();

            //Assert
            Assert.True(result);
        }
        [Fact]
        public void GetMenuById_ShouldThrow_RecordNotFoundException()
        {
            //Arange
            var menuRepoMock = new Mock<IMenuRepository>();
            var baseRepoMock = new Mock<IBaseRepository>();
            menuRepoMock.Setup(o => o.GetMenuById(It.IsAny<Guid>())).ReturnsAsync(null as TblMenu);
            var menuService = new MenuService(baseRepoMock.Object, menuRepoMock.Object);

            //Act
            //Assert
            Assert.ThrowsAsync<RecordNotFoundException>(() => menuService.GetMenuById(Guid.NewGuid()));
        }
        [Theory]
        [InlineData("ali1404udin@gmail.com", true)]
        [InlineData("aliuddin", false)]
        public void StringIsEmail_ShouldDoCorrectThing(string email, bool isEmail)
        {
            //Arrange
            //Act
            var result = email.StringIsEmail();
            //Assert
            Assert.Equal(isEmail, result);
        }
    }
}
