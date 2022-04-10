using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using XeynergyCodeChallenge.Application.Common.Interfaces;

namespace XeynergyCodeChallenge.Application.Queries.GetUserById
{
    public class GetUserByIdQuery: IRequest<UserByIdResultDto>
    {
        public Guid UserId { get; init; }
        public GetUserByIdQuery(Guid userId)
        {
            UserId = userId;
        }
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserByIdResultDto>
    {
        private readonly IApplicationDbContext _context;

        public GetUserByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<UserByIdResultDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.NormalUsers
                .Include(u => u.Person)
                .Include(u=> u.UserGroup).ThenInclude(g=> g.AccessRule)
                .Select(u => new UserByIdResultDto
                {
                    Id = u.Id,
                    Name = u.Person.GetFullName(),
                    Email = u.Person.EmailAddress,
                    AttachedCustomerId = u.AttachedCustomerId,
                    UserGroupName = u.UserGroup.GroupName,
                    AccessRuleName = u.UserGroup.AccessRule.RuleName
                })
                .SingleAsync(u => u.Id == request.UserId, cancellationToken: cancellationToken);
        }
    }
}
