using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using Application.Api.Controllers;
using Business_Logic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Application.Api.Filters;

/// <summary>
///     The api result logger.
/// </summary>
public class ApiResultLoggerFilter : IAsyncActionFilter, IAsyncResultFilter
{
    /// <summary>
    ///     Gets the logger.
    /// </summary>
    private readonly ITodoLogger _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ApiResultLoggerFilter" /> class.
    /// </summary>
    /// <param name="logger">
    ///     The logger.
    /// </param>
    public ApiResultLoggerFilter(ITodoLogger logger)
    {
        _logger = logger;
    }

    /// <summary>
    ///     Gets or sets the action parameters json.
    /// </summary>
    private IDictionary<string, object> ActionParameters { get; set; }

    private long StartTicks { get; set; }

    /// <inheritdoc />
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        this.StartTicks = Environment.TickCount64;
        this.ActionParameters = context.ActionArguments;
        await next();
    }

    /// <inheritdoc />
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (context.Result is ObjectResult objectResult)
        {
            try
            {
                string method = context.HttpContext.Request.Method;
                string[] paramsFromServices = context.ActionDescriptor.Parameters
                    .Where(p => p.BindingInfo.BindingSource.DisplayName == "Services" || p.ParameterType == typeof(CancellationToken)).Select(p => p.Name).ToArray();
                string parameters = string.Join(
                    ",",
                    this.ActionParameters
                        .Where(p => !paramsFromServices.Contains(p.Key))
                        .Select(p => $"{p.Key}:{Serialize(p.Value)}"));
                string action = context.ActionDescriptor.DisplayName;
                string jsonResult = Serialize(objectResult.Value);
                TimeSpan actionDuration = TimeSpan.FromMilliseconds(Environment.TickCount64 - this.StartTicks);
                string message =
                    $"в ответ на [{method}] {action} с параметрами {parameters} получил ответ {jsonResult} за {actionDuration}";
                switch (objectResult.StatusCode)
                {
                    case (int)HttpStatusCode.InternalServerError:
                        await _logger.Log(message);
                        break;
                    case (int)HttpStatusCode.Created:
                    case (int)HttpStatusCode.NoContent:
                    case (int)HttpStatusCode.OK:
                        await _logger.Log(message);
                        break;
                    default:
                        await _logger.Log(message);
                        break;
                }
            }
            catch (Exception e)
            {
                await _logger.Log(e.ToString());
            }
        }

        await next();
    }

    /// <summary>
    ///     The serialize.
    /// </summary>
    /// <param name="parameter">
    ///     The parameter.
    /// </param>
    /// <returns>
    ///     The <see cref="string" />.
    /// </returns>
    private static string Serialize(object parameter)
    {
        return JsonSerializer.Serialize(
            parameter,
            new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            });
    }
}