using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Infrastructure
{
    public class CustomException(string? message) : Exception(message);

    public class RecordNotFoundException : CustomException
    {
        public RecordNotFoundException(string message = "No record found!", object? identifier = null) : base(message)
        {
            if (identifier is not null)
                Data.Add("identifier", identifier);
        }
    }

    public class EmailOrPasswordNotValidException(string message) : CustomException(message);
    public class ActionNotValidException(string? message) : CustomException(message);
    public class UserNotAuthenticatedException(string? message) : CustomException(message);
    public class UserNotAuthorizedException(string? message) : CustomException(message);
    public class NoDataUpdatedException(string? message) : CustomException(message);
    public class RecordAlreadyExistException(string? message): CustomException(message);
}
