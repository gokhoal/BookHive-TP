using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookHive.DTO.Loan.Request;

public class UpdateLoanRequestDto
{
    public int Id { get; set; }
    public int? MemberId { get; set; }
    public int? BookId { get; set; }
    public DateOnly LoanDate { get; set; }
    public DateOnly DueDate { get; set; }
    
    public Navigation Book { get; set; }
    public Navigation Member { get; set; }
}