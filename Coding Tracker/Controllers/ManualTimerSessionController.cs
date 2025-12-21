using Coding_Tracker.Repositories;
using Spectre.Console;

namespace Coding_Tracker.Controllers
{
    internal class ManualTimerSessionController : ISessionController
    {
        private readonly CodingSessionRepository _repo;
        private readonly ConsoleUI _ui;
        public ManualTimerSessionController(CodingSessionRepository repo, ConsoleUI ui)
        {
            _repo = repo;
            _ui = ui;
        }

        public void StartSession()
        {
            var start = _ui.PromptForDateTime("Enter the [green]start[/] date and time");
            var end = _ui.PromptForDateTime("Enter the [red]end[/] date and time");

            _repo.AddSession(CodingSession.Create(start, end));
        }


    }
}
