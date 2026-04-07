namespace PersonnelTrainingAPI.Models
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }

        public string Message { get; set; } = string.Empty;

        public bool IsSuccess { get; set; }

        public static ServiceResponse<T> Success(T data, string message = "")
        {
            return new ServiceResponse<T> { Data = data, Message = message, IsSuccess = true };
        }

        public static ServiceResponse<T> Fail(string message)
        {
            return new ServiceResponse<T> { Data = default, Message = message, IsSuccess = false };
        }
    }
}

