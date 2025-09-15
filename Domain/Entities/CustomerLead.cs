using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CustomerLead
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;


        // Navigation
        public ICollection<CustomerLeadImage> Images { get; set; } = new List<CustomerLeadImage>();
    }
}
