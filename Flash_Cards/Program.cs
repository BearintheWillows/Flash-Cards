using Flash_Cards.Controllers;
using Flash_Cards.Data;
using Flash_Cards.UI;
using Microsoft.Extensions.Configuration;
using Serilog;

//Configure SeriLog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

//Configure Secrets
var config = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("Secrets.json", optional: true, reloadOnChange: true)
    .AddUserSecrets<Program>()
    .Build();

var connectionString = config.GetConnectionString("DefaultConnection");

var db = new FlashCardContext(connectionString);
var stacks = new StackController();

db.CreateTables();

stacks.GetMenuChoice();