using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using XeynergyCodeChallenge.Application.Common.Interfaces;

namespace XeynergyCodeChallenge.Application.Commands.User.UpdateUser
{
    public class UpdateUserCommandValidator: AbstractValidator<UpdateUserCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateUserCommandValidator(IApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            RuleFor(v => v.UserId)
                .NotEmpty()
                .WithMessage("User id can not be empty");

            RuleFor(v => v.FirstName)
                .MaximumLength(200);

            RuleFor(v => v.LastName)
                .MaximumLength(100);

            RuleFor(v => v.EmailAddress)
                .EmailAddress().WithMessage("A valid email is required")
                .MustAsync(IsUniqeEmail).WithMessage("Email address is alredy exist");
        }

        public async Task<bool> IsUniqeEmail(string email, CancellationToken cancellationToken)
        {
            return !(await _context.Persons.AnyAsync(p => p.EmailAddress == email, cancellationToken: cancellationToken));
        }
    }
}
