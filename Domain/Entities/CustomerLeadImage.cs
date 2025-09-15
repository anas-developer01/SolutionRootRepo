using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CustomerLeadImage
    {
        public int Id { get; set; }


        // Store image as Base64 string (requirement)
        public string Base64Data { get; set; } = string.Empty;


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        // FK
        public int CustomerLeadId { get; set; }
        public CustomerLead? CustomerLead { get; set; }
    }
}
