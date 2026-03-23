using AutoMapper;
using BookHive.DTO.Review.Request;
using BookHive.Models;

namespace BookHive.Profiles;

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        CreateMap<Review, GetReviewDto>().ForMember(
            dest => dest.Member,
            opt => opt.MapFrom(src => $"{src.Member.FirstName} {src.Member.LastName}"));
        CreateMap<CreateReviewRequestDto, Review>();
    }
}