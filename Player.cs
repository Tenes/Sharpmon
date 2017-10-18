using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;

namespace Sharpmon_213979
{
    [Serializable]                                  //Indicate that this class thus object is serializable (used for saving the game)
    public class Player : ISerializable             //The Item class inherit from the Effect class and also from the ISerializable Interface (so do all the other classes).
    {
        //FIELDS
        public string Name { get; set; }            //Props used to Get/Set the name variable of a player.
        public int SharpDollars { get; set; }       //Props used to Get/Set the sharpDollars of a player.
        private List<Sharpmon> SharpmonsList;       //The list that stocks all the player's sharpmons.
        private List<Item> ItemsList;               //The list that stocks all the player's items.
        public string currentTown { get; set; }

        //Enum containing all the arenas.

        //PROPERTIES
        /// <summary>
        /// Getter to return the entire list of sharpmons.
        /// </summary>
        /// <returns></returns>
        public List<Sharpmon> GetSharpmonses()
        {
            return this.SharpmonsList;
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
            this.ItemsList = new List<Item>();
            this.currentTown = GameInstance.Towns[0];
            GetFirstSharpmon();
        }
        
        //SPECIAL CONSTRUCTOR FOR DESERIALIZED VALUES
        /// <summary>
        /// Constructor only used when a save of the game is loaded in order to recreate all the exact same objects.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public Player(SerializationInfo info, StreamingContext context)
        {
            this.Name = (string)info.GetValue("Name", typeof(string));
            this.SharpDollars = info.GetInt32("SharpDollars");
            this.SharpmonsList = (List<Sharpmon>)info.GetValue("SharpmonsList", typeof(List<Sharpmon>));
            this.ItemsList = (List<Item>)info.GetValue("ItemsList", typeof(List<Item>));
            this.currentTown = (string)info.GetValue("currentTown", typeof(string));
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
            Console.Clear();
            Console.WriteLine($"Alright {this.Name}, allow me to introduce myself.\nI'm the professor Shoap, an expert of the Sharpworld.\n" +
                              "Before exploring this vast and glorious world, choose a Sharpmon among those:\n");
            string choice;
            int hiddenCount = 0;        //Hidden counter used for the various check present in this method.
            while (true)
            {
                /*All of the below code has the purpose to offer the player a better
                  user experience. When the user press 1 or 2, another starter is displayed
                  to the player and so on. If he presses 0, he select the current sharpmon
                  displaying.*/
                GameInstance.AllSharpmons[hiddenCount].GetColorElementalType();
                Console.WriteLine($"\t\t _______________________________________\n" +
                              $"\t\t|\t\t\t\t\t|\n" +
                              $"\t\t|\t {GameInstance.AllSharpmons[hiddenCount].Name}\tHp: {GameInstance.AllSharpmons[hiddenCount].MaxHp} \t\t|\n" +
                              $"\t\t|\t Attacks:\t{GameInstance.AllSharpmons[hiddenCount].GetAttack(0).GetName()}   \t|\n" +
                              $"\t\t|\t\t\t{GameInstance.AllSharpmons[hiddenCount].GetAttack(1).GetName()}    \t|\n" +
                              $"\t\t|_______________________________________|\n");
                Console.ForegroundColor = ConsoleColor.Gray;

                Console.WriteLine("\t\t\t0: Choose this Sharpmon");
                if(hiddenCount > 0)
                    Console.WriteLine("\t\t\t1: Return on the previous Sharpmon");
                if (hiddenCount < 3)
                    Console.WriteLine("\t\t\t2: View the next Sharpmon");

                choice = Console.ReadLine();
                /*Add the displayed sharpmon to the player's list of sharpmons.*/
                if (choice == "0")
                {
                    this.AddSharpmon(Sharpmon.GetSharpmon(GameInstance.AllSharpmons[hiddenCount].Name, GameInstance.AllSharpmons));
                    return;
                }
                /*Display the last displayed starter to the player only if it's not the first starter.*/
                else if (choice == "1" && hiddenCount > 0)
                {
                    Console.Clear();
                    hiddenCount--;
                }
                /*Display the next starter to the player only if it's not the last starter.*/
                else if (choice == "2" && hiddenCount < 3)
                {
                    Console.Clear();
                    hiddenCount++;
                }
                /*If the user enter a different input than the 3 available,
                  it loops back and ask another input.*/
                else
                {
                    Console.Clear();
                    Console.WriteLine("Enter a valid input.");
                }
            }
        }
        
        /// <summary>
        /// Add a specific item to the player's list of items.
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(Item item)
        {
            this.ItemsList.Add(item);
        }
        
        /// <summary>
        /// Method for saving all the data of the instance of an attack type object
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", this.Name, typeof(string));
            info.AddValue("SharpDollars", this.SharpDollars, typeof(int));
            info.AddValue("SharpmonsList", this.SharpmonsList, typeof(List<Sharpmon>));
            info.AddValue("ItemsList", this.ItemsList, typeof(List<Item>));
            info.AddValue("currentTown", this.currentTown, typeof(string));
        }
    }
}
