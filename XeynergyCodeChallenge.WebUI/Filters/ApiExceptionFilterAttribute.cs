using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using XeynergyCodeChallenge.Application.Common.Exceptions;
using XeynergyCodeChallenge.WebUI.Common;

namespace XeynergyCodeChallenge.WebUI.Filters
{
    public sealed class ApiExceptionFilterAttribute: ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;
        private readonly ILogger<ApiExceptionFilterAttribute> _logger;

        public ApiExceptionFilterAttribute(ILogger<ApiExceptionFilterAttribute> logger)
        {
            // Register known exception types and their handlers.
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(ValidationException), HandleValidationException },
                { typeof(NotFoundException), HandleNotFoundException },
            };
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);
            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType().BaseType;
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }
            HandleUnknownException(context);
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Unhandled exception");
            context.Result = new ObjectResult(Envelope.Error<string>("An error occurred while processing your request."))
            {
                StatusCode = StatusCodes.Status500InternalServerError,
            };
            context.ExceptionHandled = true;
        }

        private void HandleValidationException(ExceptionContext context)
        {
            _logger.LogWarning(context.Exception, "Validation exception");
            var exception = context.Exception as ValidationException;
            context.Result = new BadRequestObjectResult(Envelope.Error<string>(exception.Message));
            context.ExceptionHandled = true;
        }

        private void HandleNotFoundException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "NotFound exception");
            var exception = context.Exception as NotFoundException;
            context.Result = new NotFoundObjectResult(Envelope.Error<string>(exception.Message));
            context.ExceptionHandled = true;
        }
    }
}
