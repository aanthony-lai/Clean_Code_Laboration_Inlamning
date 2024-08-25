using Clean_Code_Laboration.Controller;
using Clean_Code_Laboration.Controller.Factories;
using Clean_Code_Laboration.Controller.Interfaces;
using Clean_Code_Laboration.Controller.Services;
using Clean_Code_Laboration.Data.Implementations;
using Clean_Code_Laboration.Data.Interfaces;
using Clean_Code_Laboration.GameLogic.Games;
using Clean_Code_Laboration.GameLogic.Implementations;
using Clean_Code_Laboration.GameLogic.Interfaces;
using Clean_Code_Laboration.UI.AbstractClasses;
using Clean_Code_Laboration.UI.Implementations;
using Clean_Code_Laboration.UI.Interfaces;

IConsoleInterface console = new ConsoleUserInterface();
UserInterface userInterface = new MooUI(console);
IGuessChecker guessChecker = new MooGuessChecker();
IGoalGenerator goalGenerator = new MooGoalGenerator();
IGameRegistry gameRegistry = new GameRegistry();
IGameFactory gameFactory = new GameFactory(gameRegistry);
IFileHandler fileHandler = new FileHandler();
IPlayerDataRepository playerDataRepository = new PlayerDataRepository(fileHandler);

IGame game = new MooGame(guessChecker, goalGenerator);
GameController gameController = new GameController(game, gameFactory, userInterface, gameRegistry, playerDataRepository);
gameController.Play();