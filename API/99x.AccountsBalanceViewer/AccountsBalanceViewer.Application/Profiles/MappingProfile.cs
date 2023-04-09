using AccountsBalanceViewer.Application.Features.AccountsBalance.Commands.AddAccountsBalance;
using AccountsBalanceViewer.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsBalanceViewer.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Balance, AddAccountBalanceCommandVm>()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                .ForMember(dest => dest.AccountType, act => act.MapFrom(src => src.AccountType.Name))
                .ForMember(dest => dest.Year, act => act.MapFrom(src => src.Year))
                .ForMember(dest => dest.Month, act => act.MapFrom(src => src.Month))
                .ForMember(dest => dest.Amount, act => act.MapFrom(src => src.Amount));

        }
    }
}
