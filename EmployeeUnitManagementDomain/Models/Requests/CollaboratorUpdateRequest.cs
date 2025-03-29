using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeUnitManagementDomain.Models.Requests
{
    public class CollaboratorUpdateRequest
    {
        public int Id { get; set; }
        public string FullName { get; set; }        
        public int UnityId { get; set; }
    }
}
