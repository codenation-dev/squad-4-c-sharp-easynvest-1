using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace LogCenter.Domain.Results
{
    public class Result
    {
        public Result()
        {

        }

        public Result(Result result)
        {
            if (result == null)
                return;

            Message = result.Message;
            StatusCode = result.StatusCode;

            foreach (var error in result.Errors)
            {
                Errors.Add(error);
            }
        }

        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public List<ErrorDescription> Errors { get; set; } = new List<ErrorDescription>();
        public bool IsSuccess { get { return !IsFailure; } }
        public bool IsFailure { get { return Errors.Any(); } }

        public void AddError(string message, string reason, string domain)
        {
            Errors.Add(new ErrorDescription { Message = message, Domain = domain, Reason = reason });
        }

        public void Merge(Result result)
        {
            if (result == null)
                return;

            foreach (var error in result.Errors)
            {
                Errors.Add(error);
            }
        }

        public void Merge(IEnumerable<Result> results)
        {
            if (results == null)
                return;

            foreach (var result in results)
            {
                Merge(result);
            }
        }

        public Result<T> ToResult<T>()
        {
            return new Result<T>(default, this);
        }

        public Result<T> ToResult<T>(T value)
        {
            return new Result<T>(value, this);
        }

        public override string ToString()
        {
            if (!Errors.Any() && string.IsNullOrWhiteSpace(Message))
                return "No Errors";

            var sb = new StringBuilder();

            sb.AppendLine(Message);

            foreach (var error in Errors)
            {
                sb.AppendLine(error.ToString());
            }

            return sb.ToString();
        }
    }

    public class Result<T> : Result
    {
        public Result()
        {
        }

        public Result(T value)
        {
            Value = value;
        }

        public Result(T value, Result result)
            : base(result)
        {
            Value = value;
        }
        public T Value { get; set; }
    }


    public class PaginatedResult<T> : Result
    {
        public PaginatedResult()
        {
        }

        public PaginatedResult(IEnumerable<T> value, int pageIndex, int pageSize, long totalItems)
        {
            Value = value;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalItems = totalItems;
        }

        public PaginatedResult(IEnumerable<T> value, int pageIndex, int pageSize, long totalItems, Result result)
            : base(result)
        {
            Value = value;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalItems = totalItems;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public long TotalItems { get; set; }
        public IEnumerable<T> Value { get; set; }
    }
}
