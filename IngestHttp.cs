using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

public class IngestHttp
{
    private readonly ILogger _logger;
    public IngestHttp(ILoggerFactory loggerFactory) => _logger = loggerFactory.CreateLogger<IngestHttp>();

    [Function("IngestHttp")]
    [ServiceBusOutput("dataverse-events", Connection = "ServiceBusConnection")]
    public async Task<string> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
    {
        var body = await new StreamReader(req.Body).ReadToEndAsync();
        _logger.LogInformation("HTTP received {len} bytes", body?.Length ?? 0);
        return string.IsNullOrWhiteSpace(body) ? "{\"msg\":\"empty\"}" : body;
    }
}
