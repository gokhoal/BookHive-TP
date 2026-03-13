using BookHive;
using BookHive.DTO.Author.Request;
using BookHive.DTO.Author.Response;
using FastEndpoints;
using FluentValidation;

namespace Bookhive.Endpoints.Author;

public class CreateAuthorEndpoint(BookHiveDbContext bookhiveDbContext) : Endpoint<CreateAuthorRequestDto, GetAuthorDetailsDto>
{
    public override void Configure()
    {
        Post("/authors");
        AllowAnonymous();
    }

    public class CreateAuthorDtoValidator : Validator<CreateAuthorRequestDto>
    {
        public CreateAuthorDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100).MinimumLength(2);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(100).MinimumLength(2);
            RuleFor(x => x.Biography).MaximumLength(2000);
            RuleFor(x => x.BirthDate).NotEmpty().Must(x => x.DayOfYear < DateTime.Today.DayOfYear && x.Year < DateTime.Today.Year);
            RuleFor(x => x.Nationality).NotEmpty().MaximumLength(60);
        }
    }

    public override async Task HandleAsync(CreateAuthorRequestDto req, CancellationToken ct)
    {
         BookHive.Models.Author author = new()
        {
            FirstName = req.FirstName,
            LastName = req.LastName,
            Biography = req.Biography,
            BirthDate = req.BirthDate,
            Nationality = req.Nationality
            
        };
        bookhiveDbContext.Add(author);
        await bookhiveDbContext.SaveChangesAsync(ct);

        GetAuthorDetailsDto details = new()
        {
            Id = author.Id,
            LastName = author.LastName,
            FirstName = author.FirstName,
            Biography =  author.Biography,
            BirthDate = author.BirthDate,
            Nationality = author.Nationality
            
        };

        await Send.OkAsync(details, ct);

    }
}