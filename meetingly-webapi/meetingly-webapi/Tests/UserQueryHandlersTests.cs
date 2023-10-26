using Xunit;
using Moq;
using meetingly_webapi.Handlers;
using meetingly_webapi.Queries;
using meetingly_webapi.Models;
using meetingly_webapi.Data;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace meetingly_webapi.Tests
{
    public class UserQueryHandlersTests
    {
        [Fact]
        public async void GetUserByIdQueryHandler_ShouldReturnUserFromContext()
        {
            var mockContext = new Mock<MeetinglyDbContext>();
            var handler = new GetUserByIdQueryHandler(mockContext.Object);
            var query = new GetUserByIdQuery(1);

            var result = await handler.Handle(query, new CancellationToken());

            mockContext.Verify(m => m.Users.FindAsync(It.IsAny<int>()), Times.Once());
            Assert.IsType<User>(result);
        }

        [Fact]
        public async void GetAllUsersQueryHandler_ShouldReturnAllUsersFromContext()
        {
            var mockContext = new Mock<MeetinglyDbContext>();
            var handler = new GetAllUsersQueryHandler(mockContext.Object);
            var query = new GetAllUsersQuery();

            var result = await handler.Handle(query, new CancellationToken());

            mockContext.Verify(m => m.Users.ToListAsync(It.IsAny<CancellationToken>()), Times.Once());
            Assert.IsType<IEnumerable<User>>(result);
        }
    }
}
