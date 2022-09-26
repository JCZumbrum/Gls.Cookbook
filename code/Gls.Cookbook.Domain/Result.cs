using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gls.Cookbook.Domain
{
    public class EmptyResultValue { }

    public class Result : Result<EmptyResultValue>
    {
        public static Result Pass()
        {
            return new Result() { Success = true, Value = new EmptyResultValue() };
        }

        public static new Result Fail(string message)
        {
            return new Result() { Success = false, Message = message };
        }
    }
}
