using Spectre.Console;
using Microsoft.Extensions.Configuration;
using Coding_Tracker;
using Coding_Tracker.Controllers;

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
    .Build();

var dbPath = configuration["Database:ConnectionString"];

var databaseInitialiser = new DatabaseInitialiser(dbPath);
var repo = new CodingSessionRepository(dbPath);

var userInterface = new UserInterface(repo);

userInterface.MainMenu();



