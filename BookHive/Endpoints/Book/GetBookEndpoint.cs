using BookHive.DTO.Book.Request;
using BookHive.DTO.Book.Response;
using FastEndpoints;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Book;

public class GetBookEndpoint(BookHiveDbContext bookHiveDbContext) : Endpoint<GetBookDto, GetBookDetailsDto>
{
    public override void Configure()
    { 
        AllowAnonymous();
        Get ("/books/{@Id}", x => new { x.Id });
    }
    
    

    public override async Task HandleAsync(GetBookDto req, CancellationToken ct)
    {
        BookHive.Models.Book? databaseBook = await bookHiveDbContext.Books.SingleOrDefaultAsync(x => x.Id == req.Id, cancellationToken: ct);     
        
        if (databaseBook == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }
        
        GetBookDetailsDto dto = new()
        {
            Id = databaseBook.Id,
            Title = databaseBook.Title,
            ISBN = databaseBook.ISBN,
            Summary =  databaseBook.Summary,
            PageCount = databaseBook.PageCount,
            PublishedDate = databaseBook.PublishedDate,
            Genre = databaseBook.Genre,
            AuthorFullName = $"{databaseBook.Author.FirstName} {databaseBook.Author.LastName}",
            AuthorId = databaseBook.AuthorId,
        };
        
        await Send.OkAsync(dto, ct);
    }
}