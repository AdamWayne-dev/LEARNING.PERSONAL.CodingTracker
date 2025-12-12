using Spectre.Console;
using Coding_Tracker.Models;

namespace Coding_Tracker.Controllers
{
    internal class AutoTimerSessionController : BaseController, ISessionController
    {
        public void StartSession()
        {
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
