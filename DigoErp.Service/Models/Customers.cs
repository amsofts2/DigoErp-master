using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigoErp.Service.Models
{
    public class Customers
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string TaxNumber { get; set; }
        public string Address { get; set; }
        public Nullable<bool> IsEnabled { get; set; }
        public string Refrence { get; set; }
        public Nullable<System.DateTime> Created_At { get; set; }
        public Nullable<System.DateTime> Updated_At { get; set; }
        public Nullable<int> BranchId { get; set; }
    }
}
