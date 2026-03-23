using AutoMapper;
using BookHive.DTO.Book.Request;
using BookHive.DTO.Member.Request;
using BookHive.DTO.Member.Response;
using BookHive.Models;

namespace BookHive.Profiles;

public class MemberProfile : Profile
{
    public MemberProfile()
    {
        CreateMap<Member, GetMemberDto>();
        CreateMap<Member, GetMemberDetailsDto>().ForMember(
            dest => dest.Loans,
            opt => opt.MapFrom(src => src.Loans)
            )
            .ForMember(
                dest => dest.Reviews,
                opt => opt.MapFrom(src => src.Reviews)
                );
        CreateMap<CreateMemberRequestDto, Member>();
        CreateMap<UpdateMemberRequestDto, Member>();
    }

}