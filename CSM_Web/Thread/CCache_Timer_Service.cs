using CSM_Common.Common;

namespace CSM_Web.Thread
{
    public class CCache_Timer_Service : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (CCache_Common.Is_Load_Cache_Completed == false)
            {
                //Tại đây gọi các hàm load cache của danh mục
                try
                {




                    CCache_Common.Is_Load_Cache_Completed = true; // Load tắt load cache nếu đã load xong
                }
                catch (Exception)
                {
                    // Chờ 1 giây trước khi thực hiện lại
                    await Task.Delay(1000, stoppingToken);
                }
            }
        }
    }
}
