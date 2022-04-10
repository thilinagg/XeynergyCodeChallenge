using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using XeynergyCodeChallenge.Application.Common.Interfaces;

namespace XeynergyCodeChallenge.Application.Commands.User.DeleteUser
{
    public class DeleteUserCommand: IRequest
    {
        public Guid Id { get; init; }

        public DeleteUserCommand(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IApplicationDbContext _context;
        public DeleteUserCommandHandler(IApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.NormalUsers
                .SingleAsync( u=> u.Id == request.Id, cancellationToken);

            _context.NormalUsers.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
