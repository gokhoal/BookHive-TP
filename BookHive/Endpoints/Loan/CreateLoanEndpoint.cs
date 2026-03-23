using BookHive;
using BookHive.DTO.Loan.Request;
using FastEndpoints;

namespace Bookhive.Endpoints.Loan;

public class CreateLoanEndpoint(BookHiveDbContext db, AutoMapper.IMapper mapper) : Endpoint<CreateLoanRequestDto>
{
    public override async Task HandleAsync(CreateLoanRequestDto req, CancellationToken ct)
    {
        BookHive.Models.Loan? loan = mapper.Map<BookHive.Models.Loan>(req);
        db.Loans.Add(loan);
        await db.SaveChangesAsync(ct);
        await Send.NoContentAsync(ct);
    }
}