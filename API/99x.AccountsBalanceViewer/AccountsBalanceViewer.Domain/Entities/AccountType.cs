using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsBalanceViewer.Domain.Entities
{
    public class AccountType : BaseEntity
    {
        public string Name { get; set; }
        public decimal MinimumBalance { get; set; }
        public ICollection<Balance> Balances { get; set; }
    }
}
