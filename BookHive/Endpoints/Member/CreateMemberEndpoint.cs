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
    
    public class CreateMemberDtoValidator : Validator<CreateMemberRequestDto>
    {
        public CreateMemberDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(255)
                .WithMessage("L'email doit respecter le format demandé.");
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100).MinimumLength(2)
                .WithMessage("Le prénom doit comporter entre 2 et 100 caractères.");
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(100).MinimumLength(2)
                .WithMessage("Le nom doit comporter entre 2 et 100 caractères.");
            RuleFor(x => x.MembershipDate).NotEmpty()
                .Must(x => x.DayOfYear > DateTime.Today.DayOfYear && x.Year > DateTime.Today.Year);
        }
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