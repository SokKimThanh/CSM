namespace CSM_Web.Thread
{
    public class CBackground_Service_1 : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //Tại đây gọi các hàm như auto send mail, api



                // Chờ 1 giây trước khi thực hiện lại
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}