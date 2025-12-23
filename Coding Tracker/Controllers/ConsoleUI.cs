using Spectre.Console;
using Spectre.Console.Rendering;
using System.Data;

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
            return PromptUntilValid(
                prompt,
                DateFormat.ToLower(),
                input => DateTimeValidator.ValidateDateResponse(input, DateFormat),
                $"Invalid date format. Please use the format: {DateFormat.ToLower()}");
        }

        private T PromptUntilValid<T>(string prompt, string formatHint, Func<string, T> parse, string errorMessage)
        {
            while (true)
            {
                try
                {
                    var input = AnsiConsole.Ask<string>($"{prompt} format: {formatHint}:");
                    return parse(input);
                }
                catch (FormatException)
                {
                    DisplayMessage(errorMessage, true);
                }
            }
        }

        public void DisplayActivityHeatMap(List<CodingSession> sessions)
        {
            var today = DateTime.Today;
            bool isToday = sessions.Any(s => s.StartTime.Date == today);
            var startDate = today.AddDays(-29);

            // Sum minutes per day for records with multiple entries
            var minutesByDay = sessions
                .Where(s => s.StartTime.Date >= startDate && s.StartTime.Date <= today)
                .GroupBy(s => s.StartTime.Date)
                .ToDictionary(g => g.Key, g => g.Sum(s => s.Duration.TotalMinutes));

            // Snap grid start back to Monday
            DateTime gridStart = startDate;
            while (gridStart.DayOfWeek != DayOfWeek.Monday)
                gridStart = gridStart.AddDays(-1);

            // Grid end is 'today'. Compute how many columns (weeks) we need.
            int totalGridDays = (today - gridStart).Days + 1;
            int weekColumns = (int)Math.Ceiling(totalGridDays / 7.0);

            var table = new Table()
                .Border(TableBorder.None)
                .HideHeaders();

            table.AddColumn(new TableColumn(""));
            for (int c = 0; c < weekColumns; c++)
                table.AddColumn(new TableColumn(""));

            string[] labels = { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };

            for (int row = 0; row < 7; row++)
            {
                var cells = new List<IRenderable>();

                // Adding the day labels
                cells.Add(new Markup($"[grey]{labels[row]}[/]"));

                for (int col = 0; col < weekColumns; col++)
                {
                    var day = gridStart.AddDays(col * 7 + row);

                    if (day < startDate || day > today)
                    {
                        cells.Add(new Text("  ", new Style(background: Color.Grey19)));
                        continue;
                    }

                    var minutes = minutesByDay.TryGetValue(day, out var m) ? m : 0;

                    var bg =
                        minutes > 0 && minutes <= 1 ? Color.LightGreen :
                        minutes > 1 && minutes <= 30 ? Color.Green :
                        minutes > 30 ? Color.DarkGreen :
                        Color.Grey;

                    if (isToday && day == today) bg = Color.Blue;

                    cells.Add(new Text("  ", new Style(background: bg)));

                }

                table.AddRow(cells.ToArray());
            }

            AnsiConsole.Write(new Spectre.Console.Rule("[green]Last 30 Days Coding Heatmap[/]").Centered());
            AnsiConsole.Write(table);

            //legend text
            AnsiConsole.MarkupLine("\nLegend:\n" +
                "[on grey]  [/] No record\n" +
                "[on lightgreen]  [/] Up to 30 mins\n" +
                "[on green]  [/] Between 30 mins and up to 120 mins\n" +
                "[on darkgreen]  [/] Greater than 120 mins\n" +
                "[on blue]  [/] Today\n");

            AnsiConsole.MarkupLine("\nPress any key to return to the main menu...");
            Console.ReadKey(true);
        }
    }
}
