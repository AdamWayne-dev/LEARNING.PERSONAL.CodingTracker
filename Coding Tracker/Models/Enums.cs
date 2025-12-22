namespace Coding_Tracker.Models
{
    internal class Enums
    {

        internal enum MenuAction 
        {             
            Start_Coding_Session = 1,
            View_Sessions,
            View_Previous_30_Day_Activity_Heatmap,
            Exit
        }

        internal enum SessionMenuAction 
        {
            Start_Timer = 1,
            Enter_Time_Manually,
            Back_To_Main_Menu
        }

        internal enum ViewSessionsAction
        {
            View_All_Sessions = 1,
            View_Sessions_By_Date,
            Back_To_Main_Menu
        }
    }
}
