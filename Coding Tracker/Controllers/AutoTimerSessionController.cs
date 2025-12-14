using Spectre.Console;
using Coding_Tracker.Models;
using Coding_Tracker.Controllers;

namespace Coding_Tracker.Controllers
{
    internal class AutoTimerSessionController : BaseController, ISessionController
    {

        private readonly CodingSessionRepository _repo;
        public AutoTimerSessionController(CodingSessionRepository repo)
        {
            _repo = repo;
        }

        public void StartSession()
        {
            _repo.Add(new CodingSession
            {
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1) // Placeholder end time
            });
            AnsiConsole.Prompt(new SelectionPrompt<string>().
                Title("Auto-timer session started. Press any key to end the session...")
                .AddChoices(new[] { "End Session" }));

        }
        public void EndSession()
        {
            throw new NotImplementedException();
        }
    }
}
