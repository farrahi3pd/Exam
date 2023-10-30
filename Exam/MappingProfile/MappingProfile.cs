using Exam.DTOs;
using Exam.Models;
using AutoMapper;

namespace Exam.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<User, CreateUserWalletDto>();
            CreateMap<CreateUserWalletDto, User>();


            CreateMap<User, TransferRequestDto>()
                .ForMember(dest => dest.SourceUsername,
                 opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.SourcePassword,
                 Opt => Opt.MapFrom(src => src.Password));
            CreateMap<TransferRequestDto, User>();


            CreateMap<User, LoanRequestDto>();
            CreateMap<LoanRequestDto, User>();
            CreateMap<LoanRequest, LoanRequestDto>()
                 .ForMember(dest => dest.LoanAmount,
                  opt => opt.MapFrom(src => src.RequestAmount));
            CreateMap<LoanRequestDto, LoanRequest>();

           









        }

    }
}
