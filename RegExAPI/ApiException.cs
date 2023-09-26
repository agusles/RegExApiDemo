using System.Net;

namespace RegExAPI;

[Serializable]
public class ApiException : Exception
{
    public ApiException()
        : this(HttpStatusCode.InternalServerError, string.Empty) { }

    public ApiException(HttpStatusCode statusCode)
        : this(statusCode, string.Empty) { }

    public ApiException(HttpStatusCode statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }

    public HttpStatusCode StatusCode { get; set; }
}

[Serializable]
public class ApiException<TDataType> : ApiException
{
    public ApiException(HttpStatusCode statusCode, string message, TDataType data)
        : base(statusCode, message)
    {
        Data = data;
    }

    public new TDataType Data { get; }
}