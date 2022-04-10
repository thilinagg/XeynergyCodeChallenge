using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using XeynergyCodeChallenge.Application.Common.Interfaces;

namespace XeynergyCodeChallenge.Application.Commands.User.CreateUser
{
    public class CreateUserCommandValidator: AbstractValidator<CreateUserCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateUserCommandValidator(IApplicationDbContext context)
        {

            _context = context?? throw new ArgumentNullException(nameof(context));

            RuleFor(v => v.FirstName)
                .MaximumLength(200)
                .NotEmpty();

            RuleFor(v => v.LastName)
                .MaximumLength(100)
                .NotEmpty();

            RuleFor(v => v.EmailAddress)
                .NotEmpty()
                .EmailAddress().WithMessage("A valid email is required")
                .MustAsync(IsUniqeEmail).WithMessage("Email address is alredy exist");
        }

        public async Task<bool> IsUniqeEmail(string email, CancellationToken cancellationToken)
        {
            return !(await _context.Persons.AnyAsync(p => p.EmailAddress == email, cancellationToken: cancellationToken));
        }
    }
}
