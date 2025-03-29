namespace EmployeeUnitManagementDomain.Models.Entities
{
    public class Collaborator : BaseEntity
    {
        public string FullName { get; set; }
        public int UserId { get; set; }
        public int UnityId { get; set; }

        public static int MaxLengthFullName { 
            get => 100;
        }
    }
}
