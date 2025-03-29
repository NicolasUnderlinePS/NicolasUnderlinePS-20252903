namespace EmployeeUnitManagementDomain.Models.Responses
{
    public class CollaboratorAssociatedResponse
    {
        public int CollaboratorId { get; set; }
        public string FullName { get; set; }

        public int UnityId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
