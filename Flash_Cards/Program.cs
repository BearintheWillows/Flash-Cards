using Flash_Cards.Controllers;
using Flash_Cards.Data;
using Flash_Cards.View;
using Microsoft.Extensions.Configuration;
using Models;
using Serilog;

//Configure SeriLog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File( "Logs/log.txt", rollingInterval: RollingInterval.Day )
    .CreateLogger();

//Configure Secrets
var config = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("Secrets.json", optional: true, reloadOnChange: true)
    .AddUserSecrets<Program>()
    .Build();

var connectionString = config.GetConnectionString("DefaultConnection");

///Initialise Database and Create Tables
var db = new FlashCardContext(connectionString);
db.CreateTables();

//Initialise Controllers
var stackController = new StackController(db);
MenuController menuController = new(db, stackController);


bool exitProgram = false;

while (exitProgram == false )
{
    exitProgram = menuController.MainMenu();
}

db.GetCardCountByStackId(1002);