using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookHive.DTO.Member.Response;

public class GetMemberDetailsDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly MembershipDate { get; set; }
    public bool IsActive { get; set; }
    
    public List<Models.Loan> Loans { get; set; }
    public List<Models.Review> Reviews { get; set; }
}