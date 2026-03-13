using BookHive.DTO.Member.Request;
using BookHive.DTO.Member.Response;
using FastEndpoints;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Member;

public class UpdateMemberEndpoint(BookHiveDbContext bookHiveDbContext) : Endpoint<UpdateMemberRequestDto, GetMemberDto>
{
    public override void Configure()
    { 
        Put("/members/{@Id}", x => new { x.Id });
        AllowAnonymous();
    }

    
    
    public override async Task HandleAsync(UpdateMemberRequestDto req, CancellationToken ct)
    {
        Models.Member? databaseMember = await bookHiveDbContext.Members
            .SingleOrDefaultAsync(x => x.Id == req.Id, cancellationToken: ct);

        if (databaseMember == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }
        else
        {
            databaseMember.Email = req.Email;
            databaseMember.FirstName = req.FirstName;
            databaseMember.LastName = req.LastName;
            databaseMember.MembershipDate = req.MembershipDate;
            databaseMember.IsActive = req.IsActive;
        }

        await bookHiveDbContext.SaveChangesAsync(ct);
        
        GetMemberDto dto = new()
        {
            Id = databaseMember.Id
        };
        
        await Send.OkAsync(dto, ct);
    }
}