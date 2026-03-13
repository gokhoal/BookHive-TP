using BookHive.DTO.Loan.Request;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Loan;

public class UpdateLoanEndpoint(BookHiveDbContext bookHiveDbContext) : Endpoint<UpdateLoanRequestDto, GetLoanDto>
{
    public override void Configure()
    { 
        Put("/loans/{@Id}", x => new { x.Id });
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateLoanRequestDto req, CancellationToken ct)
    {
        Models.Loan? databaseLoan = await bookHiveDbContext.Loans
            .SingleOrDefaultAsync(x => x.Id == req.Id, cancellationToken: ct);

        if (databaseLoan == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }
        else
        {
            databaseLoan.LoanDate = req.LoanDate;
            databaseLoan.DueDate = req.DueDate;
        }

        await bookHiveDbContext.SaveChangesAsync(ct);
        
        GetLoanDto dto = new()
        {
            Id = databaseLoan.Id
        };
        
        await Send.OkAsync(dto, ct);
    }
}