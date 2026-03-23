using BookHive;
using BookHive.DTO.Book.Request;
using BookHive.DTO.Book.Response;
using FastEndpoints;
using BookHive.Models;

namespace Bookhive.Endpoints.Book;

public class DeleteBookEndpoint(BookHiveDbContext db, AutoMapper.IMapper mapper) : Endpoint<GetBookDto, GetBookDetailsDto>
{

    public override async Task HandleAsync(GetBookDto req, CancellationToken ct)
    {
        BookHive.Models.Book? book = mapper.Map<BookHive.Models.Book?>(req);
        db.Books.Remove(book);
        await db.SaveChangesAsync(ct);
        await Send.NoContentAsync(ct);
    }
}