using BookHive.DTO.Book.Request;
using BookHive.DTO.Book.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Book;

public class UpdateBookEndpoint(BookHiveDbContext bookHiveDbContext) : Endpoint <UpdateBookRequestDto, GetBookDto>
{
    public override void Configure()
    { 
        Put ("/books/{@Id}", x => new { x.Id });
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateBookRequestDto req, CancellationToken ct)
    {
        Models.Book? databaseBook = await bookHiveDbContext.Books.SingleOrDefaultAsync(x => x.Id == req.Id, cancellationToken: ct);

        if (databaseBook == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }
        else
        {
            databaseBook.Title = req.Title;
            databaseBook.ISBN = req.ISBN;
            databaseBook.Summary = req.Summary;
            databaseBook.PageCount = req.PageCount;
            databaseBook.Genre = req.Genre;
            databaseBook.PublishedDate = req.PublishedDate;
            databaseBook.AuthorId = req.AuthorId;
        }
        await bookHiveDbContext.SaveChangesAsync(ct);
        
        GetBookDto dto = new()
        {
            Id = databaseBook.Id
        };
        
        await Send.OkAsync(dto, ct);
    }
}