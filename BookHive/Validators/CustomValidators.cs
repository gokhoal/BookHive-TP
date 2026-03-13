using FluentValidation;

namespace BookHive.Validators;

public static class CustomValidators
{
    public static IRuleBuilderOptions<T, string>
        IsValidISBN13<T>(IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Matches(@"^\d{13}$")
            .WithMessage("L’ISBN doit contenir 13 chiffres.")
            .Must(isbn =>
            {
                if (string.IsNullOrEmpty(isbn) || isbn.Length != 13)
                    return false;
                var sum = 0;
                for (int i = 0; i < 12; i++)
                {
                    var digit = isbn[i] - '0';
                    sum += (i % 2 == 0) ? digit : digit * 3;
                }
                var checkDigit = (10 - (sum % 10)) % 10;
                return checkDigit == (isbn[12] - '0');
            })
            .WithMessage("Le chiffre de contrôle ISBN est invalide.");
    }
}