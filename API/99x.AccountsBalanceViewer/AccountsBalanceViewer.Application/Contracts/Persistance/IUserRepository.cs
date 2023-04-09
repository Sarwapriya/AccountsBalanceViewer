using AccountsBalanceViewer.Application.Features.User.Queries.GetUser;
using AccountsBalanceViewer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsBalanceViewer.Application.Contracts.Persistance
{
    public interface IUserRepository : IRepository<User>
    {
        Task<GetUserQueryVm> GetUser(GetUserQuery getUser);
    }
}
