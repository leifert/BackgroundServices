namespace WebsiteStatus
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private HttpClient _httpClient;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _httpClient = new HttpClient();
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _httpClient.Dispose();
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = await _httpClient.GetAsync("https://google.com");
                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"The website is up. Status code {result.StatusCode}");
                }
                else
                {
                    _logger.LogError($"The website is down. Status code {result.StatusCode}");
                }
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
