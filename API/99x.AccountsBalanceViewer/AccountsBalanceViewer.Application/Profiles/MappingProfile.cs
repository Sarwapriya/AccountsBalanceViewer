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
            CreateMap<Balance, AddAccountBalanceCommandVm>();
        }
    }
}
