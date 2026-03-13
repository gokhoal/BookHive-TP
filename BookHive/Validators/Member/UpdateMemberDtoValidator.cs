using BookHive.DTO.Member.Request;
using FastEndpoints;
using FluentValidation;

namespace BookHive.Validators.Member;

public class UpdateMemberDtoValidator : Validator<UpdateMemberRequestDto>
{
    public UpdateMemberDtoValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(255)
            .WithMessage("L'email doit respecter le format demandé.");
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100).MinimumLength(2)
            .WithMessage("Le prénom doit comporter entre 2 et 100 caractères.");
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(100).MinimumLength(2)
            .WithMessage("Le nom doit comporter entre 2 et 100 caractères.");
        RuleFor(x => x.MembershipDate).NotEmpty()
            .Must(x => x.DayOfYear > DateTime.Today.DayOfYear && x.Year > DateTime.Today.Year);
    }
}