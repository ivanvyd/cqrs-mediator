using CqrsMediator.Infrastructure.Queries; // This was correct, IQueryReceiver is also here
using CqrsMediator.Tests.TestDoubles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace CqrsMediator.Tests.Queries
{
    [TestClass]
    public class QueryTransmitterTests
    {
        [TestMethod]
        public async Task SendAsync_ShouldCallReceiverHandleAsync_AndReturnResult()
        {
            // Arrange
            var expectedResult = new TestQueryResult { Data = "Expected Data" };
            var mockReceiver = new Mock<IQueryReceiver<TestQuery, TestQueryResult>>();
            mockReceiver
                .Setup(r => r.ReceiveAsync(It.IsAny<TestQuery>()))
                .ReturnsAsync(expectedResult);

            var mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider
                .Setup(sp => sp.GetService(typeof(IQueryReceiver<TestQuery, TestQueryResult>)))
                .Returns(mockReceiver.Object);

            var queryTransmitter = new QueryTransmitter(mockServiceProvider.Object);
            var testQuery = new TestQuery();

            // Act
            var result = await queryTransmitter.TransmitAsync<TestQuery, TestQueryResult>(testQuery);

            // Assert
            mockReceiver.Verify(r => r.ReceiveAsync(testQuery), Times.Once);
            Assert.AreSame(expectedResult, result);
            Assert.AreEqual("Expected Data", result.Data);
        }

        [TestMethod]
        public async Task TransmitAsync_ShouldThrowIfNoReceiverRegistered() // Renamed test for clarity
        {
            // Arrange
            var mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider
                .Setup(sp => sp.GetService(typeof(IQueryReceiver<TestQuery, TestQueryResult>)))
                .Returns((IQueryReceiver<TestQuery, TestQueryResult>)null!); // Simulate no receiver registered & satisfy nullable

            var queryTransmitter = new QueryTransmitter(mockServiceProvider.Object);
            var testQuery = new TestQuery();

            // Act & Assert
            // TransmitAsync now internally uses GetRequiredService, which will throw if not found.
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(
                () => queryTransmitter.TransmitAsync<TestQuery, TestQueryResult>(testQuery).AsTask()
            );
        }
    }
}
