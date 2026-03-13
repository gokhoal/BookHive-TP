using System.Data;
using System.Text.RegularExpressions;
using BookHive;
using BookHive.DTO.Book.Request;
using BookHive.DTO.Book.Response;
using BookHive.DTO.Book.Response;
using FastEndpoints;
using FluentValidation;

namespace BookHive.Endpoints.Book;

public class CreateBookEndpoint(BookHiveDbContext bookhiveDbContext) : Endpoint<CreateBookRequestDto, GetBookDetailsDto>
{
    public override void Configure()
    {
        Post("/books");
        AllowAnonymous();
    }

    public class CreateBookDtoValidator : Validator<CreateBookRequestDto>
    {
        public CreateBookDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Le titre est obligatoire.")
                .MaximumLength(200).WithMessage("Le titre ne peut pas dépasser 200 caractères.");
            RuleFor(x => x.ISBN)
                .NotEmpty().WithMessage("L’ISBN est obligatoire.")
                .Matches(@"^\d{13}$")
                .WithMessage("L’ISBN doit contenir exactement 13 chiffres.");
            RuleFor(x => x.PageCount)
                .GreaterThan(0).WithMessage("Le nombre de pages doit être supérieur à 0.");
            RuleFor(x => x.PublishedDate)
                .NotEmpty().WithMessage("La date de publication est obligatoire.")
                .Must(d => d <= DateOnly.FromDateTime(DateTime.Now))
                .WithMessage("La date de publication ne peut pas être dans le futur.");
            RuleFor(x => x.Genre)
                .NotEmpty().WithMessage("Le genre est obligatoire.")
                .MaximumLength(50).WithMessage("Le genre ne peut pas dépasser 50 caractères.");
            RuleFor(x => x.AuthorId)
                .GreaterThan(0).WithMessage("L’identifiant de l’auteur est invalide.");
            When(x => x.Summary != null, () =>
            {
                RuleFor(x => x.Summary)
                    .MaximumLength(3000).WithMessage("Le résumé ne peut pas dépasser 3000 caractères.");
            });
        }
    }

    public override async Task HandleAsync(CreateBookRequestDto req, CancellationToken ct)
    {
        BookHive.Models.Book book = new()
        {
            Title = req.Title,
            ISBN = req.ISBN,
            Summary = req.Summary,
            PageCount = req.PageCount,
            PublishedDate = req.PublishedDate,
            Genre = req.Genre,
            AuthorId = req.AuthorId
        };
        bookhiveDbContext.Add(book);
        await bookhiveDbContext.SaveChangesAsync(ct);

        GetBookDetailsDto details = new()
        {
            Id = book.Id,
            Title = book.Title,
            ISBN = book.ISBN,
            Summary =  book.Summary,
            PageCount = book.PageCount,
            PublishedDate = book.PublishedDate,
            Genre = book.Genre,
            AuthorId = book.AuthorId
        };

        await Send.OkAsync(details, ct);

    }
}