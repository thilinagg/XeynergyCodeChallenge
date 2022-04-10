using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using XeynergyCodeChallenge.Application.Common.Interfaces;
using XeynergyCodeChallenge.Domain.Entities;

namespace XeynergyCodeChallenge.Application.Commands.User.CreateUser
{
    public class CreateUserCommand: IRequest<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        public CreateUserCommandHandler(IApplicationDbContext context)
        {
            _context = context?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var person = await CreatePerson(request);

            var user = new NormalUser
            {
                PersonId = person.Id,
                AttachedCustomerId = (await _context.Customers.FirstAsync()).Id, // this is only demonstration purpose, this value should come from client. 
                UserGroupId = (await _context.UserGroups.FirstAsync()).Id, // this is only demonstration purpose, this value should come from client.
            };

            await _context.NormalUsers.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }

        private async Task<Person> CreatePerson(CreateUserCommand request)
        {
            var person = new Person
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailAddress = request.EmailAddress,
            };
            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();
            return person;
        }
    }
}
