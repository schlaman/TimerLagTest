using System.Timers;

namespace TimerLagTest
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        private readonly DateTime _codeStartDateTime = DateTime.Now;

        private bool _isTextUpdatesEnabled = false;

        private long _counter = 0;

        private readonly System.Timers.Timer _masterTimer = new()
        {
            AutoReset = true,
            Enabled = false,
            Interval = 100,
        };

        public MainPage()
        {
            InitializeComponent();

            _masterTimer.Elapsed += MasterTimer_Elapsed;
            _isTextUpdatesEnabled = true;
        }

        private void MasterTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                if (Application.Current != null)
                {
                    Application.Current.Dispatcher.Dispatch(HandleTimerTickEvent);
                }
            }
            else
            {
                MainThread.BeginInvokeOnMainThread(HandleTimerTickEvent);
            }
        }

        private void HandleTimerTickEvent()
        {
            var now = DateTime.Now;

            try
            {
                HandleCodeTimer(now);
            }
            catch (System.Runtime.InteropServices.COMException e)
            {
                // TODO: Evaluate whether this is an issue or not
                Console.WriteLine(e);
            }
        }

        private void HandleCodeTimer(DateTime now)
        {
            var codeElapsed = now - _codeStartDateTime;
            var time = $"{codeElapsed:h':'mm':'ss}";

            if (_isTextUpdatesEnabled)
            {
                TimerLabel.Text = time;
                TimerLabel2.Text = now.ToString("h':'mm':'ss");
            }

            _counter++;
            if (_isTextUpdatesEnabled)
            {
                CounterLabel.Text = _counter.ToString();
            }
        }

        private void StartButton_OnClicked(object? sender, EventArgs e)
        {
            _masterTimer.Start();
            TimerStatus.Text="Timer  on";
        }

        private void StopButton_OnClicked(object? sender, EventArgs e)
        {
            _masterTimer.Stop();
            TimerStatus.Text="Timer off";
        }

        private void EnableTextUpdatesButton_OnClicked(object? sender, EventArgs e)
        {
            _isTextUpdatesEnabled = true;
            TextUpdatesStatus.Text="Updates  on";
        }

        private void DisableTextUpdatesButton_OnClicked(object? sender, EventArgs e)
        {
            _isTextUpdatesEnabled = false;
            TextUpdatesStatus.Text="Updates off";
        }
    }

}
