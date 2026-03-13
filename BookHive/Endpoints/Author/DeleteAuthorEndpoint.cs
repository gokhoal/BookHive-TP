using BookHive;
using BookHive.DTO.Author.Request;
using BookHive.DTO.Author.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Bookhive.Endpoints.Author;

public class DeleteAuthorEndpoint(BookHiveDbContext bookHiveDbContext) : Endpoint<GetAuthorDto, GetAuthorDetailsDto>
{
    public override void Configure()
    {
        Post("/authors/{@id}");
        AllowAnonymous();
    }


    public override async Task HandleAsync(GetAuthorDto req, CancellationToken ct)
    {
        BookHive.Models.Author? databaseAuthor = await bookHiveDbContext.Authors.SingleOrDefaultAsync(x => x.Id == req.Id, cancellationToken: ct);     
        BookHive.Models.Book? databaseBook = await bookHiveDbContext.Books.SingleOrDefaultAsync(x => x.Id == req.Id, cancellationToken: ct);     
        
        if (databaseAuthor == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }
        bookHiveDbContext.Authors.Remove(databaseAuthor);
        await bookHiveDbContext.SaveChangesAsync(ct);
        
        await Send.NoContentAsync(ct);
    }
}