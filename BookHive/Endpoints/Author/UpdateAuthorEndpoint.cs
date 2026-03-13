using BookHive.DTO.Author.Request;
using BookHive.DTO.Author.Response;
using FastEndpoints;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Author;

public class UpdateAuthorEndpoint(BookHiveDbContext bookHiveDbContext) : Endpoint <UpdateAuthorRequestDto, GetAuthorDto>
{
    public override void Configure()
    { 
        Put ("/authors/{@Id}", x => new { x.Id });
        AllowAnonymous();
    }

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
    
    public override async Task HandleAsync(UpdateAuthorRequestDto req, CancellationToken ct)
    {
        Models.Author? databaseAuthor = await bookHiveDbContext.Authors.SingleOrDefaultAsync(x => x.Id == req.Id, cancellationToken: ct);

        if (databaseAuthor == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }
        else
        {
            databaseAuthor.FirstName = req.FirstName;
            databaseAuthor.LastName = req.LastName;
            databaseAuthor.Biography = req.Biography;
            databaseAuthor.BirthDate = req.BirthDate;
            databaseAuthor.Nationality = req.Nationality;
        }
        await bookHiveDbContext.SaveChangesAsync(ct);
        
        GetAuthorDto dto = new()
        {
            Id = databaseAuthor.Id
        };
        
        await Send.OkAsync(dto, ct);
    }
}