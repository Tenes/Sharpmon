using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Sharpmon
{                         
    [Serializable]     //Indicate that this class thus object is serializable (used for saving the game)
    public class Player             //The Item class inherit from the Effect class and also from the ISerializable Interface (so do all the other classes).
    {
        //FIELDS
        [JsonProperty]
        public string Name { get; set; }            //Props used to Get/Set the name variable of a player.
        [JsonProperty]
        public int SharpDollars { get; set; }       //Props used to Get/Set the sharpDollars of a player.
        [JsonProperty]
        private List<Sharpmon> SharpmonsList;       //The list that stocks all the player's sharpmons.
        [JsonProperty]
        private List<Sharpmon> PCList;       //The list that stocks all the player's sharpmons in the pc.
        [JsonProperty]
        private List<Item> ItemsList;               //The list that stocks all the player's items.
        [JsonProperty]
        public string currentTown { get; set; }

        //PROPERTIES
        /// <summary>
        /// Getter to return the entire list of sharpmons.
        /// </summary>
        /// <returns></returns>
        public List<Sharpmon> GetSharpmons()
        {
            return this.SharpmonsList;
        }
        
        /// <summary>
        /// Getter to return the entire list of sharpmons in the PC.
        /// </summary>
        /// <returns></returns>
        public List<Sharpmon> GetSharpmonsInPC()
        {
            return this.PCList;
        }

        /// <summary>
        /// Getter to return the entire list of sharpmons.
        /// </summary>
        /// <returns></returns>
        public void AddSharpmonInPC(Sharpmon sharpmon)
        {
            this.PCList.Add(sharpmon);
        }
        /// <summary>
        /// Getter to return the entier list of items.
        /// </summary>
        /// <returns></returns>
        public List<Item> GetItems()
        {
            return this.ItemsList;
        }
        
        /// <summary>
        /// Getter to return the first sharpmon of the list only.
        /// </summary>
        /// <returns></returns>
        public Sharpmon GetCurrentSharpmon()
        {
            return this.SharpmonsList[0];
        }

        //CONSTRUCTOR
        public Player(string name)
        {
            this.Name = name;
            this.SharpDollars = 20000;
            this.SharpmonsList = new List<Sharpmon>();
            this.PCList = new List<Sharpmon>();
            this.ItemsList = new List<Item>();
            this.currentTown = Sharpdex.Towns[0];
            GetFirstSharpmon();
        }
        //SPECIAL CONSTRUCTOR FOR DESERIALIZED VALUES
        /// <summary>
        /// Constructor only used when a save of the game is loaded in order to recreate all the exact same objects.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [JsonConstructor]
        public Player(string name, int sharpDollars, List<Sharpmon> sharpmonsList, List<Sharpmon> pcList, List<Item> itemsList, string currentTown)
        {
            this.Name = name;
            this.SharpDollars = sharpDollars;
            this.SharpmonsList = sharpmonsList;
            this.PCList = pcList;
            this.ItemsList = itemsList;
            this.currentTown = currentTown;
        }

        //METHODS
        /// <summary>
        /// Method that adds a full copy of a sharpmon to the player's list.
        /// </summary>
        /// <param name="entity"></param>
        public void AddSharpmon(Sharpmon entity)
        {
            this.SharpmonsList.Add(Sharpmon.CopySharpmon(entity));
        }
        
        /// <summary>
        /// Method only used once in the constructor for the player to select
        /// his first sharpmons
        /// </summary>
        private void GetFirstSharpmon()
        {
            /*All of the code below has the purpose to offer the player a better
                user experience. When the user press 1 or 2, another starter is displayed
                to the player and so on. If he presses 0, he select the current sharpmon
                displaying.*/
            Console.Clear();
            Console.WriteLine($"Alright {this.Name}, allow me to introduce myself.\nI'm the professor Shoap, an expert of the Sharpworld.\n" +
                              "Before exploring this vast and glorious world, choose a Sharpmon among those:\n");
            ConsoleKeyInfo choice;
            int hiddenScroll = 0;        //Hidden counter used for the various check present in this method.
            string dynamicText;
            Selector hiddenSelector = new Selector();
            this.DrawStarters(hiddenScroll);
            dynamicText = "\t\t\tChoose this Sharpmon";
            hiddenSelector.SetText(dynamicText);
            while (true)
            {
                choice = Console.ReadKey(true);
                hiddenSelector.PlaceAtLastPosition();
                if(choice.Key == ConsoleKey.Enter)
                {
                    this.AddSharpmon(Sharpmon.GetSharpmon(Sharpdex.AllSharpmons[hiddenScroll].Name, Sharpdex.AllSharpmons));       
                    break;
                }
                else if(hiddenSelector.ModifyHiddenScroll(choice.Key, 4, out hiddenScroll))
                {
                    Console.Clear();
                    this.DrawStarters(hiddenScroll);
                    hiddenSelector.FirstDraw();
                }
            }
        }
        private void DrawStarters(int hiddenScroll)
        {
            Sharpdex.AllSharpmons[hiddenScroll].GetColorElementalType();
            Console.WriteLine($"\t\t _______________________________________\n" +
                            $"\t\t|\t\t\t\t\t|\n" +
                            $"\t\t|\t {Sharpdex.AllSharpmons[hiddenScroll].Name}\tHp: {Sharpdex.AllSharpmons[hiddenScroll].MaxHp} \t\t|\n" +
                            $"\t\t|\t Attacks:\t{Sharpdex.AllSharpmons[hiddenScroll].GetAttack(0).GetName()}   \t|\n" +
                            $"\t\t|\t\t\t{Sharpdex.AllSharpmons[hiddenScroll].GetAttack(1).GetName()}    \t|\n" +
                            $"\t\t|_______________________________________|\n");
            Console.ForegroundColor = ConsoleColor.Gray;

        }
        /// <summary>
        /// Add a specific item to the player's list of items.
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(Item item)
        {
            this.ItemsList.Add(item);
        }
    }
}
