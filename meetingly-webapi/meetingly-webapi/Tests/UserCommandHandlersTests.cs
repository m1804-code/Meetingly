using Xunit;
using Moq;
using meetingly_webapi.Handlers;
using meetingly_webapi.Commands;
using meetingly_webapi.Models;
using meetingly_webapi.Data;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace meetingly_webapi.Tests
{
    public class UserCommandHandlersTests
    {
        [Fact]
        public async void CreateUserCommandHandler_ShouldAddUserToContext()
        {
            var mockContext = new Mock<MeetinglyDbContext>();
            var handler = new CreateUserCommandHandler(mockContext.Object);
            var command = new CreateUserCommand("Test User");

            var result = await handler.Handle(command, new CancellationToken());

            mockContext.Verify(m => m.Users.Add(It.IsAny<User>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
            Assert.Equal("Test User", result.Name);
        }

        [Fact]
        public async void UpdateUserCommandHandler_ShouldUpdateUserNameInContext()
        {
            var mockContext = new Mock<MeetinglyDbContext>();
            var handler = new UpdateUserCommandHandler(mockContext.Object);
            var command = new UpdateUserCommand(1, "Updated User");

            var result = await handler.Handle(command, new CancellationToken());

            mockContext.Verify(m => m.Users.FindAsync(It.IsAny<int>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
            Assert.Equal("Updated User", result.Name);
        }
    }
}
