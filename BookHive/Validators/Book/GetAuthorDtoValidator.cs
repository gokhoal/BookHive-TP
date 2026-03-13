using BookHive.DTO.Book.Response;
using FastEndpoints;
using FluentValidation;

namespace BookHive.Validators.Book;

public class GetBookDtoValidator : Validator<GetBookDetailsDto>
{
    public GetBookDtoValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.ISBN).NotEmpty().Matches(@"^\d{13}$");
        RuleFor(x => x.Genre).NotEmpty();
        RuleFor(x => x.Author.LastName).NotEmpty();
    }
}