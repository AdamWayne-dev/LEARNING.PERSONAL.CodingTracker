namespace Coding_Tracker.Models
{
    internal class Enums
    {

        internal enum MenuAction 
        {             
            StartCodingSession = 1,
            ViewSessions,
            ViewStatistics,
            Settings,
            Exit
        }

        internal enum SessionMenuAction 
        {
            StartTimer = 1,
            EnterTimeManually,
            BackToMainMenu
        }

        internal enum TimeFrame 
        {
            Daily = 1,
            Weekly,
            Monthly,
            Yearly,
            AllTime
        }

        internal enum SettingOption 
        {
            ChangeDailyGoal = 1,
            ChangeTheme,
            BackToMainMenu
        }
    }
}
