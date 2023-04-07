using AccountsBalanceViewer.Application.Features.AccountsBalance.Queries.GetAccountsBalance;
using AccountsBalanceViewer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsBalanceViewer.Application.Contracts.Persistance
{
    public interface IBalanceRepository :IRepository<Balance>
    {
        Task<IList<GetAccountBalanceQueryVm>> GetBalanceAsync(GetAccountBalanceQuery request);
    }
}
