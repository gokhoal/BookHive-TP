using BookHive.DTO.Book.Request;
using BookHive.DTO.Member.Request;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookHive.DTO.Loan.Request;

public class GetLoanDto
{
    public int Id { get; set; }
    public int? MemberId { get; set; }
    public int? BookId { get; set; }
    public DateOnly LoanDate { get; set; }
    public DateOnly DueDate { get; set; }
    public DateOnly? ReturnDate { get; set; }
    
    public GetBookDto? Book { get; set; }
    public GetMemberDto? Member { get; set; }
}