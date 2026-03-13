namespace BookHive.DTO.Loan.Response;

public class GetLoanDetailsDto
{
    public int Id { get; set; }
    public int? MemberId { get; set; }
    public int? BookId { get; set; }
    public DateOnly LoanDate { get; set; }
    public DateOnly DueDate { get; set; }
    
    public Models.Book Book { get; set; }
    public Models.Member Member { get; set; }
}