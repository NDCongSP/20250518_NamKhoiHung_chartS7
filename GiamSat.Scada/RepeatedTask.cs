using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GiamSat.Scada
{
    public class RepeatedTask
    {
        private CancellationTokenSource _cts;

        public void Start()
        {
            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        // 👉 Thực hiện công việc ở đây
                        Console.WriteLine($"Thời gian: {DateTime.Now:HH:mm:ss}");

                        await Task.Delay(1000, token); // Chờ 1 giây
                    }
                    catch (TaskCanceledException)
                    {
                        break;
                    }
                }
            }, token);
        }

        public void Stop()
        {
            _cts?.Cancel();
        }
    }
}