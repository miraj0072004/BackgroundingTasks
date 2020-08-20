using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BackgroundingTasks.Messages;
using Xamarin.Forms;

namespace BackgroundingTasks
{
    public class TaskCounter
    {
        public async Task RunCounter(CancellationToken token)
        {
            await Task.Run(async () => {

                for (long i = 0; i < long.MaxValue; i++)
                {
                    token.ThrowIfCancellationRequested();

                    await Task.Delay(250);
                    var message = new TickedMessage
                    {
                        Message = i.ToString()
                    };

                    Device.BeginInvokeOnMainThread(() => {
                        MessagingCenter.Send<TickedMessage>(message, "TickedMessage");
                    });
                }
            }, token);
        }
	}
}
