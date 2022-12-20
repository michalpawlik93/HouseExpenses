using HousExpenses.Domain.Enums;
using HousExpenses.Domain.Exceptions;
using System.Net;

namespace HouseExpenses.Api.Models;

/// <summary>
/// Service response data
/// </summary>
public class ServiceResponse
{
    public ServiceResponse() { }
    public ServiceResponse(Exception exception)
    {
        Messages = GetServiceResponseMessages(exception);
    }
    /// <summary>
    /// The service response DTO
    /// </summary>
    public string Data { get; set; }

    /// <summary>
    /// List of <see cref="ServiceResponseMessage"/> associated to the service response
    /// </summary>
    public List<ServiceResponseMessage> Messages { get; set; }

    private static List<ServiceResponseMessage> GetServiceResponseMessages(Exception exception)
    {
        return exception switch
        {
            ResourceNotFoundException resourceNotFoundException => GetExceptionMessages(resourceNotFoundException),
            _ => GetInternalServerErrorMessages()
        };
    }

    private static List<ServiceResponseMessage> GetInternalServerErrorMessages()
    {
        return new()
            {
                new()
                {
                    Message = "Internal server exception. Please, contact your provider.",
                    Type = MessageType.Error
                }
            };
    }

    private static List<ServiceResponseMessage> GetExceptionMessages(Exception platformException) =>
        new()
        {
            new()
                {
                    Message = platformException.Message,
                    Type = MessageType.Error
                }
            };
}

/// <summary>
/// Service response data
/// </summary>
public class ServiceResponse<T>
{
    /// <summary>
    /// The service response DTO
    /// </summary>
    public T Data { get; set; }

    /// <summary>
    /// List of <see cref="ServiceResponseMessage"/> associated to the service response
    /// </summary>
    public List<ServiceResponseMessage> Messages { get; set; }
}
