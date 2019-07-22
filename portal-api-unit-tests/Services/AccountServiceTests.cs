using AmfValor.AmfMoney.PortalApi.Data;
using AmfValor.AmfMoney.PortalApi.Data.Model;
using AmfValor.AmfMoney.PortalApi.Model;
using AmfValor.AmfMoney.PortalApi.Services;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace PortalApi.UnitTests.Services
{
    public class AccountServiceTests
    {
        DbContextOptions<AmfMoneyContext> _options;
        public AccountServiceTests()
        {
            _options = new DbContextOptionsBuilder<AmfMoneyContext>()
                .UseInMemoryDatabase(databaseName: "amf_money")
                .Options;
        }

        [Fact]
        public void ShouldSignUp()
        {
            int expectedId;

            using (var accountService = new AccountService(new AmfMoneyContext(_options)))
            {
                Account account = new Account()
                {
                    Birth = new DateTime(1991, 03, 15),
                    Email = "alan-1503@hotmail.com",
                    Password = "senha",
                    Pin="1234"
                };

                expectedId = accountService.SignUp(account);
            }

            using (var context = new AmfMoneyContext(_options))
            {
                AccountEntity expected = context.Accounts.Find(expectedId);
                Assert.NotNull(expected);
            }
        }

        [Fact]
        public void ShouldReturnTrueForExistingEmail()
        {
            string emailToBeChecked = "alan-1503@hotmail.com";

            using (var context = new AmfMoneyContext(_options))
            {
                AccountEntity account = new AccountEntity()
                {
                    Birth = new DateTime(1991, 03, 15),
                    Email = emailToBeChecked,
                    PasswordHashed = new byte[] { 1, 2},
                    PinHashed = new byte[] { 1, 2 },
                    PasswordSalt = new byte[] { 1, 2 },
                    PinSalt = new byte[] { 1, 2 }
                };

                context.Accounts.Add(account);
                context.SaveChanges();
            }

            var accountService = new AccountService(new AmfMoneyContext(_options));
            bool actual = accountService.CheckIfExists(emailToBeChecked);

            Assert.True(actual);
        }

        [Fact]
        public void ShouldReturnFalseForNonExistingEmail()
        {
            using (var context = new AmfMoneyContext(_options))
            {
                AccountEntity user = new AccountEntity()
                {
                    Birth = new DateTime(1991, 03, 15),
                    Email = "alan-1503@hotmail.com",
                    PasswordHashed = new byte[] { 1, 2 },
                    PinHashed = new byte[] { 1, 2 },
                    PasswordSalt = new byte[] { 1, 2 },
                    PinSalt = new byte[] { 1, 2 }
                };

                context.Accounts.Add(user);
                context.SaveChanges();
            }

            var accountService = new AccountService(new AmfMoneyContext(_options));
            bool actual = accountService.CheckIfExists("amfmoney@amfmoney.com");

            Assert.False(actual);
        }
    }
}
