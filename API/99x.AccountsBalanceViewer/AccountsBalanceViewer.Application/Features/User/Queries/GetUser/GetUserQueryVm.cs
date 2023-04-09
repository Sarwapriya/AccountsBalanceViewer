using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsBalanceViewer.Application.Features.User.Queries.GetUser
{
    public class GetUserQueryVm
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}
