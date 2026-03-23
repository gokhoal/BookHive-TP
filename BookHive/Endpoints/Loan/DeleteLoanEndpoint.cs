using BookHive;
using Microsoft.EntityFrameworkCore;
using FastEndpoints;
using BookHive.DTO.Loan.Request;
using BookHive.DTO.Loan.Response;


namespace BookHive.Endpoints.Loan;

public class DeleteLoanEndpoint(BookHiveDbContext db, AutoMapper.IMapper mapper) : Endpoint<GetLoanDto, GetLoanDetailsDto>
{

    public override async Task HandleAsync(GetLoanDto req, CancellationToken ct)
    {
        BookHive.Models.Loan? loan = mapper.Map<BookHive.Models.Loan?>(req);
        db.Loans.Remove(loan);
        await db.SaveChangesAsync(ct);
        await Send.NoContentAsync(ct);
    }
}