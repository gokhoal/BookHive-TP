using BookHive.DTO.Author.Response;
using FastEndpoints;
using FluentValidation;

namespace BookHive.Validators.Author;

public class GetAuthorDtoValidator : Validator<GetAuthorDetailsDto>
{
    public GetAuthorDtoValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
    }
}