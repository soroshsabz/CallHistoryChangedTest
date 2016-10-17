using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.Calls.Background;
using Windows.ApplicationModel.Calls.Provider;

namespace CallHangUpTask
{
    public sealed class CallHangupTask : IBackgroundTask
    {
        //
        // The Run method is the entry point of a background task.
        //
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            // Associate a cancellation handler with the background task.
            // Even though this task isn't performing much work, it can still be cancelled.
            taskInstance.Canceled += new BackgroundTaskCanceledEventHandler(OnCanceled);

            // Do the background task activity.
            BackgroundTaskDeferral _deferral = taskInstance.GetDeferral();

            Toast.Send("CallHistoryChanged");

            _deferral.Complete();
        }

        //
        // Handles background task cancellation.
        //
        private void OnCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            // Normally, we would signal the task to exit.
            // However, our sample task has no async operations and is so short that
            // we'll just let it run to completion instead of trying to cancel it.
        }
    }
}
