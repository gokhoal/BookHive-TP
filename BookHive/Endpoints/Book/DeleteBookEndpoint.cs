using BookHive;
using BookHive.DTO.Book.Request;
using BookHive.DTO.Book.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Bookhive.Endpoints.Book;

public class DeleteBookEndpoint(BookHiveDbContext bookHiveDbContext) : Endpoint<GetBookDto, GetBookDetailsDto>
{
    public override void Configure()
    {
        
        Post("/books/{@id}");
        AllowAnonymous();
    }


    public override async Task HandleAsync(GetBookDto req, CancellationToken ct)
    {
        BookHive.Models.Book? databaseBook = await bookHiveDbContext.Books.SingleOrDefaultAsync(x => x.Id == req.Id, cancellationToken: ct); 
        BookHive.Models.Loan? databaseLoan = await bookHiveDbContext.Loans.SingleOrDefaultAsync(x => x.BookId == req.Id, cancellationToken: ct); 
        
        if (databaseBook == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }
        
        if (databaseLoan == null)
        {
            bookHiveDbContext.Books.Remove(databaseBook);
            await bookHiveDbContext.SaveChangesAsync(ct);
        }
        
        await Send.NoContentAsync(ct);
    }
}