using Bogus;
using Moq;
using OrderingSystem.Application.Repositories;
using OrderingSystem.Application.Services;
using OrderingSystem.Application.Utils;
using OrderingSystem.Infrastructure;
using OrderingSystem.Infrastructure.Databases.OrderingSystem;

namespace OrderingSystem.UnitTest
{
    public class UnitTest1
    {
        //[Fact]
        public void IsCustomException_ShouldReturnTrue()
        {
            //Arrange
            var exception = new RecordNotFoundException("Testing");

            //Act
            var result = exception.IsCustomException();

            //Assert
            Assert.True(result);
        }
        //[Theory]
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
        //[Fact]
        public void GetMenuById_ShouldThrow_RecordNotFoundException()
        {
            //Arange
            var menuRepoMock = new Mock<IMenuRepository>();
            var baseRepoMock = new Mock<IBaseRepository>();
            menuRepoMock.Setup(o => o.GetMenuById(It.IsAny<Guid>())).ReturnsAsync(null as TblMenu);
            var menuService = new MenuService(baseRepoMock.Object, menuRepoMock.Object);

            //Act
            //Arrange
            Assert.ThrowsAsync<RecordNotFoundException>(() => menuService.GetMenuById(Guid.NewGuid()));
        }
       // [Fact]
        public async Task GetMenusByTypes_ShouldDoCorrectThing()
        {
            //Arange
            var menuRepoMock = new Mock<IMenuRepository>();
            var baseRepoMock = new Mock<IBaseRepository>();
            var tblMenuFaker = new Faker<TblMenu>();
            var menuType = new MenuType[] { MenuType.Drinks, MenuType.MainCourse, MenuType.Others, MenuType.Dessert };
            tblMenuFaker.RuleForType(typeof(string), f => f.Random.String());
            tblMenuFaker.RuleForType(typeof(Guid), f => f.Random.Guid());
            tblMenuFaker.RuleFor(o => o.MenuType, f => menuType[f.Random.Number(menuType.Length - 1)]);
            var tblMenus = tblMenuFaker.Generate(20);
            menuRepoMock.Setup(o => o.GetAllMenu(It.IsAny<bool>())).ReturnsAsync(tblMenus);
            var menuService = new MenuService(baseRepoMock.Object, menuRepoMock.Object);

            //Act
            var result = await menuService.GetMenusByTypes(true);

            //Assert
            Assert.All(result.MainCourse, o => Assert.Equal(MenuType.MainCourse, o.MenuType));
            Assert.All(result.Drinks, o => Assert.Equal(MenuType.Drinks, o.MenuType));
            Assert.All(result.Desert, o => Assert.Equal(MenuType.Dessert, o.MenuType));
            Assert.All(result.Others, o => Assert.Equal(MenuType.Others, o.MenuType));
        }
    }
}