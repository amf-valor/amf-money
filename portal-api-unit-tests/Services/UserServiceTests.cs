using AmfValor.AmfMoney.PortalApi.Data;
using AmfValor.AmfMoney.PortalApi.Data.Model;
using AmfValor.AmfMoney.PortalApi.Model;
using AmfValor.AmfMoney.PortalApi.Services;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace PortalApi.UnitTests.Services
{
    public class UserServiceTests
    {
        DbContextOptions<AmfMoneyContext> _options;
        public UserServiceTests()
        {
            _options = new DbContextOptionsBuilder<AmfMoneyContext>()
                .UseInMemoryDatabase(databaseName: "amf_money")
                .Options;
        }

        [Fact]
        public void ShouldSignUp()
        {
            int expectedId;

            using (var userService = new UserService(new AmfMoneyContext(_options)))
            {
                User user = new User()
                {
                    Birth = new DateTime(1991, 03, 15),
                    Email = "alan-1503@hotmail.com",
                    Password = "senha",
                    Pin="1234"
                };

                expectedId = userService.SignUp(user);
            }

            using (var context = new AmfMoneyContext(_options))
            {
                UserEntity expected = context.Users.Find(expectedId);
                Assert.NotNull(expected);
            }
        }
    }
}
