using System.Text.Json.Serialization;

namespace EmployeeUnitManagementDomain.Models.Entities
{
    public class Unity : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool StatusActive { get; set; }

        public static int MaxLengthCode
        {
            get => 64;
        }

        public static int MaxLengthName
        {
            get => 100;
        }
    }
}
