using AccountsBalanceViewer.Application.Features.AccountsBalance.Queries.GetAccountsBalance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsBalanceViewer.Application.Features.AccountsBalance.Commands.AddAccountsBalance
{
    public class AddAccountBalanceCommand : IRequest<IList<AddAccountBalanceCommandVm>>
    {
        public string AccountType { get; set; }
        public decimal Amount { get; set; }
    }
}
