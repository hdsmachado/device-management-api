using Device.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Device.Api.Handlers;

public class ApiExceptionHandler : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        ProblemDetails problem;

        switch (context.Exception)
        {
            case DeviceNotFoundException ex:
                problem = CreateProblemDetails(
                    StatusCodes.Status404NotFound,
                    "Resource not found",
                    "Device with the given id was not found.");
                break;

            case DeviceInUseException ex when
                ex.Message.Contains("deleted", StringComparison.OrdinalIgnoreCase):
                problem = CreateProblemDetails(
                    StatusCodes.Status409Conflict,
                    "Conflict",
                    "Devices in use cannot be deleted.");
                break;

            case DeviceInUseException:
            case InvalidDeviceOperationException:
                problem = CreateProblemDetails(
                    StatusCodes.Status400BadRequest,
                    "Invalid operation",
                    "Name and brand cannot be updated when the device is in use.");
                break;

            default:
                problem = CreateProblemDetails(
                    StatusCodes.Status500InternalServerError,
                    "Internal server error",
                    "An unexpected error occurred.");
                break;
        }

        context.Result = new ObjectResult(problem)
        {
            StatusCode = problem.Status
        };

        context.ExceptionHandled = true;
    }

    private static ProblemDetails CreateProblemDetails(
        int status,
        string title,
        string detail)
    {
        return new ProblemDetails
        {
            Status = status,
            Title = title,
            Detail = detail,
            Type = $"{status}"
        };
    }
}
