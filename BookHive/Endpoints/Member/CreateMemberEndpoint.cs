using System.Text.RegularExpressions;
using BookHive;
using BookHive.DTO.Member.Request;
using BookHive.DTO.Member.Response;
using FastEndpoints;
using FluentValidation;

namespace BookHive.Endpoints.Member;

public class CreateMemberEndpoint(BookHiveDbContext bookhiveDbContext) : Endpoint<CreateMemberRequestDto, GetMemberDetailsDto>
{
    public override void Configure()
    {
        
        Post("/members");
        AllowAnonymous();
    }
    
    

    public override async Task HandleAsync(CreateMemberRequestDto req, CancellationToken ct)
    {
    BookHive.Models.Member member = new()
        {
            Email = req.Email,
            FirstName = req.FirstName,
            LastName = req.LastName,
            MembershipDate = req.MembershipDate,
            IsActive = req.IsActive
        };

        bookhiveDbContext.Add(member);
        await bookhiveDbContext.SaveChangesAsync(ct);

        GetMemberDetailsDto details = new()
        {
            Id = member.Id,
            Email = member.Email,
            FirstName = member.FirstName,
            LastName = member.LastName,
            MembershipDate = member.MembershipDate,
            IsActive = member.IsActive
        };

        await Send.OkAsync(details, ct);
    }
    }