using BookHive;
using BookHive.DTO.Loan.Request;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using BookHive.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Bookhive.Endpoints.Loan;

public class PatchLoanRequest
{
    public int Id { get; set; }
    public DateOnly? ReturnDate { get; set; }
}

public class PatchLoanEndpoint(BookHiveDbContext bookHiveDbContext) : Endpoint <PatchLoanRequest ,GetLoanDto>
{
    
    public override void Configure()
    { 
        Patch ("/loans/{@Id}/return", x => new { x.Id });
        AllowAnonymous();
    }

    public async Task HandleAsync(PatchLoanRequest req, CancellationToken ct)
    {
        BookHive.Models.Loan? databaseLoan = await bookHiveDbContext.Loans.SingleOrDefaultAsync(x => x.Id == req.Id, cancellationToken: ct);

        if (databaseLoan == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }
        else
        {
            databaseLoan.ReturnDate =  req.ReturnDate;
        }
        await bookHiveDbContext.SaveChangesAsync(ct);
        
        GetLoanDto dto = new()
        {
            Id = databaseLoan.Id,
            ReturnDate = databaseLoan.ReturnDate
        };
        
        await Send.OkAsync(dto, ct);
    }
}