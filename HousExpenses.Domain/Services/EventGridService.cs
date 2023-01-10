using Azure;
using Azure.Messaging.EventGrid;
using HousExpenses.Domain.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HousExpenses.Domain.Services;

public class EventGridService : IEventGridService
{
    private readonly Lazy<EventGridPublisherClient> _client;
    private readonly ILogger _logger;
    public EventGridService(IOptions<EventGridConfig> options, ILogger<EventGridService> logger)
    {
        _ = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _client = new Lazy<EventGridPublisherClient>(() => new EventGridPublisherClient(new Uri(options.Value.Endpoint)
            , new AzureKeyCredential("<access-key>")));
        _logger = logger;
    }

    public async Task Publish()
    {
        try
        {
           // await _client.Value.SendEventAsync()
        }
        catch (Exception ex)
        {
            _logger.LogError(default, ex, $"Error publishing event.");
            throw;
        }
    }
}
