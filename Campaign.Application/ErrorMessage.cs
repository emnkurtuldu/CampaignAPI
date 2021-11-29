using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campaign.Application
{
    public class ErrorMessage
    {
        public string Message { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public string PropertyName { get; set; }

        public static ErrorMessage Create(string message, 
            string type,  
            string code 
        ) =>
            new() { Message = message, Type = type, Code = code };

        public static ErrorMessage Create(string message, 
            string type 
        ) =>
            new() { Message = message, Type = type };

        public static ErrorMessage Create(string message 
        ) =>
            new() { Message = message };

    }
}
