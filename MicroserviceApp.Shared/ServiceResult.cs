using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Refit;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

namespace MicroserviceApp.Shared;

public class ServiceResult
{
    [JsonIgnore] public HttpStatusCode Status { get; set; }
    public ProblemDetails? Fail { get; set; }
    [JsonIgnore] public bool IsSuccess => Fail == null;
    [JsonIgnore] public bool IsFailure => !IsSuccess;
    
    //Static factory methods
    public static ServiceResult SuccessAsNoContent()
    {
        return new ServiceResult { Status = HttpStatusCode.NoContent };
    }
    
    public static ServiceResult ErrorAsNotFound()
    {
        return new ServiceResult
        {
            Status = HttpStatusCode.NotFound,
            Fail = new ProblemDetails
            {
                Title = "Not Found",
                Detail = "Resource not found"
            }
        };
    }
    
    public static ServiceResult ErrorFromProblemDetails(ApiException apiException)
    {
        if (string.IsNullOrEmpty(apiException.Content))
        {
            return new ServiceResult()
            {
                Status = apiException.StatusCode,
                Fail = new ProblemDetails
                {
                    Title = apiException.Message
                }
            };
        }

        var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(apiException.Content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return new ServiceResult()
        {
            Status = apiException.StatusCode,
            Fail = problemDetails
        };
        
    }
    
    public static ServiceResult Error(ProblemDetails problemDetails, HttpStatusCode statusCode)
    {
        return new ServiceResult()
        {
            Status = statusCode,
            Fail = problemDetails
        };
    }
    
    public static ServiceResult Error(string title, string description, HttpStatusCode statusCode)
    {
        return new ServiceResult()
        {
            Status = statusCode,
            Fail = new ProblemDetails
            {
                Title = title,
                Detail = description,
                Status = statusCode.GetHashCode()
            }
        };
    }
    
    public static ServiceResult Error(string title, HttpStatusCode statusCode)
    {
        return new ServiceResult()
        {
            Status = statusCode,
            Fail = new ProblemDetails
            {
                Title = title,
                Status = statusCode.GetHashCode()
            }
        };
    }
    
    public static ServiceResult ErrorFromValidation(IDictionary<string, object?> errors)
    {
        return new ServiceResult()
        {
            Status = HttpStatusCode.BadRequest,
            Fail = new ProblemDetails()
            {
                Title = "Validation Error",
                Detail = "One or more validation errors occurred.",
                Status = HttpStatusCode.BadRequest.GetHashCode(),
                Extensions =
                {
                    { "errors", errors }
                }
            }
        };
    }
}

public class ServiceResult<T> : ServiceResult
{
    public T? Data { get; set; }
    public string? UrlAsCreated { get; set; }
    
    //Static factory methods
    //200
    public static ServiceResult<T> SuccessAsOk(T data)
    {
        return new ServiceResult<T> { Status = HttpStatusCode.OK, Data = data };
    }
    //201
    public static ServiceResult<T> SuccessAsCreated(T data, string urlAsCreated)
    {
        return new ServiceResult<T>
        {
            Status = HttpStatusCode.Created,
            Data = data,
            UrlAsCreated = urlAsCreated
        };
    }

    public new static ServiceResult<T> ErrorFromProblemDetails(ApiException apiException)
    {
        if (string.IsNullOrEmpty(apiException.Content))
        {
            return new ServiceResult<T>()
            {
                Status = apiException.StatusCode,
                Fail = new ProblemDetails
                {
                    Title = apiException.Message
                }
            };
        }

        var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(apiException.Content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return new ServiceResult<T>()
        {
            Status = apiException.StatusCode,
            Fail = problemDetails
        };
        
    }
    
    public new static ServiceResult<T> Error(ProblemDetails problemDetails, HttpStatusCode statusCode)
    {
        return new ServiceResult<T>()
        {
            Status = statusCode,
            Fail = problemDetails
        };
    }
    
    public new static ServiceResult<T> Error(string title, string description, HttpStatusCode statusCode)
    {
        return new ServiceResult<T>()
        {
            Status = statusCode,
            Fail = new ProblemDetails
            {
                Title = title,
                Detail = description,
                Status = statusCode.GetHashCode()
            }
        };
    }
    
    public new static ServiceResult<T> Error(string title, HttpStatusCode statusCode)
    {
        return new ServiceResult<T>()
        {
            Status = statusCode,
            Fail = new ProblemDetails
            {
                Title = title,
                Status = statusCode.GetHashCode()
            }
        };
    }
    
    public new static ServiceResult<T> ErrorFromValidation(IDictionary<string, object?> errors)
    {
        return new ServiceResult<T>()
        {
            Status = HttpStatusCode.BadRequest,
            Fail = new ProblemDetails()
            {
                Title = "Validation Error",
                Detail = "One or more validation errors occurred.",
                Status = HttpStatusCode.BadRequest.GetHashCode(),
                Extensions =
                {
                    { "errors", errors }
                }
            }
        };
    }
    
}