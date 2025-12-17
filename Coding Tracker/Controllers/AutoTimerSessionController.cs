using Spectre.Console;
using Coding_Tracker.Models;
using Coding_Tracker.Controllers;

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
            
            _ui.DisplayMessage("Auto-timer session started. Press any key to end the session...", false);

            var end = DateTime.Now;

            _repo.AddSession(CodingSession.Create(start, end));
        }

    }
}
