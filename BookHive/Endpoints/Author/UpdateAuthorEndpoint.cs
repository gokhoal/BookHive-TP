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