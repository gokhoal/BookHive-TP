using BookHive.DTO.Loan.Request;
using FastEndpoints;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Loan;

public class UpdateLoanEndpoint(BookHiveDbContext bookHiveDbContext) : Endpoint<UpdateLoanRequestDto, GetLoanDto>
{
    public override void Configure()
    { 
        Put("/loans/{@Id}", x => new { x.Id });
        AllowAnonymous();
    }

    public class UpdateLoanDtoValidator : Validator<UpdateLoanRequestDto>
    {
        public UpdateLoanDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.BookId).GreaterThan(0);
            RuleFor(x => x.MemberId).GreaterThan(0);
            RuleFor(x => x.LoanDate).NotEmpty();
            RuleFor(x => x.DueDate)
                .Must((dto, dueDate) =>
                {
                    var dayOfWeek = dto.LoanDate.DayOfWeek;
                    var isWeekend = dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday;
                    var maxDays = isWeekend ? 14 : 30;
                    return dueDate <= dto.LoanDate.AddDays(maxDays);
                })
                .WithMessage("La durée max est de 14j (week-end) ou 30j (semaine).");
        }
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