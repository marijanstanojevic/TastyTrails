using FluentValidation;
using FluentValidation.Results;
using MediatR;
using TastyTrails.Application.Common.Exceptions;

namespace TastyTrails.Application.Common.Behaviours
{
    public class RequestValidationBehaviour<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public RequestValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if(_validators.Any())
            {
                var validationTasks = _validators.Select(v => v.ValidateAsync(request, cancellationToken));
                var validationResult = await Task.WhenAll(validationTasks);
                HandleValidationResult(validationResult);
            }

            return await next();
        }

        private void HandleValidationResult(IEnumerable<ValidationResult> validationResults)
        {
            var errors = validationResults
                .Where(x => x.Errors.Count > 0)
                .SelectMany(x => x.Errors)
                .ToList();

            if (errors.Count > 0)
            {
                var errorsByProperty = errors
                    .GroupBy(x => x.PropertyName, x => x.ErrorMessage)
                    .ToDictionary(x => x.Key, x => x.ToArray());

                throw new RequestValidationException(errorsByProperty);
            }
        }
    }
}
