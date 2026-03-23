using AutoMapper;
using BookHive.DTO.Loan.Request;
using BookHive.Models;

namespace BookHive.Profiles;

public class LoanProfile : Profile
{
    public LoanProfile()
    {
        CreateMap<Loan, GetLoanDto>().ForMember(
                dest => dest.Book.Title,
                opt => opt.MapFrom(src => src.Book.Title)
            )
            .ForMember(
                dest => dest.Member,
                opt => opt.MapFrom(src => $"{src.Member.FirstName} {src.Member.LastName}"));
        CreateMap<CreateLoanRequestDto, Loan>();
    }
}