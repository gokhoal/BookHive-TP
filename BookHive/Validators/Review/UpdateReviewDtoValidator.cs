using BookHive.DTO.Review.Request;
using FastEndpoints;
using FluentValidation;

namespace BookHive.Validators.Review;

public class UpdateReviewDtoValidator : Validator<UpdateReviewRequestDto>
{
    public UpdateReviewDtoValidator()
    {
        RuleFor(x => x.MemberId).GreaterThan(0);
        RuleFor(x => x.Rating).InclusiveBetween(1, 5);
        RuleFor(x => x.Comment).MaximumLength(1000);
    }
}