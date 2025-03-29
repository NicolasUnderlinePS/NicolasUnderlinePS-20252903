namespace EmployeeUnitManagementDomain.Models.Requests
{
    public class UserUpdateRequest
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public bool StatusActive { get; set; }
    }
}
