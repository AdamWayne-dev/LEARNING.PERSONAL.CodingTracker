using Coding_Tracker.Controllers;
using Spectre.Console;
using static Coding_Tracker.Models.Enums;
namespace Coding_Tracker
{
    internal class UserInterface
    {

        private readonly AutoTimerSessionController _autoTimerSessionController;
        private readonly ManualTimerSessionController _manualTimerSessionController;
        private readonly CodingSessionRepository _repo;

        public UserInterface(CodingSessionRepository repo)
        {
            _repo = repo;
            _autoTimerSessionController = new AutoTimerSessionController(_repo);
            _manualTimerSessionController = new ManualTimerSessionController(_repo);
        }
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
                        bool flowControl = StartSession();
                        if (!flowControl)
                        {
                            continue;
                        }
                        break;

                    case MenuAction.ViewSessions:
                        ShowSessions();

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

        private bool StartSession()
        {
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
                    return false;
            }

            return true;
        }

        private void ShowSessions()
        {
            var sessions = _repo.GetAll();

            AnsiConsole.Clear();
            if (sessions.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No coding sessions found.[/]");
            }
            else
            {
                foreach (var s in sessions)
                {
                    AnsiConsole.MarkupLine($"[green]Session ID:[/] {s.Id}, [green]Start Time:[/] {s.StartTime}, [green]End Time:[/] {s.EndTime}, [green]Duration:[/] {s.Duration}");
                }
            }
            AnsiConsole.MarkupLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
        }
    }
}
