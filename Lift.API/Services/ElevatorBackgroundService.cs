namespace Lift.API.Services
{
    public class ElevatorBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ElevatorBackgroundService> _logger;

        public ElevatorBackgroundService(IServiceProvider serviceProvider, ILogger<ElevatorBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Lift fon servisi ishga tushdi.");

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var elevatorService = scope.ServiceProvider.GetRequiredService<ElevatorService>();

                    try
                    {
                        await elevatorService.ProcessNextRequestAsync();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Lift ishida xatolik yuz berdi.");
                    }
                }

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
