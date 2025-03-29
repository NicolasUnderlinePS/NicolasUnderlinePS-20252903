namespace EmployeeUnitManagementDomain.Models.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
                
        }

        public User(bool? statusActive)
        {
            StatusActive = statusActive;
        }

        public string Login { get; set; }
        public string Password { get; set; }
        public bool? StatusActive { get; set; }

        public static int MaxLengthLogin
        {
            get => 100;
        }
        public static int MaxLengthPassword
        {
            get => 64;
        }
    }
}
