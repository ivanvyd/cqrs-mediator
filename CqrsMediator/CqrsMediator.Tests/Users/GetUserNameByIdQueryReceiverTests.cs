using CqrsMediator.WebApi.Users; // Adjusted namespace
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CqrsMediator.Tests.Users
{
    [TestClass]
    public class GetUserNameByIdQueryReceiverTests
    {
        [TestMethod]
        public async Task ReceiveAsync_ShouldReturnUserName_WhenUserExists()
        {
            // Arrange
            // GetUserNameByIdQuery.Receiver news up its own UsersDatabase.SeedUsersData
            // To test this, we need to know what data is in SeedUsersData
            // Let's pick a known Guid from UsersDatabase.SeedUsersData if possible,
            // or rely on the fact that Guid.Empty is one of them.
            var query = new GetUserNameByIdQuery { UserId = Guid.Empty };
            var receiver = new GetUserNameByIdQuery.Receiver(); // The actual receiver

            // Act
            var result = await receiver.ReceiveAsync(query);

            // Assert
            Assert.AreEqual("Empty User", result, "Should return the name for Guid.Empty");
        }

        [TestMethod]
        public async Task ReceiveAsync_ShouldThrowKeyNotFoundException_WhenUserDoesNotExist()
        {
            // Arrange
            var nonExistentUserId = Guid.NewGuid(); // A random Guid not in the seeded data
            var query = new GetUserNameByIdQuery { UserId = nonExistentUserId };
            var receiver = new GetUserNameByIdQuery.Receiver();

            // Act & Assert
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(
                () => receiver.ReceiveAsync(query).AsTask(),
                "Should throw KeyNotFoundException for a non-existent user ID."
            );
        }
    }
}
