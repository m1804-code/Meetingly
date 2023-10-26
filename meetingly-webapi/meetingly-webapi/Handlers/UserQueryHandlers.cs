using MediatR;
using System.Threading;
using System.Threading.Tasks;
using meetingly_webapi.Models;
using meetingly_webapi.Data;
using meetingly_webapi.Queries;

namespace meetingly_webapi.Handlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly MeetinglyDbContext _context;

        public GetUserByIdQueryHandler(MeetinglyDbContext context)
        {
            _context = context;
        }

        public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users.FindAsync(request.Id);
        }
    }

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<User>>
    {
        private readonly MeetinglyDbContext _context;

        public GetAllUsersQueryHandler(MeetinglyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users.ToListAsync(cancellationToken);
        }
    }
}
