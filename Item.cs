using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Sharpmon
{                     
    [Serializable]            //Indicate that this class thus object is serializable (used for saving the game)
    public class Item : Effect       //The Item class inherit from the Effect class and also from the ISerializable Interface (so do all the other classes).
    {
        //FIELDS
        [JsonProperty]
        private int HealAmount;         //Variable used to know the healing effectiveness of the object.
        [JsonProperty]
        private int LevelGiven;         //Variable used to know the level empowerement of the object.
        [JsonProperty]
        private int MaxHpBoost;         //Variabe used to know the MaxHp's stat reinforcement of the target.

        //PROPERTIES
        public int GetPrice()
        {
            return this.Price;
        }
        public int GetSellPrice()
        {
            return this.SellPrice;
        }
        public string GetDescription()
        {
            return this.Description;
        }

        //CONSTRUCTOR
        [JsonConstructor]
        public Item(string name, int price, string description, int healAmount, int power = 0, int defense = 0, int dodge = 0, int accucary = 0, int speed = 0, int levelGiven = 0, int maxHpBoost = 0)
        {
            this.Name = name;
            this.Price = price;
            this.SellPrice = this.Price/2;
            this.Description = description;
            this.HealAmount = healAmount;
            this.Power = power;
            this.Defense = defense;
            this.Dodge = dodge;
            this.Accucary = accucary;
            this.Speed = speed;
            this.LevelGiven = levelGiven;
            this.MaxHpBoost = maxHpBoost;
        }

        //METHODS
        /// <summary>
        /// Method to say that the current item is used on a said sharpmon.
        /// If it does someting, it returns true (it returns false when a potion is used a full life sharpmon).
        /// </summary>
        /// <param name="target"></param>
        /// <param name="ennemy"></param>
        public bool Use(Sharpmon target, Sharpmon ennemy)
        {
            if (this.HealAmount > 0 && target.CurrentHp == target.MaxHp)
            {
                Console.WriteLine("This Sharpmon is already full life. Use another item:");
                return false;
            }
            target.CurrentHp += this.HealAmount;
            if(target.CurrentHp > target.MaxHp)
                target.CurrentHp = target.MaxHp;
            target.BasePower += this.Power;
            target.BaseDefense += this.Defense;
            target.BaseDodge += this.Dodge;
            target.BaseAccucary += this.Accucary;
            target.BaseSpeed += this.Speed;
            target.MaxHp += this.MaxHpBoost;
            target.CurrentHp += this.MaxHpBoost;
            if (this.LevelGiven > 0)
                target.OnLevelUp();
            return true;
        }

        /// <summary>
        /// Method using LINQ to get an attack by its name.
        /// </summary>
        /// <param name="Name"></param>  
        /// <param name="items"></param>
        /// <returns></returns>
        public static Item GetItem(string Name, List<Item> items)
        {
            return items.FirstOrDefault(item => item.GetName() == Name);
        }

        /// <summary>
        /// Method to get how much of a specific item is in a list 
        /// (used for example to know how much potions are the inventory for a better user experience).
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static int GetNumberOfItem(string Name, List<Item> items)
        {
            return items.Count(item => item.Name == Name);
        }

        /// <summary>
        /// Method which return a true if a given list contains a specific item given by its name.
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool ContainItem(string Name, List<Item> items)
        {
            return items.Contains(GetItem(Name, items));
        }
    }
}
