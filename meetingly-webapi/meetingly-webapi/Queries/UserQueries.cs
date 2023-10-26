using MediatR;
using meetingly_webapi.Models;
using System.Collections.Generic;

namespace meetingly_webapi.Queries
{
    public class GetUserByIdQuery : IRequest<User>
    {
        public int Id { get; }

        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }

    public class GetAllUsersQuery : IRequest<IEnumerable<User>>
    {
        public GetAllUsersQuery()
        {
        }
    }
}
