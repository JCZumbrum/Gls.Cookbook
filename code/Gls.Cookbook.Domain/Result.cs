using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gls.Cookbook.Domain
{
    public class Result
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public Result()
        {
            this.Message = String.Empty;
        }

        public static Result Pass()
        {
            return new Result() { Success = true };
        }

        public static Result Fail(string message)
        {
            return new Result() { Success = false, Message = message };
        }
    }
}
