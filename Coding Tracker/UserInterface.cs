using Coding_Tracker.Controllers;
using Coding_Tracker.Repositories;
using Spectre.Console;
using static Coding_Tracker.Models.Enums;
namespace Coding_Tracker
{
    internal class UserInterface
    {
        private readonly ConsoleUI _ui = new ConsoleUI();
        private readonly AutoTimerSessionController _autoTimerSessionController;
        private readonly ManualTimerSessionController _manualTimerSessionController;
        private readonly CodingSessionRepository _repo;

        public UserInterface(CodingSessionRepository repo)
        {
            _repo = repo;
            _autoTimerSessionController = new AutoTimerSessionController(_repo, _ui);
            _manualTimerSessionController = new ManualTimerSessionController(_repo, _ui);
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



                switch (choice)
                {

                    case MenuAction.Start_Coding_Session:
                        bool flowControl = StartSession();
                        if (!flowControl)
                        {
                            continue;
                        }
                        break;

                    case MenuAction.View_Sessions:
                        ShowSessions();

                        break;

                    case MenuAction.View_Previous_30_Day_Activity_Heatmap:
                        _ui.DisplayActivityHeatMap(_repo.GetAllSessions());
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
                case SessionMenuAction.Start_Timer:
                    _autoTimerSessionController.StartSession();
                    break;
                case SessionMenuAction.Enter_Time_Manually:
                    _manualTimerSessionController.StartSession();
                    break;
                case SessionMenuAction.Back_To_Main_Menu:
                    return false;
            }

            return true;
        }

        private void ShowSessions()
        {
            switch (AnsiConsole.Prompt(
                        new SelectionPrompt<ViewSessionsAction>()
                            .Title("View sessions by:")
                            .AddChoices(Enum.GetValues<ViewSessionsAction>()
                            )))
            {
                case ViewSessionsAction.View_All_Sessions:
                    var allSessions = _repo.GetAllSessions();
                    DisplayFilteredSessions(allSessions);
                    break;

                case ViewSessionsAction.View_Sessions_From_Set_Date_Selection:
                    FilteredSessions();
                    break;

                case ViewSessionsAction.Back_To_Main_Menu:
                    Environment.Exit(0);
                    break;
            }
        }

        private void FilteredSessions()
        {
            switch (AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("Please select an option from the following:")
                .AddChoices(new[] {
                    "Yesterday",
                    "Last 7 Days",
                    "Last 30 Days",
                    "Custom Date Range",
                    "Back to Main Menu"
                }))){
                case "Yesterday":
                    var yesterday = DateTime.Today.AddDays(-1);
                    var sessionsYesterday = _repo.GetFilteredSession(yesterday, null);
                    DisplayFilteredSessions(sessionsYesterday);
                    break;

                case "Last 7 Days":
                    var last7Days = DateTime.Today.AddDays(-7);
                    var sessions7Days = _repo.GetFilteredSession(last7Days, null);
                    DisplayFilteredSessions(sessions7Days);
                    break;

                case "Last 30 Days":
                    var last30Days = DateTime.Today.AddDays(-30);
                    var sessions30Days = _repo.GetFilteredSession(last30Days, null);
                    DisplayFilteredSessions(sessions30Days);
                    break;

                case "Custom Date Range":
                    var fromDate = _ui.PromptForDateTime("Enter the [green]start[/] date");
                    var toDate = _ui.PromptForDateTime("Enter the [red]end[/] date");
                    var customSessions = _repo.GetFilteredSession(fromDate, toDate);
                    DisplayFilteredSessions(customSessions);
                    break;
            }
        }

        private static void DisplayFilteredSessions(List<CodingSession> sessions)
        {
            if (sessions.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No coding sessions found in the specified date range.[/]");
            }
            else
            {
                foreach (var s in sessions)
                {
                    AnsiConsole.MarkupLine($"[green]Session ID:[/] {s.Id}, [green]Start Time:[/] {s.StartTime}, [green]End Time:[/] {s.EndTime}, [green]Duration:[/] {s.Duration:hh\\:mm\\:ss}");
                }
            }
            AnsiConsole.MarkupLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
        }
    }
}
