using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using XeynergyCodeChallenge.Application.Common.Interfaces;

namespace XeynergyCodeChallenge.Application.Queries
{
    public class GetUsersListQuery: IRequest<List<UserDto>>
    {
    }

    public class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery, List<UserDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetUsersListQueryHandler(IApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<UserDto>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            return await _context.NormalUsers
                .Include(u => u.Person)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Person.GetFullName(),
                    Email = u.Person.EmailAddress,
                    AttachedCustomerId = u.AttachedCustomerId
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
