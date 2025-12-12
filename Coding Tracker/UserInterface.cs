using Coding_Tracker.Controllers;
using Spectre.Console;
using static Coding_Tracker.Models.Enums;
namespace Coding_Tracker
{
    internal class UserInterface
    {

        private readonly AutoTimerSessionController _autoTimerSessionController = new();
        private readonly ManualTimerSessionController _manualTimerSessionController = new();
        internal void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<MenuAction>()
                        .Title("Select an option:")
                        .AddChoices(Enum.GetValues<MenuAction>()));

                

                switch (choice) { 

                    case MenuAction.StartCodingSession:
                        var sessionType = AnsiConsole.Prompt(
                        new SelectionPrompt<SessionMenuAction>()
                        .Title("Select session type:")
                        .AddChoices(Enum.GetValues<SessionMenuAction>()));
                        switch (sessionType)
                        {
                            case SessionMenuAction.StartTimer:
                                _autoTimerSessionController.StartSession();
                                break;
                            case SessionMenuAction.EnterTimeManually:
                                _manualTimerSessionController.StartSession();
                                break;
                            case SessionMenuAction.BackToMainMenu:
                                continue;
                        }
                        break;
                    case MenuAction.ViewStatistics:
                        //var statsViewer = new StatisticsViewer();
                        //statsViewer.DisplayStatistics();
                        break;
                    case MenuAction.Settings:
                        //var settingsManager = new SettingsManager();
                        //settingsManager.ConfigureSettings();
                        break;
                    case MenuAction.Exit:
                        Environment.Exit(0);
                        return;
                }

            }
        }
    }
}
