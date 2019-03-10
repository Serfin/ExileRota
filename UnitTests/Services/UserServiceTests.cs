using System;
using System.Threading.Tasks;
using AutoMapper;
using ExileRota.Core.Domain;
using ExileRota.Core.Repositories;
using ExileRota.Infrastructure.Services;
using Moq;
using NUnit.Framework;

namespace ExileRota.UnitTests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        [Test]
        public async Task calling_register_async_should_invoke_user_repository_add_assync()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var encrypterMock = new Mock<IEncrypter>();

            encrypterMock.Setup(x => x.GetSalt(It.IsAny<string>())).Returns("hash123");
            encrypterMock.Setup(x => x.GetHash(It.IsAny<string>(), It.IsAny<string>())).Returns("salt123");

            var userService = new UserService(userRepositoryMock.Object, mapperMock.Object, encrypterMock.Object);

            await userService.RegisterAsync(Guid.NewGuid(), "user1", "password123qwe", "user1@gmail.com", "ign1", "user");
            userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
        }

        [Ignore("Red")]
        [Test]
        public async Task calling_get_async_and_user_exists_should_invoke_user_repository_get_async()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var encrypterMock = new Mock<IEncrypter>();

            encrypterMock.Setup(x => x.GetSalt(It.IsAny<string>())).Returns("hash123");
            encrypterMock.Setup(x => x.GetHash(It.IsAny<string>(), It.IsAny<string>())).Returns("salt123");

            var userService = new UserService(userRepositoryMock.Object, mapperMock.Object, encrypterMock.Object);
            await userService.RegisterAsync(Guid.NewGuid(), "user1", "password123qwe", "user1@gmail.com", "ign1", "user");

            await userService.GetByEmailAsync("user1@email.com");

            var user = new User(Guid.NewGuid(), "user1@email.com", "user", "password", "salt", "ign", "user");
            userRepositoryMock.Setup(x => x.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            userRepositoryMock.Verify(x => x.GetByEmailAsync(It.IsAny<string>()), Times.Once());
        }

        [Ignore("Red")]
        [Test]
        public async Task calling_get_async_and_user_does_not_exists_should_ivoke_user_repository_get_async()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var encrypterMock = new Mock<IEncrypter>();

            var userService = new UserService(userRepositoryMock.Object, mapperMock.Object, encrypterMock.Object);
            await userService.GetByEmailAsync("user1@gmail.com");

            userRepositoryMock.Setup(x => x.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(() => null);
            userRepositoryMock.Verify(x => x.GetByEmailAsync(It.IsAny<string>()), Times.Once());
        }

        [Ignore("Red")]
        [Test]
        public async Task calling_login_async_with_valid_arguments_should_login_user()
        {
            //Arrange

            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var encrypterMock = new Mock<IEncrypter>();
            var userService = new UserService(userRepositoryMock.Object, mapperMock.Object, encrypterMock.Object);

            encrypterMock.Setup(x => x.GetSalt(It.IsAny<string>())).Returns("salt123");
            encrypterMock.Setup(x => x.GetHash(It.IsAny<string>(), It.IsAny<string>())).Returns("hash123");


            await userService.RegisterAsync(Guid.NewGuid(), "test_user", "test_password", "test@gmail.com", "test_ign", "user");


            // Act

            await userService.LoginAsync("test@gmail.com", "test_password");

            // Assert

            Assert.DoesNotThrow(() => userService.LoginAsync("test@gmail.com", "test_password"));
        }
    }   
}