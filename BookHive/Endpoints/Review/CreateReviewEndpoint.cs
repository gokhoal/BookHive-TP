using BookHive;
using BookHive.DTO.Review.Request;
using BookHive.DTO.Review.Response;
using FastEndpoints;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BookHive.Endpoints.Review;

public class CreateReviewEndpoint(BookHiveDbContext bookHiveDbContext) : Endpoint<CreateReviewRequestDto, GetReviewDetailsDto>
{
    public override void Configure()
    {
        
        Post("/books/{@BookId}/reviews");
        AllowAnonymous();
    }
    
    public class CreateReviewDtoValidator : Validator<CreateReviewRequestDto>
    {
        public CreateReviewDtoValidator()
        {
            RuleFor(x => x.MemberId).GreaterThan(0);
            RuleFor(x => x.Rating).InclusiveBetween(1, 5);
            RuleFor(x => x.Comment).MaximumLength(1000);
        }
    }

    public override async Task HandleAsync(CreateReviewRequestDto req, CancellationToken ct)
    {
        BookHive.Models.Book? databaseBook = await bookHiveDbContext.Books.SingleOrDefaultAsync(x => x.Id == req.BookId, cancellationToken: ct);
        BookHive.Models.Member? databaseMember = await bookHiveDbContext.Members.SingleOrDefaultAsync(x => x.Id == req.MemberId, cancellationToken: ct);
        
        BookHive.Models.Review review = new()
        {
            Rating = req.Rating,
            Comment = req.Comment,
            CreatedAt = req.CreatedAt,
            BookId = req.BookId,
            MemberId = req.MemberId
        };

        if (databaseBook is null)
        {
            if (databaseMember.IsActive)
            {
                bookHiveDbContext.Add(review);
                await bookHiveDbContext.SaveChangesAsync(ct);
            }
        }

        GetReviewDetailsDto details = new()
        {
            Id = review.Id,
            Rating = review.Rating,
            Comment = review.Comment,
            CreatedAt = review.CreatedAt,
            BookId = review.BookId,
            MemberId = review.MemberId
        };

        await Send.OkAsync(details, ct);
    }
}