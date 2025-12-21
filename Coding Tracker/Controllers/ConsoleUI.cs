using Spectre.Console;

namespace Coding_Tracker.Controllers
{
    internal class ConsoleUI
    {

        private const string DateFormat = "dd-MM-yyyy HH:mm";
        public void DisplayMessage(string message, bool isError, string colour = "yellow")
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

        public bool ConfirmDeletion(string itemName)
        {
            return AnsiConsole.Confirm($"[red]Are you sure you want to delete[/] [yellow]{itemName}[/]? This action cannot be undone.");
        }

        public DateTime PromptForDateTime(string prompt)
        {
            while (true)
            {
                try
                {
                    var input = AnsiConsole.Ask<string>($"{prompt} (format: {DateFormat.ToLower()}):");
                    return DateTimeValidator.ValidateDateResponse(input, DateFormat);
                }
                catch (FormatException)
                {
                    DisplayMessage($"Invalid date format. Please use the format: {DateFormat.ToLower()}", true);
                }
            }
        }
    }
}
