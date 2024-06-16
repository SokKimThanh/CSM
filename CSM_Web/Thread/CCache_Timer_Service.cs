
namespace CSM_Web.Thread
{
    public class CCache_Timer_Service : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //Tại đây gọi các hàm load cache



                // Chờ 1 giây trước khi thực hiện lại
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
