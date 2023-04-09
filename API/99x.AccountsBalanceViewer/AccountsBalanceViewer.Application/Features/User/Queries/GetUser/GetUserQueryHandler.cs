using AccountsBalanceViewer.Application.Contracts.Persistance;
using AccountsBalanceViewer.Application.Features.AccountsBalance.Queries.GetAccountsBalance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsBalanceViewer.Application.Features.User.Queries.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserQueryVm>
    {
        #region Fields
        private readonly IUserRepository _userRepository;
        #endregion

        #region Constructor
        public GetUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        #endregion

        #region Handler

        /// <summary>Handles the specified request.</summary>
        /// <param name="request">The request.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public async Task<GetUserQueryVm> Handle(GetUserQuery request, CancellationToken token)
        {
            return await _userRepository.GetUser(request);
        }
        #endregion
    }
}
