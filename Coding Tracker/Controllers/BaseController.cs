using Spectre.Console;

namespace Coding_Tracker.Controllers
{
    internal abstract class BaseController
    {
        protected void DisplayMessage(string message, bool isError, string colour = "yellow")
        {
            var panel = new Panel(message)
            {
                Border = BoxBorder.Rounded,
                Padding = new Padding(1, 1),
                Header = new PanelHeader(isError ? "[red]Error[/]" : "[green]Info[/]")
            };
            AnsiConsole.Write(panel);
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[grey]Press any key to continue...[/]");
            Console.ReadKey(true);
        }

        protected bool ConfirmDeletion(string itemName)
        {
            return AnsiConsole.Confirm($"[red]Are you sure you want to delete[/] [yellow]{itemName}[/]? This action cannot be undone.");
        }
    }
}
