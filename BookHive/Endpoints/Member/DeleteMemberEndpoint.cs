using BookHive;
using BookHive.DTO.Member.Request;
using BookHive.DTO.Member.Response;
using BookHive.DTO.Member.Request;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Member;

public class DeleteMemberEndpoint(BookHiveDbContext bookHiveDbContext) : Endpoint<GetMemberDto>
{
    public override void Configure()
    {
        
        Delete("/members/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetMemberDto req, CancellationToken ct)
    {
        BookHive.Models.Member? databaseMember =
            await bookHiveDbContext.Members
                .SingleOrDefaultAsync(x => x.Id == req.Id, ct);

        if (databaseMember is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        bookHiveDbContext.Members.Remove(databaseMember);
        await bookHiveDbContext.SaveChangesAsync(ct);

        await Send.NoContentAsync(ct);
    }
}