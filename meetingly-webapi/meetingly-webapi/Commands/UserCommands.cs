using MediatR;
using meetingly_webapi.Models;

namespace meetingly_webapi.Commands
{
    public class CreateUserCommand : IRequest<User>
    {
        public string Name { get; set; }

        public CreateUserCommand(string name)
        {
            Name = name;
        }
    }

    public class UpdateUserCommand : IRequest<User>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public UpdateUserCommand(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
