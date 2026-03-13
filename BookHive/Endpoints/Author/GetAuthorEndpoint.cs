using BookHive;
using BookHive.DTO.Author.Request;
using BookHive.DTO.Author.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Bookhive.Endpoints.Author;

public class GetAuthorEndpoint(BookHiveDbContext bookHiveDbContext) : Endpoint<GetAuthorDto, GetAuthorDetailsDto>
{
    public override void Configure()
    { 
        AllowAnonymous();
        Get ("/authors/{@Id}", x => new { x.Id });
    }

    public override async Task HandleAsync(GetAuthorDto req, CancellationToken ct)
    {
        BookHive.Models.Author? databaseAuthor = await bookHiveDbContext.Authors.SingleOrDefaultAsync(x => x.Id == req.Id, cancellationToken: ct);     
        
        if (databaseAuthor == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }
        
        GetAuthorDetailsDto dto = new()
        {
            Id = databaseAuthor.Id,
            LastName = databaseAuthor.LastName,
            FirstName = databaseAuthor.FirstName,
            BirthDate = databaseAuthor.BirthDate,
            Nationality = databaseAuthor.Nationality,
            Biography = databaseAuthor.Biography
        };
        
        await Send.OkAsync(dto, ct);
    }
}