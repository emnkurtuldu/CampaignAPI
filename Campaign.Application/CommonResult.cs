using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Campaign.Application
{
    public class CommonResult<T> where T : class
    {
        public int? HttpStatusCode { get; set; }

        public bool Result => !IsError;

        public T Data { get; internal set; }
        public List<ErrorMessage> ErrorMessages { get; private set; }

        public bool IsError
        {
            get
            {
                return (ErrorMessages != null && ErrorMessages.Count > 0);
            }
        }

        public CommonResult(T data, 
                                int? httpStatusCode = null,
                                List<ErrorMessage> errorMessages = null
                                )
        {
            Data = data;
            HttpStatusCode = httpStatusCode;
            ErrorMessages = errorMessages ?? new List<ErrorMessage>();
        }

        public string ErrorsWithCode(string lineBreak = "\n")
        {
            StringBuilder bld = new StringBuilder();
            foreach (ErrorMessage error in ErrorMessages)
                bld.Append((string.IsNullOrEmpty(bld.ToString()) ? "" : lineBreak) + error.Code + " - " + error.Message);
            return bld.ToString();
        }

        public string Errors(string lineBreak = "\n")
        {
            StringBuilder bld = new StringBuilder();
            foreach (ErrorMessage error in ErrorMessages)
                bld.Append((string.IsNullOrEmpty(bld.ToString()) ? "" : lineBreak) + error.Message);
            return bld.ToString();
        }

        public void AddErrors(params string[] errorMessages) =>
            ErrorMessages = errorMessages.Select(ErrorMessage.Create).ToList();

        public static CommonResult<T> CreateError(string errorType = null, string errorCode = null, params string[] validationErrors) =>
            new(null, (int)System.Net.HttpStatusCode.BadRequest,
                validationErrors.Select(p => ErrorMessage.Create(p, errorType, errorCode)).ToList());

        public static CommonResult<T> CreateError(params string[] validationErrors) =>
            new(null, (int)System.Net.HttpStatusCode.BadRequest, validationErrors.Select(ErrorMessage.Create).ToList());

        public static CommonResult<T> CreateError(List<ErrorMessage> errors) =>
            new(null, (int)System.Net.HttpStatusCode.BadRequest, errors.Select(p => ErrorMessage.Create(p.Message, p.Type, p.Code)).ToList());

        public static CommonResult<T> CreateError(HttpStatusCode httpStatusCode, /*!< HttpStatusCode */
                                                    params string[] errorMessages) =>
            new(null, (int)httpStatusCode,
                errorMessages.Select(ErrorMessage.Create).ToList());

        public static CommonResult<T> CreateError(HttpStatusCode httpStatusCode, /*!< HttpStatusCode */
                                                    params ErrorMessage[] errorMessages) =>
            new(null, (int)httpStatusCode, errorMessages.Select(p => new ErrorMessage
            {
                Message = p.Message,
                Code = p.Code,
                Type = p.Type
            }).ToList());

        public static CommonResult<T> CreateResult(T data) =>
            new(data, 200);

        public static CommonResult<T> CreateEmpty() =>
            new(null, 200);

    }
}
