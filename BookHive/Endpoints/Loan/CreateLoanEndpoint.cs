using BookHive;
using BookHive.DTO.Loan.Request;
using FastEndpoints;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Bookhive.Endpoints.Loan;

public class CreateLoanEndpoint(BookHiveDbContext bookHiveDbContext) : Endpoint<CreateLoanRequestDto, GetLoanDto>
{
    public override void Configure()
    {

        Post("/loans");
        AllowAnonymous();
    }

    public class CreateLoanDtoValidator : Validator<CreateLoanRequestDto>
    {
        public CreateLoanDtoValidator()
        {
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
    
    public override async Task HandleAsync(CreateLoanRequestDto req, CancellationToken ct)
    {
        BookHive.Models.Book? databaseBook = await bookHiveDbContext.Books.SingleOrDefaultAsync(x => x.Id == req.BookId, cancellationToken: ct);
        BookHive.Models.Member? databaseMember = await bookHiveDbContext.Members.SingleOrDefaultAsync(x => x.Id == req.MemberId, cancellationToken: ct);
        
        BookHive.Models.Loan loan = new()
        {
            BookId = req.BookId,
            MemberId = req.MemberId,
            DueDate= req.LoanDate.AddDays(25),
            ReturnDate = req.ReturnDate,
            LoanDate = req.LoanDate,
            
        };

        if (databaseBook != null)
        {
            if (databaseMember.IsActive == true)
            {
                bookHiveDbContext.Add(loan);
                await bookHiveDbContext.SaveChangesAsync(ct);
            }
        }


        GetLoanDto dto = new()
        {
            Id = loan.Id,
            MemberId = loan.MemberId,
            BookId = loan.BookId,
            DueDate = loan.DueDate,
            LoanDate = loan.LoanDate,
            ReturnDate = loan.ReturnDate
        };

        await Send.OkAsync(dto, ct);

    }
}