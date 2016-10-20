using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.Calls.Background;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using CallHangUpTask;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CallHistoryChangedTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            if (!(BackgroundTaskRegistration.AllTasks.Count > 0))
            {
                RegisterBackgroundTask(typeof(CallHangupTask),
                    new PhoneTrigger(PhoneTriggerType.CallHistoryChanged, false));
            }
            else
                Toast.Send("Registered background tasks");
        }

        private async void RegisterBackgroundTask(Type type, IBackgroundTrigger trigger)
        {
            BackgroundAccessStatus access = await BackgroundExecutionManager.RequestAccessAsync();
            if (access == BackgroundAccessStatus.AlwaysAllowed ||
                access == BackgroundAccessStatus.AllowedSubjectToSystemPolicy)
            {
                // If the task is already registered. then don't register it again.
                foreach (IBackgroundTaskRegistration task in BackgroundTaskRegistration.AllTasks.Values)
                {
                    if (task.Name == type.Name)
                    {
                        return;
                    }
                }

                var builder = new BackgroundTaskBuilder();

                // Name the background task.
                builder.Name = type.Name;

                // Specify the background task to run when the trigger fires.
                builder.TaskEntryPoint = type.FullName;

                builder.SetTrigger(trigger);
                BackgroundTaskRegistration taskRegistration = builder.Register();
                Toast.Send("Background tasks is now registered");
            }
            else
                Toast.Send("Background agent access denied");
        }
    }
}
