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
        private static string savePath = "SharpmonSave.json"; //The path to your future (or existing) save file of the game. Note that this is MY own path, it may not work for you.

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
        public static void NewGame()
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
        public static void PrincipalScene()
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
                Console.WriteLine($"You're currently in {player.currentTown}.\nWhere do you want to go?\n\t0: Into the wild\n\t1: Fight for the {Sharpdex.Badges[Sharpdex.Towns.IndexOf(player.currentTown)]} badge\n\t2: In the shop\n" +
                                  "\t3: In the Sharpmon Center\n\t4: Save Game\n\t5: Exit Game");

                Choice = Console.ReadLine().ToLower();
                /*If the player enter 0 or "Into the wild" he's transported to the discovering Scene that will eventually lead him to a fight.
                  If he chooses 1 or "In the shop", he will be transported to the Shop so that he can buy or sell items.
                  If he chooses 2 or "In the sharpmon center", he will be transported to the sharpmon center so that he can heal his sharpmon or simply see all of them and their current Hp/Level.
                  If he chooses 3, "save" or "save game", the game will be saved. It means that all the player's informations will be Serialized into a .txt file.
                  If he chosses 4 or "exit game", the game will instantly be shut down.
                  If he enters none of the above input, he will be asked to re-enter an input in hope of matching the above inputs.*/
                switch (Choice)
                {
                    case "0":
                    case "into the wild":
                        if(player.GetCurrentSharpmon().IsAlive())
                            Discovering();
                        break;
                    case "1":
                    case "Fight the next arena":
                        if(player.GetCurrentSharpmon().IsAlive())
                            FightNextArena();
                        break;
                    case "2":
                    case "in the shop":
                        ShopScene();
                        break;
                    case "3":
                    case "in the sharpmon center":
                        SharpmonCenterScene();
                        break;
                    case "4":
                    case "save":
                    case "save game":
                        SerializeItem(savePath);
                        continue;
                    case "5":
                    case "exit game":
                        Environment.Exit(0);
                        return;
                    case "6":
                        player.GetCurrentSharpmon().SetExp(90000000);
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
        public static void Discovering()
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
            if (player.GetCurrentSharpmon().GetLevel() < 20)
                Ennemy = Sharpmon.CopySharpmon(Sharpmon.GetRandomSharpmon(Rng.Next(Sharpdex.AllSharpmons.Count - 18)));      //Allow the discovering of common Sharpmons.
            else if (player.GetCurrentSharpmon().GetLevel() >= 20 && player.GetCurrentSharpmon().GetLevel() < 40)
                Ennemy = Sharpmon.CopySharpmon(Sharpmon.GetRandomSharpmon(Rng.Next(Sharpdex.AllSharpmons.Count - 5)));       //Allow the discovering of a possible Advanced Sharpmon.
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
        public static void FightScene(Sharpmon ennemy, bool arenaFight = false)
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
                        FightSystem(player.GetCurrentSharpmon(), ennemy);
                        return;
                    case "1":
                    case "change sharpmon":
                        ChangeSystem(ennemy);
                        return;
                    case "2":
                    case "use item":
                        ItemSystem(ennemy);
                        return;
                    case "3":
                    case "capture":
                        if (!arenaFight)
                        {
                            Console.WriteLine($"You threw a Sharpball at {ennemy.Name}!");
                            CaptureSystem(player.GetCurrentSharpmon(), ennemy);
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
                        EscapeSystem(player.GetCurrentSharpmon(), ennemy);    
                        return;
                    default:
                        Console.WriteLine("Please enter a valid input.\nPress a key to continue the fight.");
                        Console.ReadKey();
                        continue;
                }
            }
        }

        public static void FightNextArena()
        {
            Console.Clear();
            Console.Write($"The gym leader accepted your challenge!\n\nLoading first fight.");
            Loading(150, 10);
            Sharpmon Ennemy = null;
            switch (player.currentTown)
            {
                case "Pewter City":
                    Ennemy = Sharpmon.CopySharpmon(Sharpmon.GetSharpmon("Sharponix", Sharpdex.AllSharpmons));
                    for (int i = 0; i < 13; i++)
                        Ennemy.OnLevelUp();
                    break;
                case "Cerulean City":
                    break;
                case "Vermillon City":
                    break;
                case "Celadon City":
                    break;
                case "Saffron City":
                    break;
                case "Fuschia City":
                    break;
                case "Cinnabar Island":
                    break;
                case "Viridian City":
                    break;
                case "the Indigo Plateau":
                    break;
                default:
                    break;
            }
            FightScene(Ennemy, true);
            player.currentTown = Sharpdex.Towns[Sharpdex.Towns.IndexOf(player.currentTown)+1];
        }
        /// <summary>
        /// Static method representing the fact that the player is currently in the shop.
        /// From there he can choose what to do next.
        /// </summary>
        public static void ShopScene()
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
        public static void SharpmonCenterScene()
        {
            Console.Clear();
            Console.WriteLine("Welcome in the Sharpmon Center! Do you want to heal all your sharpmons or exit the center?\n\t0: Heal all the sharpmons");
            foreach (Sharpmon sharpmon in player.GetSharpmonses())
                Console.WriteLine($"\t\t{sharpmon.Name} (Level {sharpmon.GetLevel()}): {sharpmon.CurrentHp}/{sharpmon.MaxHp}");
            Console.WriteLine("\n\t1: Exit center");
            while (true)
            {
                Choice = Console.ReadLine().ToLower();
                switch (Choice)
                {
                    case "0":
                    case "heal":
                    case "heal all the sharpmons":
                        foreach (Sharpmon sharpmon in player.GetSharpmonses())
                            sharpmon.CurrentHp = sharpmon.MaxHp;

                        Console.Clear();
                        Console.Write("All your Sharpmons are healed!\nYou're now exiting the center.\nExiting the center.");
                        Loading(200, 10);
                        return;
                    case "1":
                    case "exit center":
                        Console.Write("Exiting the center.");
                        Loading(100, 10);
                        return;
                    default:
                        Console.WriteLine("Please enter a valid input.");
                        continue;
                }
            }
        }

        /// <summary>
        /// Method to draw the sharpmon's stat table in the color of its element.
        /// </summary>
        /// <param name="entity"></param>
        public static void DrawTable(this Sharpmon entity)
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
        public static void FightSystem(Sharpmon currentSharpmon, Sharpmon ennemy)
        {
            while (true)
            {
                if (currentSharpmon.CurrentSpeed > ennemy.CurrentSpeed)
                {
                    if (currentSharpmon.IsAlive())
                        currentSharpmon.ChooseAttack().LaunchAttack(currentSharpmon, ennemy);
                    else
                    {
                        ChangeSystem(ennemy);
                        return;
                    }

                    if (ennemy.IsAlive())
                        ennemy.GetAttack(Rng.Next(ennemy.GetAttacks().Count)).LaunchAttack(ennemy, currentSharpmon);
                    else
                    {
                        currentSharpmon.Win(ennemy);
                        return;
                    }
                    if (!currentSharpmon.IsAlive())
                    {
                        ChangeSystem(ennemy);
                        return;
                    }

                    EndTurn(ennemy);
                    return;
                }
                else if (currentSharpmon.CurrentSpeed < ennemy.CurrentSpeed)
                {
                    Attack tempAttack = currentSharpmon.ChooseAttack();
                    if (ennemy.IsAlive())
                        ennemy.GetAttack(Rng.Next(ennemy.GetAttacks().Count)).LaunchAttack(ennemy, currentSharpmon);
                    else
                    {
                        currentSharpmon.Win(ennemy);
                        return;
                    }

                    if (currentSharpmon.IsAlive())
                        tempAttack.LaunchAttack(currentSharpmon, ennemy);
                    else
                    {
                        ChangeSystem(ennemy);
                        return;
                    }
                    if(!ennemy.IsAlive())
                    {
                        currentSharpmon.Win(ennemy);
                        return;
                    }

                    EndTurn(ennemy);
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
                            currentSharpmon.Win(ennemy);
                            return;
                        }

                        if (currentSharpmon.IsAlive())
                            tempAttack.LaunchAttack(currentSharpmon, ennemy);
                        else
                        {
                            ChangeSystem(ennemy);
                            return;
                        }
                        if (!ennemy.IsAlive())
                        {
                            currentSharpmon.Win(ennemy);
                            return;
                        }

                        EndTurn(ennemy);
                        return;
                    }
                    else
                    {
                        if (currentSharpmon.IsAlive())
                            tempAttack.LaunchAttack(currentSharpmon, ennemy);
                        else
                        {
                            ChangeSystem(ennemy);
                            return;
                        }

                        if (ennemy.IsAlive())
                            ennemy.GetAttack(Rng.Next(ennemy.GetAttacks().Count)).LaunchAttack(ennemy, currentSharpmon);
                        else
                        {
                            currentSharpmon.Win(ennemy);
                            return;
                        }
                        if (!currentSharpmon.IsAlive())
                        {
                            ChangeSystem(ennemy);
                            return;
                        }

                        EndTurn(ennemy);
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
        public static Attack ChooseAttack(this Sharpmon currentSharpmon)
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
        public static void EndTurn(Sharpmon ennemy)
        {
            Console.WriteLine("Both Sharpmon attacked, press any key to continue the fight.");
            Console.ReadKey();
            FightScene(ennemy);
            return;
        }
        /// <summary>
        /// Method that displays to the player how much experience and sharpdollars his current sharpmon
        /// and himself won with this fight. It sign the end of the fight and allow the user to return to
        /// the principal scene.
        /// </summary>
        /// <param name="currentSharpmon"></param>
        /// <param name="ennemy"></param>
        public static void Win(this Sharpmon currentSharpmon, Sharpmon ennemy)
        {
            Console.WriteLine($"{ennemy.Name} fainted.\nPress any key to continue.");
            Console.ReadKey();
            Console.Clear();
            int temp = Rng.Next(50, 70) * ennemy.GetLevel();
            currentSharpmon.CurrentExperience += temp;

            Console.WriteLine($"Your {currentSharpmon.Name} won {temp} experience points!");

            temp = Rng.Next(100, 501);
            player.SharpDollars += temp;

            Console.Write($"You won {temp} SharpDollars!\nHeading back to town");
            Loading(150, 15);
            currentSharpmon.CheckForLevelUp();
            CheckEmptyInventory();
            return;
        }
        /// <summary>
        /// Method that changes (or not) the current sharpmon to another one in the player's sharpmon list.
        /// </summary>
        /// <param name="ennemy"></param>
        public static void ChangeSystem(Sharpmon ennemy)
        {
            
            Console.WriteLine("You current Sharpmon fainted!");
            string choice;
            int ParsedChoice;
            while (true)
            {
                Console.WriteLine("Choose a Sharpmon:");
                for (int i = 0; i < player.GetSharpmonses().Count; i++)
                    Console.WriteLine($"\t\t{i}: {player.GetSharpmonses()[i].Name} (Hp: {player.GetSharpmonses()[i].CurrentHp}/{player.GetSharpmonses()[i].MaxHp})");
                Console.WriteLine($"\n\t{player.GetSharpmonses().Count}: Continue the fight with current the Sharpmon\n\t{player.GetSharpmonses().Count + 1}: Leave the fight");
                choice = Console.ReadLine();
                if (int.TryParse(choice, out ParsedChoice) && ParsedChoice > 0 && ParsedChoice < player.GetSharpmonses().Count)
                {
                    if (player.GetSharpmonses()[ParsedChoice].IsAlive())
                    {
                        Sharpmon TemporarySharpmon = player.GetCurrentSharpmon();
                        player.GetSharpmonses()[0] = player.GetSharpmonses()[ParsedChoice];
                        player.GetSharpmonses()[ParsedChoice] = TemporarySharpmon;
                        Console.WriteLine($"I choose you {player.GetCurrentSharpmon().Name}!");
                        Loading(350, 3);
                        FightScene(ennemy);
                        return;
                    }
                    else
                    {
                        Console.WriteLine("You can't choose this sharpmon because he's KO.");
                        continue;
                    }

                }
                else if (int.TryParse(choice, out ParsedChoice) &&(ParsedChoice == player.GetSharpmonses().Count || ParsedChoice == 0))
                {
                    if (!player.GetCurrentSharpmon().IsAlive())
                    {
                        Console.Write("Your current Sharpmon is KO.");
                        continue;
                    }
                    else
                    {
                        FightScene(ennemy);
                        return;
                    }
                }
                else if (int.TryParse(choice, out ParsedChoice) && ParsedChoice == player.GetSharpmonses().Count + 1)
                {
                    Console.Write("You fled the fight.\nHeading back to town");
                    Loading(150,10);
                    return;
                }
                else
                    Console.WriteLine("Please enter a valid input.");
            }
            
        }
        /// <summary>
        /// Method that allows the use of items in fight.
        /// </summary>
        /// <param name="ennemy"></param>
        public static void ItemSystem(Sharpmon ennemy)
        {
            if (player.GetItems().Count == 0)
            {
                Console.WriteLine("Your inventory is empty.\nPress any key to return to the fight.");
                Console.ReadKey();
                FightScene(ennemy);
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
                        foreach (Sharpmon sharpmon in player.GetSharpmonses())
                        {
                            Console.WriteLine($"\t{hiddenCount}: {sharpmon.Name} (Level {sharpmon.GetLevel()}): {sharpmon.CurrentHp} /{sharpmon.MaxHp}");
                            hiddenCount++;
                        }
                        while (true)
                        {
                            tempChoice = Console.ReadLine();
                            if (int.TryParse(tempChoice, out ParsedChoice) && (ParsedChoice >= 0 && ParsedChoice < player.GetSharpmonses().Count))
                            {
                                /*Use the selected item on the selected sharpmon (if the usage is needed in case of potions).
                                  The item is destroyed after the usage.*/
                                if(player.GetItems()[player.GetItems().IndexOf(Item.GetItem(choice, player.GetItems()))].Use(player.GetSharpmonses()[ParsedChoice], ennemy))
                                {
                                    player.GetItems().RemoveAt(player.GetItems().IndexOf(Item.GetItem(choice, player.GetItems())));

                                    /*The ennemy always execute an attack after the usage of an item.*/
                                    ennemy.GetAttack(Rng.Next(2)).LaunchAttack(ennemy, player.GetCurrentSharpmon());
                                    if (!player.GetCurrentSharpmon().IsAlive())
                                    {
                                        ChangeSystem(ennemy);
                                        return;
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nPress any key to continue.");
                                        Console.ReadKey();
                                        FightScene(ennemy);
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
                        FightScene(ennemy);
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
        public static void CaptureSystem(Sharpmon CurrentSharpmon, Sharpmon ennemy)
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
        public static void EscapeSystem(Sharpmon CurrentSharpmon, Sharpmon ennemy)
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
        public static void Inventory()
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
        public static void CheckEmptyInventory()
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
        public static void BuyShop()
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
        public static void SellShop()
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
        public static void Loading(int time, int numberOfPoint)
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
        public static void SerializeItem(string fileName)
        {
            using (StreamWriter file = File.CreateText(savePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, player);
            }
        }
        
        /// <summary>
        /// Method used to charge the data of the player from the json file.
        /// </summary>
        /// <param name="fileName"></param>
        public static void DeserializeItem(string fileName)
        {
            using (StreamReader file = File.OpenText(savePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                player = (Player)serializer.Deserialize(file, typeof(Player));
            }
        }
    }
}
