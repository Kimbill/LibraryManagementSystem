using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LawPavillion.LibraryManagement.Infrastructure.Helpers.Validations
{
    public sealed class StrongPasswordAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage =
            "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.";

        private static readonly Regex _regex = new(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).+$",
            RegexOptions.Compiled
        );

        public StrongPasswordAttribute() : base(DefaultErrorMessage)
        {
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not string password)
                return new ValidationResult(ErrorMessage);

            return _regex.IsMatch(password)
                ? ValidationResult.Success
                : new ValidationResult(ErrorMessage);
        }
    }

}
