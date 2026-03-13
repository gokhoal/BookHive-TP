using BookHive;
using BookHive.DTO.Member.Request;
using BookHive.DTO.Member.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Member;

public class GetMemberEndpoint(BookHiveDbContext bookHiveDbContext) : Endpoint<GetMemberDto, GetMemberDetailsDto>
{
    public override void Configure()
    { 
        AllowAnonymous();
        Get("/members/{@Id}", x => new { x.Id });
    }

    public override async Task HandleAsync(GetMemberDto req, CancellationToken ct)
    {
        BookHive.Models.Member? databaseMember = await bookHiveDbContext.Members
            .SingleOrDefaultAsync(x => x.Id == req.Id, cancellationToken: ct);     
        
        if (databaseMember == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }
        
        GetMemberDetailsDto dto = new()
        {
            Id = databaseMember.Id,
            Email = databaseMember.Email,
            FirstName = databaseMember.FirstName,
            LastName = databaseMember.LastName,
            MembershipDate = databaseMember.MembershipDate,
            IsActive = databaseMember.IsActive
        };
        
        await Send.OkAsync(dto, ct);
    }
}