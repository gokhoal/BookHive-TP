using BookHive;
using BookHive.DTO.Loan.Request;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Loan;

public class GetAllLoansEndpoint(BookHiveDbContext bookhiveDbContext) : EndpointWithoutRequest<List<GetLoanDto>>
{
    public override void Configure()
    {
        Get("/loans");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        List<GetLoanDto> loans = await bookhiveDbContext.Loans
            .Select(l => new GetLoanDto
            {
                Id = l.Id,
                LoanDate = l.LoanDate,
                DueDate = l.DueDate,
                ReturnDate = l.ReturnDate,
                BookId = l.BookId,
                MemberId = l.MemberId
            })
            .ToListAsync(ct);

        await Send.OkAsync(loans, ct);
    }
}