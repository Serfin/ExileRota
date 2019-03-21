using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ExileRota.Core.Domain;
using ExileRota.Core.Repositories;
using ExileRota.Infrastructure.DTO;
using ExileRota.Infrastructure.Services;
using Moq;
using NUnit.Framework;

namespace ExileRota.UnitTests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        [Test]
        public async Task calling_get_all_async_should_return_all_users()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var encrypterMock = new Mock<IEncrypter>();
            var userService = new UserService(userRepositoryMock.Object, mapperMock.Object, encrypterMock.Object);
            IEnumerable<User> user = new List<User> { It.IsAny<User>() };
            IEnumerable<UserDto> userDto = new List<UserDto> { It.IsAny<UserDto>() };

            userRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(user);
            mapperMock.Setup(x => x.Map<IEnumerable<User>, IEnumerable<UserDto>>(user)).Returns(userDto);
            var users = await userService.GetAllAsync();

            Assert.IsNotEmpty(users);
        }
        [Test]
        public async Task calling_register_async_should_invoke_user_repository_add_assync()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var encrypterMock = new Mock<IEncrypter>();
            var userService = new UserService(userRepositoryMock.Object, mapperMock.Object, encrypterMock.Object);

            encrypterMock.Setup(x => x.GetSalt(It.IsAny<string>())).Returns("hash123");
            encrypterMock.Setup(x => x.GetHash(It.IsAny<string>(), It.IsAny<string>())).Returns("salt123");
            await userService.RegisterAsync(Guid.NewGuid(), "user1", "password123qwe", "user1@gmail.com", "ign1", "user");

            userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public async Task calling_get_async_and_user_exists_should_invoke_user_repository_get_async()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var encrypterMock = new Mock<IEncrypter>();
            var userService = new UserService(userRepositoryMock.Object, mapperMock.Object, encrypterMock.Object);
            var user = new User(Guid.NewGuid(), "user1@email.com", "user", "password", "salt", "ign", "user");

            userRepositoryMock.Setup(x => x.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            await userService.GetByEmailAsync(It.IsAny<string>());
            
            userRepositoryMock.Verify(x => x.GetByEmailAsync(It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void calling_get_async_and_user_does_not_exists_should_ivoke_user_repository_get_async()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var encrypterMock = new Mock<IEncrypter>();
            var userService = new UserService(userRepositoryMock.Object, mapperMock.Object, encrypterMock.Object);

            userRepositoryMock.Setup(x => x.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(() => null);

            Assert.ThrowsAsync<Exception>(() => userService.GetByEmailAsync(It.IsAny<string>()));
            userRepositoryMock.Verify(x => x.GetByEmailAsync(It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void calling_login_async_with_valid_arguments_should_login_user()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var encrypterMock = new Mock<IEncrypter>();
            var userService = new UserService(userRepositoryMock.Object, mapperMock.Object, encrypterMock.Object);
            var user = new User(Guid.NewGuid(), "test_email", "test_user", "password", "salt", "ign", "role");

            userRepositoryMock.Setup(x => x.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            encrypterMock.Setup(x => x.GetHash(It.IsAny<string>(), It.IsAny<string>())).Returns("password");

            Assert.DoesNotThrowAsync(async () => await userService.LoginAsync(It.IsAny<string>(), It.IsAny<string>()));
        }
    }   
}