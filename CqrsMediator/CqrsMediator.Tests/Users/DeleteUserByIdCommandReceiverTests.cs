using CqrsMediator.WebApi.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace CqrsMediator.Tests.Users
{
    [TestClass]
    public class DeleteUserByIdCommandReceiverTests
    {
        [TestMethod]
        public async Task ReceiveAsync_ShouldExecuteWithoutError()
        {
            // Arrange
            var command = new DeleteUserByIdCommand { UserId = Guid.NewGuid() };
            var receiver = new DeleteUserByIdCommand.Receiver(); // The actual receiver

            // Act
            // Assert that no exception is thrown
            try
            {
                await receiver.ReceiveAsync(command);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Expected no exception, but got: {ex.Message}");
            }
        }
    }
}
