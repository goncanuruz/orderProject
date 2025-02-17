using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProject.Application.DTOs
{
    public class BaseResponse : BaseResponse<object, object>
    {
        public static BaseResponse Create()
        {
            return new BaseResponse();
        }
    }
    public class BaseResponse<ResponseType> : BaseResponse<ResponseType, object>
    {
        public static BaseResponse Create()
        {
            return new BaseResponse();
        }
    }
    public class BaseResponse<ResponseType, ResponseDetail>
        where ResponseDetail : class

    {
        protected DateTime _startDate;
        public BaseResponse()
        {
            _startDate = DateTime.Now;
        }
        public ResponseStatus Status { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public ResponseType Result { get; set; }
        public ResponseDetail ResultDetail { get; set; }
        public object ErrorDetails { get; set; }
        public TimeSpan ProgressTime { get; set; }

        public BaseResponse<ResponseType, ResponseDetail> SetSuccess(string message, ResponseType? result = default, ResponseDetail? resultDetail = null)
        {
            Status = ResponseStatus.Success;
            Message = message;
            Result = result;
            ResultDetail = resultDetail;
            ProgressTime = DateTime.Now - _startDate;
            return this;
        }
        public BaseResponse<ResponseType, ResponseDetail> SetError(string errorMessage, object? errorDetails = null, string? errorCode = null)
        {
            Status = ResponseStatus.Error;
            Message = errorMessage;
            ErrorDetails = errorDetails;
            Code = errorCode;
            ProgressTime = DateTime.Now - _startDate;
            return this;
        }
        public enum ResponseStatus
        {
            [Description("Success")]
            Success = 1,

            [Description("Error")]
            Error = 2,
        }
    }
}
