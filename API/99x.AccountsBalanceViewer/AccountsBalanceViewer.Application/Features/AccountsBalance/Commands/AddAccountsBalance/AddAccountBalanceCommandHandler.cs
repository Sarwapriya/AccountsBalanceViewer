using AccountsBalanceViewer.Application.Contracts.Persistance;
using MediatR;
using System.Collections.Generic;

namespace AccountsBalanceViewer.Application.Features.AccountsBalance.Commands.AddAccountsBalance
{
    public class AddAccountBalanceCommandHandler : IRequestHandler<List<AddAccountBalanceCommand>, IList<AddAccountBalanceCommandVm>>
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
        /// <summary>Handles the specified request.</summary>
        /// <param name="request">The request.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public async Task<IList<AddAccountBalanceCommandVm>> Handle(List<AddAccountBalanceCommand> request, CancellationToken token)
        {
            return await _balanceRepository.AddBalanceAsync(request);
        }
        #endregion
    }
}
