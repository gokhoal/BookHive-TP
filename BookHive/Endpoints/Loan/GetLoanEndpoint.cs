using BookHive;
using BookHive.DTO.Loan.Request;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Loan;

public class GetLoanEndpoint(BookHiveDbContext bookHiveDbContext) : Endpoint<GetLoanDto, GetLoanDto>
{
    public override void Configure()
    { 
        AllowAnonymous();
        Get("/loans/{@Id}", x => new { x.Id });
    }

    public override async Task HandleAsync(GetLoanDto req, CancellationToken ct)
    {
        BookHive.Models.Loan? databaseLoan = await bookHiveDbContext.Loans
            .SingleOrDefaultAsync(x => x.Id == req.Id, cancellationToken: ct);     
        
        if (databaseLoan == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }
        
        GetLoanDto dto = new()
        {
            Id = databaseLoan.Id,
            LoanDate = databaseLoan.LoanDate,
            DueDate = databaseLoan.DueDate,
            ReturnDate = databaseLoan.ReturnDate,
            BookId = databaseLoan.BookId,
            MemberId = databaseLoan.MemberId
        };
        
        await Send.OkAsync(dto, ct);
    }
}