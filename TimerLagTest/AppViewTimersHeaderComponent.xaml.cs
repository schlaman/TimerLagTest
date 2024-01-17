namespace TimerLagTest;

public partial class AppViewTimersHeaderComponent : ContentView
{
    private static string _lastTimerText = string.Empty;
    public AppViewTimersHeaderComponent()
    {
        InitializeComponent();
        Instance = this;
    }

    public static AppViewTimersHeaderComponent? Instance { get; private set; }

    public static void SetTimerText(string timerText)
    {
        if (timerText == _lastTimerText)
            return;

        _lastTimerText = timerText;

        if (Instance != null)
        {
            Instance.TimerLabel.Text = timerText;
            return;
        }
    }
}