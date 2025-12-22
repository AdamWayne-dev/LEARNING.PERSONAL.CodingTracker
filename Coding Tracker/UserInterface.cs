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

                

                switch (choice) {

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
            var sessions = _repo.GetAllSessions();

            AnsiConsole.Clear();
            if (sessions.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No coding sessions found.[/]");
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
