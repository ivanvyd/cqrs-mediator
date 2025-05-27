using CqrsMediator.Infrastructure.Commands; // This was correct, but ICommandReceiver is also here
using CqrsMediator.Tests.TestDoubles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace CqrsMediator.Tests.Commands
{
    [TestClass]
    public class CommandTransmitterTests
    {
        [TestMethod]
        public async Task SendAsync_ShouldCallReceiverHandleAsync()
        {
            // Arrange
            var mockReceiver = new Mock<ICommandReceiver<TestCommand>>();
            var mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider
                .Setup(sp => sp.GetService(typeof(ICommandReceiver<TestCommand>)))
                .Returns(mockReceiver.Object);

            var commandTransmitter = new CommandTransmitter(mockServiceProvider.Object);
            var testCommand = new TestCommand();

            // Act
            await commandTransmitter.TransmitAsync(testCommand);

            // Assert
            mockReceiver.Verify(r => r.ReceiveAsync(testCommand), Times.Once);
        }

        [TestMethod]
        public async Task TransmitAsync_ShouldThrowIfNoReceiverRegistered() // Renamed test for clarity
        {
            // Arrange
            var mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider
                .Setup(sp => sp.GetService(typeof(ICommandReceiver<TestCommand>)))
                .Returns((ICommandReceiver<TestCommand>)null!); // Simulate no receiver registered & satisfy nullable

            var commandTransmitter = new CommandTransmitter(mockServiceProvider.Object);
            var testCommand = new TestCommand();

            // Act & Assert
            // TransmitAsync now internally uses GetRequiredService, which will throw if not found.
            // The exception type will be InvalidOperationException from GetRequiredService.
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(
                () => commandTransmitter.TransmitAsync(testCommand).AsTask()
            );
        }
    }
}
