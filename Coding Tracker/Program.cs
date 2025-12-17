using Spectre.Console;
using Coding_Tracker;
using Coding_Tracker.Controllers;

var dbPath = "coding_Tracker.db";

var databaseInitialiser = new DatabaseInitialiser(dbPath);
var repo = new CodingSessionRepository(dbPath);

var userInterface = new UserInterface(repo);

userInterface.MainMenu();



