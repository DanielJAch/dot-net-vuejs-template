using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace DotNETVueJSTemplate.Services.Validation
{
    /// <summary>
    /// this validator is used for classes that inherit or implement classes with their own validators
    /// </summary>
    /// <example>
    /// public class [DerivedClass]Validator : CompositeValidator`[DerivedClass]
    /// {
    ///     public [DerivedClass]Validator()
    ///     {
    ///         RegisterBaseValidator(new [BaseClass]Validator());
    ///         
    ///         RuleFor...
    ///         RuleFor...
    ///     }
    /// }
    /// </example>
    public abstract class CompositeValidator<T> : AbstractValidator<T>
    {
        private readonly IList<IValidator> _otherValidators = new List<IValidator>();

        protected void RegisterBaseValidator<TBase>(IValidator<TBase> validator)
        {
            // Ensure that we've registered a compatible validator
            if (validator.CanValidateInstancesOfType(typeof(T)))
            {
                _otherValidators.Add(validator);
            }
            else
            {
                throw new NotSupportedException($"Type {typeof(TBase).Name} is not a base class or interface implemented by {typeof(T).Name}");
            }
        }

        public override ValidationResult Validate(ValidationContext<T> context)
        {
            var errors = base.Validate(context).Errors.ToList();

            foreach (var other in _otherValidators)
            {
                errors.AddRange(other.Validate(context).Errors);
            }

            return new ValidationResult(errors);
        }

        public override async Task<ValidationResult> ValidateAsync(ValidationContext<T> context, CancellationToken cancellation = default(CancellationToken))
        {
            var asyncResult = await base.ValidateAsync(context, cancellation);
            var errors = asyncResult.Errors.ToList();

            foreach (var other in _otherValidators)
            {
                var result = await other.ValidateAsync(context, cancellation);
                errors.AddRange(result.Errors);
            }

            return new ValidationResult(errors);
        }
    }
}
