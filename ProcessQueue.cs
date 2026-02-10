using System.Text;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Hub.Functions;

public class ProcessQueue
{
    private readonly ILogger _logger;
    public ProcessQueue(ILoggerFactory loggerFactory) => _logger = loggerFactory.CreateLogger<ProcessQueue>();

    [Function("ProcessQueue")]
    [BlobOutput("messages/{rand-guid}.json", Connection = "AzureWebJobsStorage")]
    public byte[] Run(
        [ServiceBusTrigger("dataverse-events", Connection = "ServiceBusConnection")] string message)
    {
        _logger.LogInformation("ServiceBus message received, length={len}", message?.Length ?? 0);
        return Encoding.UTF8.GetBytes(message ?? "");
    }
}
