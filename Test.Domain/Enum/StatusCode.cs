using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Domain.Enum
{
    public enum StatusCode
    {
        //User
        UserNotFound = 0,
        ContactNotFound = 10,
        UserAlreadyExist = 20,
        OK = 200,
        //NotFound = 404,
        InternalServerError = 500,
    }
}
