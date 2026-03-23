using BookHive.DTO.Book.Request;
using FastEndpoints;

namespace BookHive.Endpoints.Book;

public class CreateBookEndpoint(BookHiveDbContext db, AutoMapper.IMapper mapper) : Endpoint<CreateBookRequestDto>
{
    public override async Task HandleAsync(CreateBookRequestDto req, CancellationToken ct)
    {
        Models.Book? book = mapper.Map<Models.Book>(req);
        db.Books.Add(book);
        await db.SaveChangesAsync(ct);
        await Send.NoContentAsync(ct);
    }
}
