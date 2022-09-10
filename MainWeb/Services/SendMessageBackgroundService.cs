using MainWeb.Data;
using System.Diagnostics;

namespace MainWeb.Services
{
    public class SendMessageBackgroundService : BackgroundService
    {

        private readonly IFcmNotificationService fcmService;
        private readonly IServiceScopeFactory _scopeFactory;

        public SendMessageBackgroundService(IFcmNotificationService fcmService, IServiceScopeFactory scopeFactory)
        {
            this.fcmService = fcmService;
            _scopeFactory = scopeFactory;
        }

        private readonly PeriodicTimer _timer = new PeriodicTimer(TimeSpan.FromMinutes(30));

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (await _timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
            {
                 await DoWorkAsync();
            }
        }

        private async Task DoWorkAsync()
        {

            try
            {
                using var scope = _scopeFactory.CreateScope();

                // Get a Dbcontext from the scope
                var dbContext = scope.ServiceProvider
                    .GetRequiredService<ApplicationDbContext>();

                var messagesQuery = from a in dbContext.Messages.Where(x => !x.Sended)
                                    join b in dbContext.Peserta on a.PesertaId equals b.Id
                                    select new { message = a, peserta = b };

                if (messagesQuery.Any())
                {
                    foreach (var item in messagesQuery.Where(x=>x.message.Tanggal <= DateTime.Now))
                    {
                        var result = await fcmService.SendNotification(new Models.NotificationModel { Body = item.message.Message, DeviceId = item.peserta.DeviceToken });
                        if (result.IsSuccess)
                        {
                            item.message.Sended = true;
                        }
                    }

                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
