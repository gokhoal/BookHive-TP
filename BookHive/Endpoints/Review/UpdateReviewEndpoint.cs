using BookHive.DTO.Review.Request;
using BookHive.DTO.Review.Response;
using FastEndpoints;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Review;

public class UpdateReviewEndpoint(BookHiveDbContext bookHiveDbContext) : Endpoint<UpdateReviewRequestDto, GetReviewDto>
{
    public override void Configure()
    { 
        Put("/reviews/{@Id}", x => new { x.Id });
        AllowAnonymous();
    }

    public class UpdateReviewDtoValidator : Validator<UpdateReviewRequestDto>
    {
        public UpdateReviewDtoValidator()
        {
            RuleFor(x => x.MemberId).GreaterThan(0);
            RuleFor(x => x.Rating).InclusiveBetween(1, 5);
            RuleFor(x => x.Comment).MaximumLength(1000);
        }
    }
    
    public override async Task HandleAsync(UpdateReviewRequestDto req, CancellationToken ct)
    {
        Models.Review? databaseReview = await bookHiveDbContext.Reviews
            .SingleOrDefaultAsync(x => x.Id == req.Id, cancellationToken: ct);

        if (databaseReview == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }
        else
        {
            databaseReview.Rating = req.Rating;
            databaseReview.Comment = req.Comment;
            databaseReview.CreatedAt = req.CreatedAt;
            databaseReview.BookId = req.BookId;
            databaseReview.MemberId = req.MemberId;
        }

        await bookHiveDbContext.SaveChangesAsync(ct);
        
        GetReviewDto dto = new()
        {
            Id = databaseReview.Id
        };
        
        await Send.OkAsync(dto, ct);
    }
}