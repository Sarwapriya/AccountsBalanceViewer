using AccountsBalanceViewer.Application.Features.AccountsBalance.Commands.AddAccountsBalance;
using AccountsBalanceViewer.Application.Features.AccountsBalance.Queries.GetAccountsBalance;
using AccountsBalanceViewer.Domain.Entities;

namespace AccountsBalanceViewer.Application.Contracts.Persistance
{
    public interface IBalanceRepository :IRepository<Balance>
    {
        Task<IList<GetAccountBalanceQueryVm>> GetBalanceAsync(GetAccountBalanceQuery request);
        Task<AddAccountBalanceCommandVm> AddBalanceAsync(AddAccountBalanceCommand request);
        string RemoveSpecialCharacters(string str);
    }
}
