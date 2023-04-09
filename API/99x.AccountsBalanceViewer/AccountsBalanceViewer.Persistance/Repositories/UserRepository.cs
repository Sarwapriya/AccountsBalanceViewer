using AccountsBalanceViewer.Application.Contracts.Persistance;
using AccountsBalanceViewer.Application.Features.User.Queries.GetUser;
using AccountsBalanceViewer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsBalanceViewer.Persistance.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        #region Fields
        private readonly AccountsBalanceViewerContext _context;
        #endregion
        #region Constructor
        public UserRepository(AccountsBalanceViewerContext context) : base(context)
        {
            _context = context;
        }
        #endregion
        #region Public Methods
        public async Task<GetUserQueryVm> GetUser(GetUserQuery user)
        {
            var result = _context.Users
           .Include(u => u.UserRoles)
           .Where(u => (user.Email == null) || u.Email == user.Email)
           .AsQueryable();

            GetUserQueryVm getUserQueryVm = new GetUserQueryVm();
            if (result != null)
            {
                getUserQueryVm.Id = Convert.ToInt64(result.Select(u => u.Id));
                getUserQueryVm.Email = result.Select(u => u.Email).ToString() ?? string.Empty;
                getUserQueryVm.IsAdmin = false;
                if (await result.Select(u => u.UserRoles.Name).FirstOrDefaultAsync() == "Admin")
                {
                    getUserQueryVm.IsAdmin = true;
                }
            }
            else
            {
                User newUser = new User();
                newUser.Email = result.Select(u => u.Email).ToString();
                if (user.IsAdmin == true)
                {
                    newUser.RoleId = await _context.UserRoles.Where(r => r.Name == "Admin").Select(r=> r.Id).FirstOrDefaultAsync();
                }
                _context.Users.Add(newUser);
                _context.SaveChanges();

                getUserQueryVm.Id = newUser.Id;
                getUserQueryVm.Email = newUser.Email.ToString() ?? string.Empty;
                getUserQueryVm.IsAdmin = false;
                if (user.IsAdmin == true)
                {
                    getUserQueryVm.IsAdmin = true;
                }
            }
            return getUserQueryVm;
        }
        #endregion
    }
}
