using BookHive;
using Microsoft.EntityFrameworkCore;
using FastEndpoints;
using BookHive.DTO.Loan.Request;


namespace BookHive.Endpoints.Loan;

public class DeleteLoanEndpoint(BookHiveDbContext bookHiveDbContext) : Endpoint<GetLoanDto, GetLoanDto>
{
    public override void Configure()
    {

        Delete("/loans/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetLoanDto req, CancellationToken ct)
    {
        BookHive.Models.Loan? databaseLoan =
            await bookHiveDbContext.Loans
                .SingleOrDefaultAsync(x => x.Id == req.Id, ct);

        if (databaseLoan is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        bookHiveDbContext.Loans.Remove(databaseLoan);
        await bookHiveDbContext.SaveChangesAsync(ct);

        await Send.NoContentAsync(ct);
    }
}