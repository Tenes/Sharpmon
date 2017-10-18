using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sharpmon_213979
{
    [Serializable]                                  //Indicate that this class thus object is serializable (used for saving the game)
    public class Item : Effect, ISerializable       //The Item class inherit from the Effect class and also from the ISerializable Interface (so do all the other classes).
    {
        //FIELDS
        private int HealAmount;         //Variable used to know the healing effectiveness of the object.
        private int LevelGiven;         //Variable used to know the level empowerement of the object.
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
        
        //SPECIAL CONSTRUCTOR FOR DESERIALIZED VALUES
        /// <summary>
        /// Constructor only used when a save of the game is loaded in order to recreate all the exact same objects.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public Item(SerializationInfo info, StreamingContext context)
        {
            this.Name = (string)info.GetValue("Name", typeof(string));
            this.Price = (int)info.GetValue("Price", typeof(int));
            this.SellPrice = this.Price / 2;
            this.Description = (string)info.GetValue("Description", typeof(string));
            this.Power = (int)info.GetValue("Power", typeof(int));
            this.Defense = (int)info.GetValue("Defense", typeof(int));
            this.Dodge = (int)info.GetValue("Dodge", typeof(int));
            this.Accucary = (int)info.GetValue("Accucary", typeof(int));
            this.Speed = (int)info.GetValue("Speed", typeof(int));
            this.HealAmount = (int)info.GetValue("HealAmount", typeof(int));
            this.LevelGiven = (int)info.GetValue("LevelGiven", typeof(int));
            this.MaxHpBoost = (int)info.GetValue("MaxHpBoost", typeof(int));
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

        /// <summary>
        /// Method for saving all the data of the instance of an attack type object
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", this.Name, typeof(string));
            info.AddValue("Price", this.Price, typeof(int));
            info.AddValue("Description", this.Description, typeof(string));
            info.AddValue("Power", this.Power, typeof(int));
            info.AddValue("Defense", this.Defense, typeof(int));
            info.AddValue("Dodge", this.Dodge, typeof(int));
            info.AddValue("Accucary", this.Accucary, typeof(int));
            info.AddValue("Speed", this.Speed, typeof(int));
            info.AddValue("HealAmount", this.HealAmount, typeof(int));
            info.AddValue("LevelGiven", this.LevelGiven, typeof(int));
            info.AddValue("MaxHpBoost", this.MaxHpBoost, typeof(int));
        }
    }
}
