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

    public class UpdateMemberDtoValidator : Validator<UpdateMemberRequestDto>
    {
        public UpdateMemberDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
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