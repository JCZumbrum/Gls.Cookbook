using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gls.Cookbook.Domain
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Value { get; set; }

        public Result()
        {
            this.Message = String.Empty;
        }

        public static Result<T> Pass(T value)
        {
            return new Result<T>() { Success = true, Value = value };
        }

        public static Result<T> Fail(string message)
        {
            return new Result<T>() { Success = false, Message = message };
        }
    }
}
