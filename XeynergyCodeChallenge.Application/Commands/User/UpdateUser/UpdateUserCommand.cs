using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using XeynergyCodeChallenge.Application.Common.Interfaces;

namespace XeynergyCodeChallenge.Application.Commands.User.UpdateUser
{
    public class UpdateUserCommand: IRequest
    {
        public Guid UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateUserCommandHandler(IApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.NormalUsers
                .Include(u=> u.Person)
                .SingleAsync(x => x.Id == request.UserId, cancellationToken: cancellationToken);

            user.Person.FirstName ??= request.FirstName;
            user.Person.LastName ??= request.LastName;
            user.Person.EmailAddress ??= request.EmailAddress;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

