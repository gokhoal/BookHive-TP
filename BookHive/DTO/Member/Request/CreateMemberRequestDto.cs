using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookHive.DTO.Member.Request;

public class CreateMemberRequestDto
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly MembershipDate { get; set; }
    public bool IsActive { get; set; }
    
    public List<Navigation> Loans { get; set; }
    public List<Navigation> Reviews { get; set; }
}