using Coding_Tracker.Repositories;
using Spectre.Console;

namespace Coding_Tracker.Controllers
{
    internal class ManualTimerSessionController : ISessionController
    {
        private readonly CodingSessionRepository _repo;
        public ManualTimerSessionController(CodingSessionRepository repo)
        {
            _repo = repo;
        }

        public void StartSession()
        {

            // Prompt the user to use a specific date format and then enforce it.
            AnsiConsole.Prompt(new SelectionPrompt<string>().
               Title("Manual-timer session started. Press any key to end the session...")
               .AddChoices(new[] { "End Session" }));
        }
    }
}
