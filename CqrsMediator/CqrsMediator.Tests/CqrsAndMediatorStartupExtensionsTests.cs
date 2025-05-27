using CqrsMediator.Infrastructure.Commands;
using CqrsMediator.Infrastructure; // Ensures AddCustomCqrsAndMediator is found
using CqrsMediator.Infrastructure.Queries;
using CqrsMediator.WebApi.Users; // Corrected: GetUserNameByIdQuery is directly under this
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CqrsMediator.Tests.Infrastructure
{
    [TestClass]
    public class CqrsAndMediatorStartupExtensionsTests
    {
        [TestMethod]
        public void AddCqrsAndMediator_ShouldRegisterServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            // Use the assembly where GetUserNameByIdQuery and its nested Receiver are defined.
            services.AddCustomCqrsAndMediator(typeof(GetUserNameByIdQuery.Receiver).Assembly); 

            // Assert
            // Verify transmitters
            Assert.IsNotNull(services.FirstOrDefault(s => s.ServiceType == typeof(ICommandTransmitter) && s.ImplementationType == typeof(CommandTransmitter)));
            Assert.IsNotNull(services.FirstOrDefault(s => s.ServiceType == typeof(IQueryTransmitter) && s.ImplementationType == typeof(QueryTransmitter)));

            // Verify a known handler registration
            // For IQueryReceiver<GetUserNameByIdQuery, string> which is GetUserNameByIdQuery.Receiver
            var queryReceiverDescriptor = services.FirstOrDefault(s => s.ServiceType == typeof(IQueryReceiver<GetUserNameByIdQuery, string>));
            Assert.IsNotNull(queryReceiverDescriptor, "IQueryReceiver<GetUserNameByIdQuery, string> should be registered.");
            Assert.AreEqual(typeof(GetUserNameByIdQuery.Receiver), queryReceiverDescriptor.ImplementationType, "Implementation for IQueryReceiver<GetUserNameByIdQuery, string> should be GetUserNameByIdQuery.Receiver.");

            // More robust check: Build ServiceProvider and resolve
            var serviceProvider = services.BuildServiceProvider();

            Assert.IsNotNull(serviceProvider.GetService<ICommandTransmitter>(), "ICommandTransmitter should be resolvable.");
            Assert.IsNotNull(serviceProvider.GetService<IQueryTransmitter>(), "IQueryTransmitter should be resolvable.");
            
            var resolvedQueryReceiver = serviceProvider.GetService<IQueryReceiver<GetUserNameByIdQuery, string>>();
            Assert.IsNotNull(resolvedQueryReceiver, "IQueryReceiver<GetUserNameByIdQuery, string> should be resolvable.");
            Assert.IsInstanceOfType(resolvedQueryReceiver, typeof(GetUserNameByIdQuery.Receiver), "Resolved query receiver should be of type GetUserNameByIdQuery.Receiver.");

            // UsersDatabase is static and not registered as an instance, so remove checks for it.
        }
    }
}
