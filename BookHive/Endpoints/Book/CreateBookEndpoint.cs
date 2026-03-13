using BookHive;
using BookHive.DTO.Book.Request;
using BookHive.DTO.Book.Response;
using BookHive.DTO.Book.Response;
using FastEndpoints;
namespace BookHive.Endpoints.Book;

public class CreateBookEndpoint(BookHiveDbContext bookhiveDbContext) : Endpoint<CreateBookRequestDto, GetBookDetailsDto>
{
    public override void Configure()
    {
        
        Post("/books");
        AllowAnonymous();
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