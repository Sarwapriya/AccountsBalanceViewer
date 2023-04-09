using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsBalanceViewer.Application.Features.AccountsBalance.Commands.AddAccountsBalance
{
    public class AddAccountBalanceCommandVm
    {
        public long Id { get; set; }
        public string AccountType { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public decimal Amount { get; set; }
    }
}
