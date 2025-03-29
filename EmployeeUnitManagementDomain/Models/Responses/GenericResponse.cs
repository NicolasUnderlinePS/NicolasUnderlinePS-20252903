namespace EmployeeUnitManagementDomain.Models.Responses
{
    public sealed class GenericResponse
    {
        public GenericResponse(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
        public GenericResponse()
        {
                
        }

        public bool IsSuccess { get; set; }
        public string Message { get; set; }


    }
}
