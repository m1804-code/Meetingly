using MediatR;
using System.Threading;
using System.Threading.Tasks;
using meetingly_webapi.Models;
using meetingly_webapi.Data;
using meetingly_webapi.Commands;

namespace meetingly_webapi.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly MeetinglyDbContext _context;

        public CreateUserCommandHandler(MeetinglyDbContext context)
        {
            _context = context;
        }

        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User { Name = request.Name };
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
            return user;
        }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, User>
    {
        private readonly MeetinglyDbContext _context;

        public UpdateUserCommandHandler(MeetinglyDbContext context)
        {
            _context = context;
        }

        public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.Id);
            user.Name = request.Name;
            await _context.SaveChangesAsync(cancellationToken);
            return user;
        }
    }
}
