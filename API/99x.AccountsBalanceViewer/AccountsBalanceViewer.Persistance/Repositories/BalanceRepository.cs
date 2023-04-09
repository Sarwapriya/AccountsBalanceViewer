using AccountsBalanceViewer.Application.Contracts.Persistance;
using AccountsBalanceViewer.Application.Features.AccountsBalance.Commands.AddAccountsBalance;
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
    public class BalanceRepository : Repository<Balance>, IBalanceRepository
    {
        #region Fields
        private readonly AccountsBalanceViewerContext _context;
        #endregion
        #region Constructor
        public BalanceRepository(AccountsBalanceViewerContext context) : base(context)
        {
            _context = context;
        }
        #endregion
        #region PublicMethods
        /// <summary>Gets the balance asynchronous.</summary>
        /// <param name="balanceRequest">The balance request.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public async Task<IList<GetAccountBalanceQueryVm>> GetBalanceAsync(GetAccountBalanceQuery balanceRequest)
        {

            var result = _context.Balances
                .Include(b => b.AccountType)
                .Where(b => (balanceRequest.Year == null && balanceRequest.Month == null) || b.Year == balanceRequest.Year && b.Month == balanceRequest.Month)
                .AsQueryable();
            return await result.Select(b => new GetAccountBalanceQueryVm
            {
                Id = b.Id,
                AccountType = b.AccountType.Name,
                Year = b.Year,
                Month = b.Month
            }).OrderBy(b => b.AccountType).ToListAsync();
        }

        /// <summary>Adds the balance asynchronous.</summary>
        /// <param name="request">The request.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public async Task<AddAccountBalanceCommandVm> AddBalanceAsync(AddAccountBalanceCommand request)
        {
            string AccountType = RemoveSpecialCharacters(request.AccountType.Trim().ToLower());
            DateTime current = DateTime.Now;
            _context.Balances.Add(new Balance()
            {
                AccountTypeId = await _context.AccountTypes.Where(a => a.Name.Trim().ToLower() == AccountType).Select(a => a.Id).FirstOrDefaultAsync(),
                Month = current.Month.ToString("MMMMMMMMMMMMM"),
                Year = current.Year.ToString(),
                Amount = request.Amount,
            });
            AddAccountBalanceCommandVm returnResult = new AddAccountBalanceCommandVm();
            _context.SaveChanges();
            var balanceResult = _context.Balances
                .Include(b => b.AccountType)
                .Where(b => b.Year == current.Year.ToString() && b.Month == current.Month.ToString("MMMMMMMMMMMMM") && b.AccountType.Name == AccountType)
                .AsQueryable().FirstOrDefaultAsync();
            returnResult.Id = balanceResult.Id;
            returnResult.AccountType = balanceResult.Result.AccountType.Name;
            returnResult.Year = balanceResult.Result.Year;
            returnResult.Month = balanceResult.Result.Month;
            return returnResult;
        }
        #endregion

        #region PrivateMethods
        public string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if (c.ToString() != "'")
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
        #endregion
    }
}
