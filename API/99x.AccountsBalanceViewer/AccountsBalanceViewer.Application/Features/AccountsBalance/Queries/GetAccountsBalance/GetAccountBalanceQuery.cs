using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsBalanceViewer.Application.Features.AccountsBalance.Queries.GetAccountsBalance
{
    public class GetAccountBalanceQuery: IRequest<IList<GetAccountBalanceQueryVm>>
    {
        public string Year { get; set; }
        public string Month { get; set; }
    }
}
