using AccountsBalanceViewer.Application.Contracts.Persistance;
using MediatR;

namespace AccountsBalanceViewer.Application.Features.AccountsBalance.Queries.GetAccountsBalance
{
    public class GetAccountBalanceQueryHandler : IRequestHandler<GetAccountBalanceQuery, IList<GetAccountBalanceQueryVm>>
    {
        #region Fields
        private readonly IBalanceRepository _balanceRepository;
        #endregion
        #region Constructor
        public GetAccountBalanceQueryHandler(IBalanceRepository balanceRepository)
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
        public async Task<IList<GetAccountBalanceQueryVm>> Handle(GetAccountBalanceQuery request, CancellationToken token)
        {
            return await _balanceRepository.GetBalanceAsync(request);
        }
        #endregion
    }
}
