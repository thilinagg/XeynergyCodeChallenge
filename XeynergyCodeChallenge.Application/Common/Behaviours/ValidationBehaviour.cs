using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = XeynergyCodeChallenge.Application.Common.Exceptions.ValidationException;

namespace XeynergyCodeChallenge.Application.Common.Behaviours
{
    public sealed class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var failures = _validators
                    .Select(v => v.Validate(context))
                    .SelectMany(result => result.Errors)
                    .Where(f => f is not null)
                    .ToList();

                if (failures.Count != 0)
                    throw new ValidationException(failures.First().ErrorMessage);
            }

            return next();
        }
    }
}
