using Spectre.Console;
using Coding_Tracker.Models;
using Coding_Tracker.Repositories;

namespace Coding_Tracker.Controllers
{
    internal class AutoTimerSessionController : ISessionController
    {
        private readonly ConsoleUI _ui;
        private readonly CodingSessionRepository _repo;
        public AutoTimerSessionController(CodingSessionRepository repo, ConsoleUI ui)
        {
            _repo = repo;
            _ui = ui;
        }

        public void StartSession()
        {
            var start = DateTime.Now;
            DisplayStopWatch();
            var end = DateTime.Now;

            _repo.AddSession(CodingSession.Create(start, end));
        }

        public void DisplayStopWatch()
        {
            float timer = 0f;
            var stopWatchPanel = new Panel("");
            stopWatchPanel.Border = BoxBorder.Rounded;
            stopWatchPanel.Header = new PanelHeader("[green]Coding Session In Progress[/]");
            while (true)
            {
                stopWatchPanel = new Panel(
                    new Markup($"[yellow]{TimeSpan.FromSeconds(timer):hh\\:mm\\:ss}[/]")
                );
                AnsiConsole.Write(stopWatchPanel);
                AnsiConsole.WriteLine();
                AnsiConsole.MarkupLine("[grey]Press [blue]S[/] to stop the session...[/]");
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.S)
                    {
                        break;
                    }
                }
                timer += 1f;
                System.Threading.Thread.Sleep(1000);
                AnsiConsole.Clear();
            }

        }
    }
}
