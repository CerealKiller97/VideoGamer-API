using System;
using FluentValidation.Resources;
using FluentValidation.Validators;

namespace SharedModels.CustomValidators
{
    public class UriValidator : PropertyValidator
    {
        public UriValidator(IStringSource errorMessageSource) : base(errorMessageSource)
        {
        }

        public UriValidator(string errorMessageResourceName, Type errorMessageResourceType) : base(
            errorMessageResourceName, errorMessageResourceType)
        {
        }

        public UriValidator(string errorMessage) : base(errorMessage)
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            return context.PropertyValue is string uriString && Uri.TryCreate(uriString, UriKind.Absolute, out var uri);
        }
    }
}