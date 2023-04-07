using AccountsBalanceViewer.Application.Contracts.Persistance;
using AccountsBalanceViewer.Application.Features.AccountsBalance.Queries.GetAccountsBalance;
using AccountsBalanceViewer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsBalanceViewer.Persistance.Repositories
{
    public class BalanceRepository: Repository<Balance>, IBalanceRepository
    {
        #region Fields
        private readonly AccountsBalanceViewerContext _context;
        #endregion
        #region Constructor
        public BalanceRepository(AccountsBalanceViewerContext context): base(context)
        {
            _context= context;
        }
        #endregion
        #region PublicMethods
        public async Task<IList<GetAccountBalanceQueryVm>> GetBalanceAsync(GetAccountBalanceQuery balanceRequest)
        {

            var result = _context.Balances
                .Include(b => b.AccountType)
                .Where(b => (balanceRequest.Year == null && balanceRequest.Month == null) || b.Year == balanceRequest.Year && b.Month == balanceRequest.Month)
                .AsQueryable();
            return await result.Select(b=> new GetAccountBalanceQueryVm
            {
                Id = b.Id,
                AccountType = b.AccountType.Name,
                Year = b.Year,
                Month = b.Month
            }).OrderBy(b => b.AccountType).ToListAsync();
        }
        #endregion
    }
}
