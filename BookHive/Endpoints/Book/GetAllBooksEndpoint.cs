namespace BookHive.Endpoints.Book;
using BookHive.DTO.Book.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;


public class GetAllBooksEndpoint(BookHiveDbContext bookHiveDbContext) : EndpointWithoutRequest<List<GetBookDetailsDto>>
{
    public override void Configure()
    {
        Get ("/books");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        List<GetBookDetailsDto> books= await bookHiveDbContext.Books.Select(x => new GetBookDetailsDto()
            {
                Id = x.Id
            
            })
            .ToListAsync(ct);
        
        await Send.OkAsync(books, ct);
    }
}