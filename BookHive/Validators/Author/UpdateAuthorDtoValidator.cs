using BookHive.DTO.Author.Request;
using FastEndpoints;
using FluentValidation;

namespace BookHive.Validators.Author;

public class UpdateAuthorDtoValidator : Validator<UpdateAuthorRequestDto>
{
    public UpdateAuthorDtoValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100).MinimumLength(2);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(100).MinimumLength(2);
        RuleFor(x => x.Biography).MaximumLength(2000);
        RuleFor(x => x.BirthDate).NotEmpty().Must(x => x.DayOfYear < DateTime.Today.DayOfYear && x.Year < DateTime.Today.Year);
        RuleFor(x => x.Nationality).NotEmpty().MaximumLength(60);
    }
}