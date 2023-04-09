using AccountsBalanceViewer.Application.Contracts.Persistance;
using MediatR;
using System.Collections.Generic;

namespace AccountsBalanceViewer.Application.Features.AccountsBalance.Commands.AddAccountsBalance
{
    public class AddAccountBalanceCommandHandler : IRequestHandler<IList<AddAccountBalanceCommand>, IList<AddAccountBalanceCommandVm>>
    {
        #region Fileds
        private readonly IBalanceRepository _balanceRepository;
        #endregion
        #region Constructor
        public AddAccountBalanceCommandHandler(IBalanceRepository balanceRepository)
        {
            _balanceRepository = balanceRepository;
        }
        #endregion
        #region Handler
        public async Task<IList<AddAccountBalanceCommandVm>> Handle(IList<AddAccountBalanceCommand> request, CancellationToken token)
        {
            return await _balanceRepository.AddBalanceAsync(request);
        }
        #endregion
    }
}
