using BookHive;
using BookHive.DTO.Member.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Member;

public class GetAllMembersEndpoint(BookHiveDbContext bookhiveDbContext) : EndpointWithoutRequest<List<GetMemberDetailsDto>>
{
    public override void Configure()
    {
        Get("/members");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        List<GetMemberDetailsDto> members = await bookhiveDbContext.Members
            .Select(m => new GetMemberDetailsDto
            {
                Id = m.Id,
                Email = m.Email,
                FirstName = m.FirstName,
                LastName = m.LastName,
                MembershipDate = m.MembershipDate,
                IsActive = m.IsActive
            })
            .ToListAsync(ct);

        await Send.OkAsync(members, ct);
    }
}