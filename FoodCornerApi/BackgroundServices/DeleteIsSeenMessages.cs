using FoodCornerApi.Database;

namespace FoodCornerApi.BackgroundServices
{
    public class DeleteIsSeenMessages : IHostedService, IDisposable
    {

        private ILogger<DeleteIsSeenMessages> _logger;

        public IServiceProvider Services { get; }

        private Timer _timer;

        public DeleteIsSeenMessages(IServiceProvider service, ILogger<DeleteIsSeenMessages> logger)
        {
            Services = service;
            _logger = logger;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {

            Console.WriteLine($"{nameof(DeleteIsSeenMessages)} is startet");

            _timer = new Timer(DeleteMessages, null, TimeSpan.Zero, TimeSpan.FromMinutes(20));

            return Task.CompletedTask;
        }

        private void DeleteMessages(object state)

        {

            using IServiceScope scope = Services.CreateScope();

            var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();

            try
            {

                var messages = dataContext.Messages.Where(m => m.IsSeen == true).ToList();

                foreach (var deleteMessages in messages)
                {
                    if (deleteMessages.IsSeen == true)
                    {
                        dataContext.Messages.Remove(deleteMessages);

                    }
                    else
                    {
                        continue;
                    }
                }

                dataContext.SaveChanges();

            }
            catch (Exception e)
            {
                _logger.LogWarning($"Something went wrong while {nameof(DeleteIsSeenMessages)} working");
                throw e;
            }

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"{nameof(DeleteIsSeenMessages)} is stoped");

            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _timer.Dispose();
        }
    }
}
