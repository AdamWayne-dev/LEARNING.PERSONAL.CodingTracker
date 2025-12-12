using Spectre.Console;

namespace Coding_Tracker.Controllers
{
    internal interface ISessionController
    {
        void StartSession();
        void EndSession();
    }
}
