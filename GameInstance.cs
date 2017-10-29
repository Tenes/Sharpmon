using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sharpmon
{
    public static class GameInstance
    {
        //FIELDS
        public static Random Rng = new Random();                            //The random object that every class will use when it comes to Random number generator.
        public static string Choice;                                        //The value that will be used for stocking (almost) every choices in the game, one at a time.
        private static Player player;                                       //The one and only, main player of the game.
        private static string savePath = "SharpmonSave"; //The path to your future (or existing) save file of the game. Note that this is MY own path, it may not work for you.
        private static UnicodeEncoding ByteConverter = new UnicodeEncoding();
        //METHODS
        public static void Run()
        {   
            /*A little ASCII Art used to offer a better user experience.*/
            Console.WriteLine("\t _____ _   _   ___  _______________  ________ _   _ \n" +
                              "\t/  ___| | | | / _ \\ | ___ \\ ___ \\  \\/  |  _  | \\ | |\n" +
                              "\t\\ `--.| |_| |/ /_\\ \\| |_/ / |_/ / .  . | | | |  \\| |\n" +
                              "\t `--. \\  _  ||  _  ||    /|  __/| |\\/| | | | | . ` |\n" +
                              "\t/\\__/ / | | || | | || |\\ \\| |   | |  | \\ \\_/ / |\\  |\n" +
                              "\t\\____/\\_| |_/\\_| |_/\\_| \\_\\_|   \\_|  |_/\\___/\\_| \\_/\n");
            
            /*Check if a save file of our game exist at the savepath entered. If so, display a little Menu so that
              the player can choose if he wants to continue his saved game or if he wants to start a new one.*/
            if (File.Exists(savePath))
            {
                Console.WriteLine("\t\t\t0: New Game\n\t\t\t1: Continue");
                while (true)
                {
                    Choice = Console.ReadLine();
                    switch (Choice.ToLower())
                    {
                        case "0":
                        case "start":
                        case "new game":
                        case "new":
                            NewGame();
                            break;
                        case "1":
                        case "continue":
                        case "charge":
                            DeserializeItem(savePath);
                            PrincipalScene();
                            break;
                        default:
                            Console.WriteLine("Enter a valid input.");
                            break;
                    }
                }
            }
            /*If no save file exists at the savepath entered, a new game is the only option.*/
            else
            {
                Console.WriteLine("\t\tPress any key to start a new game");
                Console.ReadKey();
                NewGame();
            }

        }
        
        /// <summary>
        /// Method that instanciate a new player into our static player variable. Isn't called if the player is loaded.
        /// </summary>
        private static void NewGame()
        {
            Console.Clear();
            Console.WriteLine(
                "Greetings adventurer!\nBefore you enter the wonderful world of Sharpmon, please enter your name:");
            player = new Player(Console.ReadLine());

            PrincipalScene();
        }

        /// <summary>
        /// Static method representing the fact that the player is currently into the Main Menu having plenty of choices.
        /// </summary>
        private static void PrincipalScene()
        {
            while (true)
            {
                /*Those few lines before the switch are mostly here to offer a better user exeperience but also to display
                  to the player what are his options and what is important to be known (such as his name, his sharpdollars and his main sharpmon).*/
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"Name: {player.Name} \t\tSharpdollars: {player.SharpDollars}\n" +
                              "Current Sharpmon: ");
                player.GetCurrentSharpmon().GetColorElementalType();
                Console.Write($"{player.GetCurrentSharpmon().Name} ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"level {player.GetCurrentSharpmon().GetLevel()} ({player.GetCurrentSharpmon().CurrentHp}/{player.GetCurrentSharpmon().MaxHp} Hp)\n\n");
                Console.ForegroundColor = ConsoleColor.Gray;
                if(player.currentTown != Sharpdex.Towns.Last())
                {
                    Console.WriteLine($"You're currently in {player.currentTown}.\nWhere do you want to go?\n\t0: Into the wild\n\t1: Fight for the {Sharpdex.Badges[Sharpdex.Towns.IndexOf(player.currentTown)]} badge\n\t2: Change current Sharpmon\n\t3: In the shop\n" +
                                    "\t4: In the Sharpmon Center\n\t5: Save Game\n\t6: Exit Game");
                }
                else
                {
                    Console.WriteLine($"You're currently in {player.currentTown}.\nWhere do you want to go?\n\t0: Into the wild\n\t1: Fight {player.currentTown}\n\t2: Change current Sharpmon\n\t3: In the shop\n" +
                                  "\t4: In the Sharpmon Center\n\t5: Save Game\n\t6: Exit Game");
                }

                Choice = Console.ReadLine().ToLower();
                /*If the player enter 0 or "into the wild" he's transported to the discovering Scene that will eventually lead him to a fight.
                  If he chooses 1 or "arena" he'll fight for the current town's badge.
                  If he chooses 2 or "change" he'll be able to change his current main sharpmon.
                  If he chooses 3 or "in the shop", he will be transported to the Shop so that he can buy or sell items.
                  If he chooses 4 or "in the sharpmon center", he will be transported to the sharpmon center so that he can heal his sharpmon or simply see all of them and their current Hp/Level.
                  If he chooses 5, "save" or "save game", the game will be saved. It means that all the player's informations will be Serialized into a .txt file.
                  If he chosses 6 or "exit game", the game will instantly be shut down.
                  If he enters none of the above input, he will be asked to re-enter an input in hope of matching the above inputs.*/
                switch (Choice)
                {
                    case "0":
                    case "into the wild":
                    case "wild":
                        if(player.GetCurrentSharpmon().IsAlive())
                            Discovering();
                        break;
                    case "1":
                    case "fight the next arena":
                    case "arena":
                        if(player.GetCurrentSharpmon().IsAlive())
                            FightNextArena();
                        break;
                    case "2":
                    case "change":
                        ChangeCurrentSharpmon();
                        continue;
                    case "3":
                    case "in the shop":
                    case "shop":
                        ShopScene();
                        break;
                    case "4":
                    case "in the sharpmon center":
                    case "center":
                        SharpmonCenterScene();
                        break;
                    case "5":
                    case "save":
                    case "save game":
                        SerializeItem(savePath);
                        continue;
                    case "6":
                    case "exit game":
                    case "exit":
                        Environment.Exit(0);
                        return;
                    case "7":
                        player.GetCurrentSharpmon().SetExp(90000000);
                        for(int i = 0; i < 5; i++)
                            player.GetSharpmons().Add(Sharpmon.CopySharpmon(Sharpmon.GetRandomSharpmon(Rng.Next(Sharpdex.AllSharpmons.Count))));
                        player.GetCurrentSharpmon().CheckForLevelUp();
                        break;
                    default:
                        Console.WriteLine("Please enter a valid input.\nPress any key to continue.");
                        Console.ReadKey();
                        continue;
                }
            }
        }

        /// <summary>
        /// Static method used as a substitute of a loading screen to offer
        /// a better user experience but also change the range of the discoverable sharpmons
        /// based on the current Sharpmon's level.
        /// </summary>
        private static void Discovering()
        {
            Console.Clear();
            Console.Write("You're currently walking trought the grass");
            for (int i = 0; i < Rng.Next(10, 30); i++)
            {
                if (i == 15)
                {
                    Console.Clear();
                    Console.Write("You're currently walking trought the grass");
                }
                System.Threading.Thread.Sleep(100);
                Console.Write(".");
            }

            Console.Clear();

            Sharpmon Ennemy;
            if (player.GetCurrentSharpmon().GetLevel() < 30)
                Ennemy = Sharpmon.CopySharpmon(Sharpmon.GetRandomSharpmon(Rng.Next(Sharpdex.AllSharpmons.Count - 27)));      //Allow the discovering of common Sharpmons.
            else if (player.GetCurrentSharpmon().GetLevel() >= 30 && player.GetCurrentSharpmon().GetLevel() < 40)
                Ennemy = Sharpmon.CopySharpmon(Sharpmon.GetRandomSharpmon(Rng.Next(Sharpdex.AllSharpmons.Count - 6)));       //Allow the discovering of a possible Advanced Sharpmon.
            else 
                Ennemy = Sharpmon.CopySharpmon(Sharpmon.GetRandomSharpmon(Rng.Next(Sharpdex.AllSharpmons.Count)));           //Allow the discovering of a possible Legendary Sharpmon.

            for (int i = 0; i < player.GetCurrentSharpmon().GetLevel()-1; i++)
                Ennemy.OnLevelUp();

            Console.Write($"A wild {Ennemy.Name} appeared!\n\nLoading battle.");
            Loading(150, 10);
            FightScene(Ennemy);
        }

        /// <summary>
        /// Static method representing the fact that the player is currently against a specific sharpmon.
        /// From there he can choose what to do next.
        /// </summary>
        private static void FightScene(Sharpmon ennemy, bool arenaFight = false)
        {
            while (true)
            {
                Console.Clear();
                ennemy.DrawTable();
                player.GetCurrentSharpmon().DrawTable();

                Console.WriteLine("\n\t\t\tWhat will you do?\n\t\t\t\t0: Attack\n\t\t\t\t1: Change Sharpmon\n\t\t\t\t2: Use item\n\t\t\t\t3: Capture\n\t\t\t\t4: Run away");
                Choice = Console.ReadLine().ToLower();
                switch (Choice)
                {
                    case "0":
                    case "attack":
                        FightSystem(player.GetCurrentSharpmon(), ennemy, arenaFight);
                        return;
                    case "1":
                    case "change sharpmon":
                        ChangeSystem(ennemy, arenaFight);
                        return;
                    case "2":
                    case "use item":
                        ItemSystem(ennemy, arenaFight);
                        return;
                    case "3":
                    case "capture":
                        if (!arenaFight)
                        {
                            if(player.GetSharpmons().Count < 6)
                            {
                                Console.WriteLine($"You threw a Sharpball at {ennemy.Name}!");
                                CaptureSystem(player.GetCurrentSharpmon(), ennemy);
                            }
                            else
                            {
                                Console.WriteLine("You can't capture more sharpmons unless you put some into your PC! (in the Sharpmon Center)\nPress a key to continue the fight.");
                                Console.ReadKey();
                                continue;
                            }
                        }
                        else
                        {
                            Console.WriteLine("You can't capture someone's sharpmon!\nPress a key to continue the fight.");
                            Console.ReadKey();
                            continue;
                        }
                        return;
                    case "4":
                    case "run away":
                        if (!arenaFight)
                        {
                            EscapeSystem(player.GetCurrentSharpmon(), ennemy);    
                            return;
                        }
                        else
                        {
                            Console.WriteLine("You can't flee from an arena fight until all your sharpmons are KO!\nPress a key to continue the fight.");
                            Console.ReadKey();
                            continue;
                        }
                    default:
                        Console.WriteLine("Please enter a valid input.\nPress a key to continue the fight.");
                        Console.ReadKey();
                        continue;
                }
            }
        }

        private static void FightNextArena()
        {
            Console.Clear();
            Sharpmon Ennemy = Sharpdex.ArenaSharpmons.ElementAt(Sharpdex.Towns.IndexOf(player.currentTown)).Key;
            for (int i = 0; i < Sharpdex.ArenaSharpmons.ElementAt(Sharpdex.Towns.IndexOf(player.currentTown)).Value - 1; i++)
                Ennemy.OnLevelUp();
            Console.Write($"The gym leader accepted your challenge with his");
            Ennemy.ToString();
            Console.Write("!\n\nPrepare yourself.");
            Loading(150, 10);
            FightScene(Ennemy, true);
        }
        /// <summary>
        /// Static method representing the fact that the player is currently in the shop.
        /// From there he can choose what to do next.
        /// </summary>
        private static void ShopScene()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the SharpShop!\nWhat do you want to do?\n\t0: Buy items\n\t1: Sell items\n\t2: Exit the shop");
                Choice = Console.ReadLine().ToLower();
                switch (Choice)
                {
                    case "0":
                    case "buy":
                    case "buy item":
                        BuyShop();
                        break;
                    case "1":
                    case "sell":
                    case "sell item":
                        SellShop();
                        break;
                    case "2":
                    case "exit":
                    case "exit shop":
                        Console.Write("Heading back to town");
                        Loading(100, 10);
                        return;
                    default:
                        Console.WriteLine("Please enter a valid input.\nPress any key to continue.");
                        Console.ReadKey();
                        continue;
                }
            }
        }

        /// <summary>
        /// Static method representing the fact that the player is currently into the Sharpmon Center.
        /// From there he can choose what to do next.
        /// </summary>
        private static void SharpmonCenterScene()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome in the Sharpmon Center! Do you want to heal all your sharpmons or exit the center?\n\t0: Heal all the sharpmons");
                player.GetSharpmons().ForEach(sharpmon => Console.WriteLine($"\t\t{sharpmon.Name} (Level {sharpmon.GetLevel()}): {sharpmon.CurrentHp}/{sharpmon.MaxHp}"));
                Console.WriteLine("\n\t1: Launch your PC\n\t2: Exit center");
                Choice = Console.ReadLine().ToLower();
                switch (Choice)
                {
                    case "0":
                    case "heal":
                    case "heal all the sharpmons":
                        player.GetSharpmons().ForEach(sharpmon => sharpmon.CurrentHp = sharpmon.MaxHp);
                        Console.Clear();
                        Console.Write("All your Sharpmons have been healed!\nYou're now exiting the center.\nExiting the center.");
                        Loading(200, 10);
                        return;
                    case "1":
                    case "pc":
                        Console.Write("Launching your PC.");
                        Loading(100, 10);
                        LaunchPC();
                        continue;
                    case "2":
                    case "exit center":
                        Console.Write("Exiting the center.");
                        Loading(100, 10);
                        return;
                    default:
                        continue;
                }
            }
        }
        private static void LaunchPC()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("You're logged into your PC!\nDo you want to store your sharpmons, take one or log out?\n\t0: Store a sharpmon\n\t1: Take a sharpmon\n\t2: Log out");
            
                Choice = Console.ReadLine().ToLower();
                switch (Choice)
                {
                    case "0":
                    case "store":
                        StoreInPC();
                        continue;
                    case "1":
                    case "take":
                        TakeFromPC();
                        continue;
                    case "2":
                    case "log off":
                        return;
                    default:
                        continue;
                }
            }
        }
        private static void StoreInPC()
        {
            Console.WriteLine("Choose a Sharpmon to store in your PC:");
            for (int i = 0; i < player.GetSharpmons().Count; i++)
            { 
                player.GetSharpmons()[i].GetColorElementalType();
                Console.WriteLine($"\t\t{i}: {player.GetSharpmons()[i].Name} (Hp: {player.GetSharpmons()[i].CurrentHp}/{player.GetSharpmons()[i].MaxHp})");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.WriteLine($"\n\tEnter: Return to the PC's menu");
            int parsedChoice;
            while (true)
            {
                Choice = Console.ReadLine();
                if (int.TryParse(Choice, out parsedChoice) && parsedChoice >= 0 && parsedChoice < player.GetSharpmons().Count)
                {
                    if(player.GetSharpmons().Count > 1)
                    {
                        Console.Write("Placing");
                        player.GetSharpmons()[parsedChoice].ToString();
                        Console.WriteLine("in your PC.");
                        player.AddSharpmonInPC(player.GetSharpmons()[parsedChoice]);
                        player.GetSharpmons().RemoveAt(parsedChoice);
                        Loading(150, 5);
                        return;
                    }
                    else
                    {
                        Console.WriteLine("You can't store your last sharpmon.\nPress any key to return to the PC's menu");
                        Console.ReadKey();
                        return;
                    }
                }
                else if(Choice == "")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Enter a valid input.");
                }
            }
        }
        private static void TakeFromPC()
        {
            int page = 1;
            int pageMax = (int)Math.Ceiling(player.GetSharpmonsInPC().Count/8f);
            int parsedChoice;
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Choose a Sharpmon from your PC:\t(Page {page}/{pageMax})");
                for (int i = (page-1)*8; i < ((page < pageMax) ? page*8: ((page-1)*8)+player.GetSharpmonsInPC().Count%8); i++)
                {
                    player.GetSharpmonsInPC()[i].GetColorElementalType();
                    Console.WriteLine($"\t\t{i-((page-1)*8)}: {player.GetSharpmonsInPC()[i].Name} (Hp: {player.GetSharpmonsInPC()[i].CurrentHp}/{player.GetSharpmonsInPC()[i].MaxHp})");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                if(page < pageMax)
                {
                    Console.Write($"\t8: Next page");
                }
                if(page > 1)
                {
                    Console.Write($"\n\t9 Previous page");
                }
                Console.WriteLine("\n\tEnter: Return to the PC's menu");


                Choice = Console.ReadLine();
                if (int.TryParse(Choice, out parsedChoice) && parsedChoice >= 0 && parsedChoice < player.GetSharpmonsInPC().Count%(page*8))
                {
                    if(player.GetSharpmons().Count < 6)
                    {
                        Console.Write("Placing");
                        player.GetSharpmonsInPC()[(page-1)*8+parsedChoice].ToString();
                        Console.WriteLine("in your inventory.");
                        player.GetSharpmons().Add((player.GetSharpmonsInPC()[(page-1)*8+parsedChoice]));
                        player.GetSharpmonsInPC().RemoveAt((page-1)*8+parsedChoice); 
                        Loading(200, 5);
                        return;
                    }
                    else
                    {
                        Console.WriteLine("You already have the maximum number of sharpmons in your inventory.\nReturning to the PC's menu.");
                        Loading(300, 9);
                        return;
                    }
                }
                else if(int.TryParse(Choice, out parsedChoice) && parsedChoice == 8)
                {
                    page++;
                    continue;
                }
                else if(int.TryParse(Choice, out parsedChoice) && parsedChoice == 9)
                {
                    page--;
                    continue;
                }
                else if(Choice == "")
                {
                    return;
                }
            }
        }
        /// <summary>
        /// Method to draw the sharpmon's stat table in the color of its element.
        /// </summary>
        /// <param name="entity"></param>
        private static void DrawTable(this Sharpmon entity)
        {
            entity.GetColorElementalType();
            Console.WriteLine($"\t _______________________________________________\n" +
                              $"\t|\t{entity.Name} Level {entity.GetLevel()}: \tHp: {entity.CurrentHp}/{entity.MaxHp}   \t|\n" +
                              $"\t|\tPow: {entity.CurrentPower}   \tDef: {entity.CurrentDefense}  \t\t|\n" +
                              $"\t|\tAcc: {entity.CurrentAccucary}   \tDodge: {entity.CurrentDodge}\t\t|\n" +
                              $"\t|\tSpd: {entity.CurrentSpeed}   \t\t\t\t|\n" +
                              $"\t|_______________________________________________|");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Method used to determine who attack first and what attack does the computer.
        /// Before and after every action, a check is done to see if the receiver of an attack died from it.
        /// </summary>
        /// <param name="currentSharpmon"></param>
        /// <param name="ennemy"></param>
        private static void FightSystem(Sharpmon currentSharpmon, Sharpmon ennemy, bool arenaFight = false)
        {
            while (true)
            {
                if (currentSharpmon.CurrentSpeed > ennemy.CurrentSpeed)
                {
                    if (currentSharpmon.IsAlive())
                        currentSharpmon.ChooseAttack().LaunchAttack(currentSharpmon, ennemy);
                    else
                    {
                        ChangeSystem(ennemy, arenaFight);
                        return;
                    }

                    if (ennemy.IsAlive())
                        ennemy.GetAttack(Rng.Next(ennemy.GetAttacks().Count)).LaunchAttack(ennemy, currentSharpmon);
                    else
                    {
                        currentSharpmon.Win(ennemy, arenaFight);
                        return;
                    }
                    if (!currentSharpmon.IsAlive())
                    {
                        ChangeSystem(ennemy, arenaFight);
                        return;
                    }

                    EndTurn(ennemy, arenaFight);
                    return;
                }
                else if (currentSharpmon.CurrentSpeed < ennemy.CurrentSpeed)
                {
                    Attack tempAttack = currentSharpmon.ChooseAttack();
                    if (ennemy.IsAlive())
                        ennemy.GetAttack(Rng.Next(ennemy.GetAttacks().Count)).LaunchAttack(ennemy, currentSharpmon);
                    else
                    {
                        currentSharpmon.Win(ennemy, arenaFight);
                        return;
                    }

                    if (currentSharpmon.IsAlive())
                        tempAttack.LaunchAttack(currentSharpmon, ennemy);
                    else
                    {
                        ChangeSystem(ennemy, arenaFight);
                        return;
                    }
                    if(!ennemy.IsAlive())
                    {
                        currentSharpmon.Win(ennemy, arenaFight);
                        return;
                    }

                    EndTurn(ennemy, arenaFight);
                    return;
                }
                else if (currentSharpmon.CurrentSpeed == ennemy.CurrentSpeed)
                {
                    Attack tempAttack = currentSharpmon.ChooseAttack();
                    if (Rng.Next(2) == 1)
                    {
                        if (ennemy.IsAlive())
                            ennemy.GetAttack(Rng.Next(ennemy.GetAttacks().Count)).LaunchAttack(ennemy, currentSharpmon);
                        else
                        {
                            currentSharpmon.Win(ennemy, arenaFight);
                            return;
                        }

                        if (currentSharpmon.IsAlive())
                            tempAttack.LaunchAttack(currentSharpmon, ennemy);
                        else
                        {
                            ChangeSystem(ennemy, arenaFight);
                            return;
                        }
                        if (!ennemy.IsAlive())
                        {
                            currentSharpmon.Win(ennemy, arenaFight);
                            return;
                        }

                        EndTurn(ennemy, arenaFight);
                        return;
                    }
                    else
                    {
                        if (currentSharpmon.IsAlive())
                            tempAttack.LaunchAttack(currentSharpmon, ennemy);
                        else
                        {
                            ChangeSystem(ennemy, arenaFight);
                            return;
                        }

                        if (ennemy.IsAlive())
                            ennemy.GetAttack(Rng.Next(ennemy.GetAttacks().Count)).LaunchAttack(ennemy, currentSharpmon);
                        else
                        {
                            currentSharpmon.Win(ennemy, arenaFight);
                            return;
                        }
                        if (!currentSharpmon.IsAlive())
                        {
                            ChangeSystem(ennemy, arenaFight);
                            return;
                        }

                        EndTurn(ennemy, arenaFight);
                        return;
                    }
                }
            }

        }

        /// <summary>
        /// Method that allows the user to choose the attack of its sharpmon he wants to use.
        /// </summary>
        /// <param name="currentSharpmon"></param>
        /// <param name="ennemy"></param>
        private static Attack ChooseAttack(this Sharpmon currentSharpmon)
        {
            Console.WriteLine($"Choose your attack:");
            for (int i = 0; i < currentSharpmon.GetAttacks().Count; i++)
                Console.WriteLine($"\t {i}: {currentSharpmon.GetAttack(i).GetName()}");

            while (true)
            {
                Choice = Console.ReadLine().ToLower();
                for (int i = 0; i < currentSharpmon.GetAttacks().Count; i++)
                {
                    if (Choice == currentSharpmon.GetAttack(i).GetName().ToLower() || Choice == $"{i}")
                    {
                        return currentSharpmon.GetAttack(i);
                    }
                }
                Console.WriteLine("Please entere a valid index or name.");
            }
        }
        /// <summary>
        /// Method that tells the user that the current turn has ended. It automaticly begins a new one.
        /// </summary>
        /// <param name="ennemy"></param>
        private static void EndTurn(Sharpmon ennemy, bool arenaFight = false)
        {
            Console.WriteLine("Both Sharpmon attacked, press any key to continue the fight.");
            Console.ReadKey();
            FightScene(ennemy, arenaFight);
            return;
        }
        /// <summary>
        /// Method that displays to the player how much experience and sharpdollars his current sharpmon
        /// and himself won with this fight. It sign the end of the fight and allow the user to return to
        /// the principal scene.
        /// </summary>
        /// <param name="currentSharpmon"></param>
        /// <param name="ennemy"></param>
        private static void Win(this Sharpmon currentSharpmon, Sharpmon ennemy, bool arenaFight = false)
        {
            Console.WriteLine($"{ennemy.Name} fainted.\nPress any key to continue.");
            Console.ReadKey();
            Console.Clear();
            int temp = (arenaFight) ? Rng.Next(50, 70) * ennemy.GetLevel() : Rng.Next(120, 150) * ennemy.GetLevel();
            currentSharpmon.CurrentExperience += temp;

            Console.WriteLine($"Your {currentSharpmon.Name} won {temp} experience points!");
            if(!arenaFight)
            {
                temp = Rng.Next(50, 100);
                player.SharpDollars += temp;
                Console.Write($"You won {temp} SharpDollars!\nHeading back to town");
                Loading(150, 15);
            }
            else
            {
                if(player.currentTown != Sharpdex.Towns.Last())
                {
                    temp = Rng.Next(2000);
                    player.SharpDollars += temp;
                    Console.Write($"You defeated the gym leader and earned the {Sharpdex.Badges[Sharpdex.Towns.IndexOf(player.currentTown)]} Badge!\nThe gym leader also gave you {temp} SharpDollars!\nHeading to {Sharpdex.Towns[Sharpdex.Towns.IndexOf(player.currentTown)+1]}");
                    player.currentTown = Sharpdex.Towns[Sharpdex.Towns.IndexOf(player.currentTown)+1];
                    Loading(300, 15);
                }
                else
                {
                    temp = Rng.Next(3000, 6000);
                    player.SharpDollars += temp;
                    Console.Write($"You defeated {player.currentTown} and acquired the title of Sharpmons' Champion Badge!\nThe eliter four also gave you {temp} SharpDollars to congratulate you!\nHeading back to {player.currentTown}.");
                    Loading(300, 20);
                }
            }
            currentSharpmon.CheckForLevelUp();
            CheckEmptyInventory();
            return;
        }

        private static void ChangeCurrentSharpmon()
        {
            string choice;
            int ParsedChoice;
            while (true)
            {
                Console.WriteLine("Choose which one of your sharpmon you want as your current one:");
                for (int i = 0; i < player.GetSharpmons().Count; i++)
                { 
                    player.GetSharpmons()[i].GetColorElementalType();
                    Console.WriteLine($"\t\t{i}: {player.GetSharpmons()[i].Name} (Hp: {player.GetSharpmons()[i].CurrentHp}/{player.GetSharpmons()[i].MaxHp})");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                choice = Console.ReadLine();
                if (int.TryParse(choice, out ParsedChoice) && ParsedChoice >= 0 && ParsedChoice < player.GetSharpmons().Count)
                {
                    Sharpmon TemporarySharpmon = player.GetCurrentSharpmon();
                    player.GetSharpmons()[0] = player.GetSharpmons()[ParsedChoice];
                    player.GetSharpmons()[ParsedChoice] = TemporarySharpmon;
                    return;
                }
                else
                    Console.WriteLine("Please enter a valid input.");
            }
            
        }
        /// <summary>
        /// Method that changes (or not) the current sharpmon to another one in the player's sharpmon list.
        /// </summary>
        /// <param name="ennemy"></param>
        private static void ChangeSystem(Sharpmon ennemy, bool arenaFight = false)
        {
            
            Console.WriteLine("You current Sharpmon fainted!");
            string choice;
            int ParsedChoice;
            while (true)
            {
                Console.WriteLine("Choose a Sharpmon:");
                for (int i = 0; i < player.GetSharpmons().Count; i++)
                { 
                    player.GetSharpmons()[i].GetColorElementalType();
                    Console.WriteLine($"\t\t{i}: {player.GetSharpmons()[i].Name} (Hp: {player.GetSharpmons()[i].CurrentHp}/{player.GetSharpmons()[i].MaxHp})");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine($"\n\t{player.GetSharpmons().Count}: Continue the fight with current the Sharpmon\n\t{player.GetSharpmons().Count + 1}: Leave the fight");
                choice = Console.ReadLine();
                if (int.TryParse(choice, out ParsedChoice) && ParsedChoice > 0 && ParsedChoice < player.GetSharpmons().Count)
                {
                    if (player.GetSharpmons()[ParsedChoice].IsAlive())
                    {
                        Sharpmon TemporarySharpmon = player.GetCurrentSharpmon();
                        player.GetSharpmons()[0] = player.GetSharpmons()[ParsedChoice];
                        player.GetSharpmons()[ParsedChoice] = TemporarySharpmon;
                        Console.WriteLine($"I choose you {player.GetCurrentSharpmon().Name}!");
                        Loading(350, 3);
                        FightScene(ennemy, arenaFight);
                        return;
                    }
                    else
                    {
                        Console.WriteLine("You can't choose this sharpmon because he's KO.");
                        continue;
                    }

                }
                else if (int.TryParse(choice, out ParsedChoice) &&(ParsedChoice == player.GetSharpmons().Count || ParsedChoice == 0))
                {
                    if (!player.GetCurrentSharpmon().IsAlive())
                    {
                        Console.Write("Your current Sharpmon is KO.");
                        continue;
                    }
                    else
                    {
                        FightScene(ennemy, arenaFight);
                        return;
                    }
                }
                else if (int.TryParse(choice, out ParsedChoice) && ParsedChoice == player.GetSharpmons().Count + 1)
                {
                    if(player.GetSharpmons().Where(sharpmon => sharpmon.CurrentHp > 0).Any())
                    {
                        Console.WriteLine("You can't run away from a fight if any of your sharpmon is alive!");
                        continue;
                    }
                    else
                    {
                        Console.Write("You fled the fight.\nHeading back to town");
                        Loading(150,10);
                        return;
                    }
                }
                else
                    Console.WriteLine("Please enter a valid input.");
            }
            
        }
        /// <summary>
        /// Method that allows the use of items in fight.
        /// </summary>
        /// <param name="ennemy"></param>
        private static void ItemSystem(Sharpmon ennemy, bool arenaFight = false)
        {
            if (!player.GetItems().Any())
            {
                Console.WriteLine("Your inventory is empty.\nPress any key to return to the fight.");
                Console.ReadKey();
                FightScene(ennemy, arenaFight);
                return;
            }
            else
            {
                Inventory();
                Console.Write("Enter the name of the item you want to use: (Press enter to return to the fight)");
                string choice;
                string tempChoice;
                int hiddenCount = 0;
                int ParsedChoice = 0;
                while (true)
                {
                    choice = Console.ReadLine();
                    if (Item.ContainItem(choice, player.GetItems()))
                    {
                        Console.WriteLine("On which Sharpmon do you want to use it?");
                        foreach (Sharpmon sharpmon in player.GetSharpmons())
                        {
                            Console.WriteLine($"\t{hiddenCount}: {sharpmon.Name} (Level {sharpmon.GetLevel()}): {sharpmon.CurrentHp} /{sharpmon.MaxHp}");
                            hiddenCount++;
                        }
                        while (true)
                        {
                            tempChoice = Console.ReadLine();
                            if (int.TryParse(tempChoice, out ParsedChoice) && (ParsedChoice >= 0 && ParsedChoice < player.GetSharpmons().Count))
                            {
                                /*Use the selected item on the selected sharpmon (if the usage is needed in case of potions).
                                  The item is destroyed after the usage.*/
                                if(player.GetItems()[player.GetItems().IndexOf(Item.GetItem(choice, player.GetItems()))].Use(player.GetSharpmons()[ParsedChoice], ennemy))
                                {
                                    player.GetItems().RemoveAt(player.GetItems().IndexOf(Item.GetItem(choice, player.GetItems())));

                                    /*The ennemy always execute an attack after the usage of an item.*/
                                    ennemy.GetAttack(Rng.Next(2)).LaunchAttack(ennemy, player.GetCurrentSharpmon());
                                    if (!player.GetCurrentSharpmon().IsAlive())
                                    {
                                        ChangeSystem(ennemy, arenaFight);
                                        return;
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nPress any key to continue.");
                                        Console.ReadKey();
                                        FightScene(ennemy, arenaFight);
                                        return;
                                    }
                                }
                                else
                                    break;
                            }
                            else
                                Console.WriteLine("Please enter a valid Sharpmon name.");
                        }
                    }
                    else if (choice == "")
                    {
                        FightScene(ennemy, arenaFight);
                        return;
                    }
                    else
                        Console.WriteLine("Please enter a valid name.");
                }
            }
        }

        /// <summary>
        /// Method that allows the capture of a sharpmon (it does a full copy of the ennemy if successfull).
        /// </summary>
        /// <param name="CurrentSharpmon"></param>
        /// <param name="ennemy"></param>
        private static void CaptureSystem(Sharpmon CurrentSharpmon, Sharpmon ennemy)
        {
            Console.Write("Capturing .");
            double CaptureSuccess = (double)(3 * ennemy.MaxHp - 2 * ennemy.CurrentHp) / (3 * ennemy.MaxHp);
            if (Rng.Next(101) <= CaptureSuccess * 100)
            {
                Loading(200, 8);
                player.AddSharpmon(ennemy);
                Console.WriteLine($"\nYou captured a {ennemy.Name}!");
                Console.Write($"Heading back to town.");
                for (int i = 0; i < 10; i++)
                {
                    System.Threading.Thread.Sleep(150);
                    Console.Write(".");
                }
                return;
            }
            else
            {
                Loading(200, Functions.Choose(2, 4, 6));
                Console.WriteLine($"\n{ennemy.Name} broke free!");
                ennemy.GetAttack(Rng.Next(2)).LaunchAttack(ennemy, CurrentSharpmon);
                if (!CurrentSharpmon.IsAlive())
                {
                    Console.WriteLine("You current Sharpmon fainted!");
                    ChangeSystem(ennemy);
                    return;
                }
                else
                {
                    Console.WriteLine("The turn has ended. Press any key to continue.");
                    Console.ReadKey();
                    FightScene(ennemy);
                    return;
                }

            }
        }
        /// <summary>
        /// Method that allow the user to run away from a fight if he wants or if he doesn't have any more sharpmon to fight.
        /// If it's a success, the player return back to town.
        /// </summary>
        /// <param name="CurrentSharpmon"></param>
        /// <param name="ennemy"></param>
        private static void EscapeSystem(Sharpmon CurrentSharpmon, Sharpmon ennemy)
        {
            double RunAwaySuccess = (double)CurrentSharpmon.CurrentDodge / ennemy.CurrentDodge;
            if (Rng.Next(101) <= RunAwaySuccess*100)
            {
                Console.WriteLine("You fled successfully.\nHeading back to the city");
                Console.Write("You're currently walking trought the grass");
                Loading(150, Rng.Next(10, 15));
                return;
            }
            else
            {
                Console.WriteLine("You failed your escape.");
                ennemy.GetAttack(Rng.Next(2)).LaunchAttack(ennemy, player.GetCurrentSharpmon());
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                FightScene(ennemy);
                return;
            }
        }

        /// <summary>
        /// Display dynamically all the item in the player inventory.
        /// </summary>
        private static void Inventory()
        {
            Console.WriteLine("Your inventory contains:");
            foreach (Item item in Sharpdex.AllItems)
            {
                if (Item.ContainItem(item.GetName(), player.GetItems()))
                    Console.WriteLine($"{item.GetName()} \tx{Item.GetNumberOfItem(item.GetName(), player.GetItems())}\t|{item.GetDescription()}");
            }
        }
        /// <summary>
        /// Check if the player's list of item (so his inventory) is empty. If so, it clears the list to set the .Count() back to Zero. (Remove let it at 1 even tho everything is removed)
        /// </summary>
        private static void CheckEmptyInventory()
        {
            int hiddenCount = 0;
            foreach (Item possibeItem in Sharpdex.AllItems)
            {
                if (Item.ContainItem(possibeItem.GetName(), player.GetItems()))
                    hiddenCount++;
            }
            if (hiddenCount == 0)
                player.GetItems().Clear();
        }
        /// <summary>
        /// Method that allows the user to buy items and put them in its inventory (list of items).
        /// </summary>
        private static void BuyShop()
        {
            Console.Clear();
            Console.WriteLine($"\tYou currently have {player.SharpDollars} sharpdollars.\n" +
                              "\tWhich item do you want? (Press enter if you want to leave)");
            for (int i = 0; i < Sharpdex.AllItems.Count; i++)
                Console.WriteLine($"\t{i}: {Sharpdex.AllItems[i].GetName()} \tPrice: {Sharpdex.AllItems[i].GetPrice()}");

            while (true)
            {
                Choice = Console.ReadLine().ToLower();
                int ParsedChoise = 0;
                if (Choice != "")
                {
                    for (int i = 0; i < Sharpdex.AllItems.Count; i++)
                    {
                        if (Choice == $"{Sharpdex.AllItems[i].GetName().ToLower()}" || Choice == $"{i}")
                        {
                            Console.WriteLine($"How much {Sharpdex.AllItems[i].GetName()} do you want?");
                            Choice = Console.ReadLine().ToLower();
                            if ((int.TryParse(Choice, out ParsedChoise) && ParsedChoise > 0) && player.SharpDollars - Sharpdex.AllItems[i].GetPrice()* ParsedChoise >= 0)
                            {
                                for (int j = ParsedChoise; j > 0; j--)
                                {
                                    player.AddItem(Sharpdex.AllItems[i]);
                                    player.SharpDollars -= Sharpdex.AllItems[i].GetPrice();
                                }

                                Console.WriteLine(
                                    $"You successfully bought {ParsedChoise} {Sharpdex.AllItems[i].GetName()}\n");
                                Console.Write("Returning to shop");
                                Loading(150, 10);
                                return;
                            }
                            else
                            {
                                Console.WriteLine("You don't have enought money or you did a wrong input.");
                                break;
                            }
                        }
                    }
                    Console.WriteLine("Please choose a valid item.");
                }
                else
                    return;
            }
        }
        /// <summary>
        /// Method that allow the user to sell his belonging items (only if he has any).
        /// </summary>
        private static void SellShop()
        {
            Console.Clear();
            if (player.GetItems().Count == 0)
            {
                Console.WriteLine("Your inventory is empty.\nPress any key to return to the shop.");
                Console.ReadKey();
                return;
            }
            else
            {
                Console.WriteLine("Your inventory contains:");
                foreach (Item item in Sharpdex.AllItems)
                {
                    if (Item.ContainItem(item.GetName(), player.GetItems()))
                        Console.WriteLine($"{item.GetName()} x{Item.GetNumberOfItem(item.GetName(), player.GetItems())} \tSelling Price:{item.GetSellPrice()}  |{item.GetDescription()}");
                }
                /*As in the item system, the name is asked to select the item you want to sell because of the grouping of them
                  (grouping done to offer a better user experience).*/
                Console.WriteLine("\nEnter the name of the item you want to sell: (Press enter to leave)");
                int ParsedChoise = 0;
                while (true)
                {
                    Choice = Console.ReadLine();
                    if (Item.ContainItem(Choice, player.GetItems()))
                    {
                        Item item = Item.GetItem(Choice, player.GetItems());
                        Console.WriteLine($"How much {item.GetName()} do you want to sell?");
                        while (true)
                        {
                            Choice = Console.ReadLine();
                            if (int.TryParse(Choice, out ParsedChoise) && ParsedChoise <= Item.GetNumberOfItem(item.GetName(), player.GetItems()))
                            {
                                for(int i = 0; i < ParsedChoise; i++)
                                    player.GetItems().RemoveAt(player.GetItems().IndexOf(Item.GetItem(item.GetName(), player.GetItems())));
                                player.SharpDollars += item.GetSellPrice()* ParsedChoise;
                                Console.Write($"You gained {item.GetSellPrice() * ParsedChoise} sharpdollars!\nFinishing transaction");
                                Loading(150, 10);
                                CheckEmptyInventory();
                                return;
                            }
                            else
                                Console.WriteLine($"You either don't have that number of {item.GetName()} or you did a wrong input.\nEnter a valid number of {item.GetName()} that you do have.");
                        }
                    }
                    else if (Choice == "")
                        return;
                    else
                        Console.WriteLine("Please enter a valid name.");
                }
            }
        }

        /// <summary>
        /// Method to simulate a loading screen to offer a better user experience.
        /// </summary>
        /// <param name="time"></param>
        /// <param name="numberOfPoint"></param>
        private static void Loading(int time, int numberOfPoint)
        {
            for (int i = 0; i < numberOfPoint; i++)
            {
                System.Threading.Thread.Sleep(time);
                Console.Write(".");
            }
        }

        /// <summary>
        /// Method used to Serialize (or save) the player's stats and objects into a json file.
        /// </summary>
        /// <param name="fileName"></param>
        private static void SerializeItem(string fileName)
        {
            File.WriteAllText(fileName, ByteConverter.GetBytes(JsonConvert.SerializeObject(player)).ToHex());
        }
        
        /// <summary>
        /// Method used to charge the data of the player from the json file.
        /// </summary>
        /// <param name="fileName"></param>
        private static void DeserializeItem(string fileName)
        {
            player = JsonConvert.DeserializeObject<Player>(ByteConverter.GetString((File.ReadAllText(fileName).ToByte())));
        }
        private static string ToHex(this byte[] bytes)
        {
            char[] c = new char[bytes.Length * 2];
            byte b;

            for(int bx = 0, cx = 0; bx < bytes.Length; ++bx, ++cx) 
            {
                b = ((byte)(bytes[bx] >> 4));
                c[cx] = (char)(b > 9 ? b + 0x37 + 0x20 : b + 0x30);

                b = ((byte)(bytes[bx] & 0x0F));
                c[++cx]=(char)(b > 9 ? b + 0x37 + 0x20 : b + 0x30);
            }

            return new string(c);
        }
        private static byte[] ToByte(this string str)
        {
            if (str.Length == 0 || str.Length % 2 != 0)
                return new byte[0];

            byte[] buffer = new byte[str.Length / 2];
            char c;
            for (int bx = 0, sx = 0; bx < buffer.Length; ++bx, ++sx)
            {
                // Convert first half of byte
                c = str[sx];
                buffer[bx] = (byte)((c > '9' ? (c > 'Z' ? (c - 'a' + 10) : (c - 'A' + 10)) : (c - '0')) << 4);

                // Convert second half of byte
                c = str[++sx];
                buffer[bx] |= (byte)(c > '9' ? (c > 'Z' ? (c - 'a' + 10) : (c - 'A' + 10)) : (c - '0'));
            }
            return buffer;
        }
    }
}
