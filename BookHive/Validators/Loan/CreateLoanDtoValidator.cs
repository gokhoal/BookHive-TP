using BookHive.DTO.Loan.Request;
using FastEndpoints;
using FluentValidation;

namespace BookHive.Validators.Loan;

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