using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sharpmon_213979
{
    [Serializable]                                              //Indicate that this class thus object is serializable (used for saving the game)
    public class Sharpmon : ISerializable                       //The Item class inherit from the Effect class and also from the ISerializable Interface (so do all the other classes).
    {
        //FIELDS
        public string Name { get; set; }
        private int Level;
        public int MaxExperience { get; set; }
        public int CurrentExperience { get; set; }
        public int MaxHp { get; set; }
        public int CurrentHp { get; set; }
        public int BasePower { get; set; }
        public int CurrentPower { get; set; }
        public int BaseDefense { get; set; }
        public int CurrentDefense { get; set; }
        public int BaseDodge { get; set; }
        public int CurrentDodge { get; set; }
        public int BaseAccucary { get; set; }
        public int CurrentAccucary { get; set; }
        public int BaseSpeed { get; set; }
        public int CurrentSpeed { get; set; }
        public enum ElementalType { FIRE, GROUND, WATER, GRASS, ELECTRIC, NORMAL, FLYING, BUG, FIGHTING, PSYCHIC }
        public ElementalType SharpmonType { get; set; }
        private List<Attack> AttacksList;

        //PROPERTIES
        public int GetLevel()
        {
            return this.Level;
        }
        public Attack GetAttack(int Attackindex)
        {
            return this.AttacksList[Attackindex];
        }
        public List<Attack> GetAttacks()
        {
            return this.AttacksList;
        }
        public string GetElementalType()
        {
            if (this.SharpmonType == ElementalType.BUG) return "BUG";
            else if (this.SharpmonType == ElementalType.FLYING) return "FLYING";
            else if (this.SharpmonType == ElementalType.FIRE) return "FIRE";
            else if (this.SharpmonType == ElementalType.WATER) return "WATER";
            else if (this.SharpmonType == ElementalType.GRASS) return "GRASS";
            else if (this.SharpmonType == ElementalType.GROUND) return "GROUND";
            else if (this.SharpmonType == ElementalType.ELECTRIC) return "ELECTRIC";
            else if (this.SharpmonType == ElementalType.FIGHTING) return "FIGHTING";
            else if (this.SharpmonType == ElementalType.PSYCHIC) return "PSYCHIC";
            else return "NORMAL";
        }
        public void GetColorElementalType()
        {
            if (this.SharpmonType == ElementalType.FIRE) Console.ForegroundColor = ConsoleColor.Red;
            else if (this.SharpmonType == ElementalType.WATER) Console.ForegroundColor = ConsoleColor.Cyan;
            else if (this.SharpmonType == ElementalType.GRASS) Console.ForegroundColor = ConsoleColor.Green;
            else if (this.SharpmonType == ElementalType.BUG) Console.ForegroundColor = ConsoleColor.DarkGreen;
            else if (this.SharpmonType == ElementalType.FLYING) Console.ForegroundColor = ConsoleColor.DarkCyan;
            else if (this.SharpmonType == ElementalType.GROUND) Console.ForegroundColor = ConsoleColor.DarkYellow;
            else if (this.SharpmonType == ElementalType.ELECTRIC) Console.ForegroundColor = ConsoleColor.Yellow;
            else if (this.SharpmonType == ElementalType.FIGHTING) Console.ForegroundColor = ConsoleColor.DarkRed;
            else if (this.SharpmonType == ElementalType.PSYCHIC) Console.ForegroundColor = ConsoleColor.Magenta;
            else Console.ForegroundColor = ConsoleColor.Gray;
        }

        //CONSTRUCTOR
        /// <summary>
        /// Constructor to create the base of every sharpmon.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="sharpmonType"></param>
        /// <param name="maxHP"></param>
        /// <param name="currentHp"></param>
        /// <param name="power"></param>
        /// <param name="defense"></param>
        /// <param name="dodge"></param>
        /// <param name="accucary"></param>
        /// <param name="speed"></param>
        /// <param name="primaryAttack"></param>
        /// <param name="secondaryAttack"></param>
        public Sharpmon(string name, ElementalType sharpmonType, int maxHP, int currentHp, int power, int defense, int dodge, int accucary, int speed,
            Attack primaryAttack, Attack secondaryAttack)
        {
            this.Name = name;
            this.SharpmonType = sharpmonType;
            this.Level = 1;
            this.MaxExperience = 100;
            this.CurrentExperience = 0;
            this.MaxHp = maxHP;
            this.CurrentHp = currentHp;
            this.BasePower = power;
            this.CurrentPower = this.BasePower;
            this.BaseDefense = defense;
            this.CurrentDefense = this.BaseDefense;
            this.BaseDodge = dodge;
            this.CurrentDodge = this.BaseDodge;
            this.BaseAccucary = accucary;
            this.CurrentAccucary = this.BaseAccucary;
            this.BaseSpeed = speed;
            this.CurrentSpeed = this.BaseSpeed;
            this.AttacksList = new List<Attack>();
            this.AttacksList.Add(primaryAttack);
            this.AttacksList.Add(secondaryAttack);
        }
        /// <summary>
        /// Constructor used when a full copy of a Sharpmon is made (mostly for the capture system).
        /// </summary>
        /// <param name="name"></param>
        /// <param name="sharpmonType"></param>
        /// <param name="maxHP"></param>
        /// <param name="currentHp"></param>
        /// <param name="power"></param>
        /// <param name="defense"></param>
        /// <param name="dodge"></param>
        /// <param name="accucary"></param>
        /// <param name="speed"></param>
        /// <param name="attackList"></param>
        /// <param name="level"></param>
        /// <param name="maxExperience"></param>
        public Sharpmon(string name, ElementalType sharpmonType, int maxHP, int currentHp, int power, int defense, int dodge, int accucary, int speed,
            List<Attack> attackList , int level, int maxExperience)
        {
            this.Name = name;
            this.SharpmonType = sharpmonType;
            this.Level = level;
            this.MaxExperience = maxExperience;
            this.CurrentExperience = 0;
            this.MaxHp = maxHP;
            this.CurrentHp = currentHp;
            this.BasePower = power;
            this.CurrentPower = this.BasePower;
            this.BaseDefense = defense;
            this.CurrentDefense = this.BaseDefense;
            this.BaseDodge = dodge;
            this.CurrentDodge = this.BaseDodge;
            this.BaseAccucary = accucary;
            this.CurrentAccucary = this.BaseAccucary;
            this.BaseSpeed = speed;
            this.CurrentSpeed = this.BaseSpeed;
            this.AttacksList = attackList;
        }

        //SPECIAL CONSTRUCTOR FOR DESERIALIZED VALUES
        public Sharpmon(SerializationInfo info, StreamingContext context)
        {
            this.Name = (string)info.GetValue("Name", typeof(string));
            this.SharpmonType = (ElementalType) info.GetValue("SharpmonType", typeof(ElementalType));
            this.Level = (int)info.GetValue("Level", typeof(int));
            this.MaxExperience = (int)info.GetValue("MaxExperience", typeof(int));
            this.CurrentExperience = (int)info.GetValue("CurrentExperience", typeof(int));
            this.MaxHp = (int)info.GetValue("MaxHp", typeof(int));
            this.CurrentHp = (int)info.GetValue("CurrentHp", typeof(int));
            this.BasePower = (int)info.GetValue("BasePower", typeof(int));
            this.CurrentPower = this.BasePower;
            this.BaseDefense = (int)info.GetValue("BaseDefense", typeof(int));
            this.CurrentDefense = this.BaseDefense;
            this.BaseDodge = (int)info.GetValue("BaseDodge", typeof(int));
            this.CurrentDodge = this.BaseDodge;
            this.BaseAccucary = (int)info.GetValue("BaseAccucary", typeof(int));
            this.CurrentAccucary = this.BaseAccucary;
            this.BaseSpeed = (int)info.GetValue("BaseSpeed", typeof(int));
            this.CurrentSpeed = this.BaseSpeed;
            this.AttacksList = (List<Attack>)info.GetValue("AttacksList", typeof(List<Attack>));
        }

        //METHODS
        /// <summary>
        /// Method which does a full copy of the sharpmon current 
        /// state passed as the parameter.
        /// </summary>
        /// <param name="WantedSharpmon"></param>
        /// <returns></returns>
        public static Sharpmon CopySharpmon(Sharpmon WantedSharpmon)
        {
            return new Sharpmon(WantedSharpmon.Name, WantedSharpmon.SharpmonType, WantedSharpmon.MaxHp, WantedSharpmon.CurrentHp, WantedSharpmon.BasePower,
                WantedSharpmon.BaseDefense, WantedSharpmon.BaseDodge, WantedSharpmon.BaseAccucary,
                WantedSharpmon.BaseSpeed, WantedSharpmon.AttacksList, WantedSharpmon.Level, WantedSharpmon.MaxExperience);
        }

        /// <summary>
        /// Method that checks if the current experience of the sharpmon is above
        /// the necessary experience to level up. If so, it calls the OnLevelUp()
        /// method until the current experience is lower than the necessary experience
        /// to level up.
        /// </summary>
        public void CheckForLevelUp()
        {
            Console.Clear();
            int count = 0;
            while (true)
            {
                /*If the sharpmon is level 40, the level up system stop acting, and that is because
                  at a certain point, with that stat growth rate, each stat of the sharpmon get
                  tremendously high, so high that it breaks the limit of the int storage meaning
                  that the stats become negative since if a int or anything else is overloadded,
                  the overload makes the number start from the min number possible with the int.*/
                if (this.Level == 40)
                    return;
                if (this.CurrentExperience >= this.MaxExperience)
                {
                    this.CurrentExperience -= this.MaxExperience;
                    this.OnLevelUp();
                    count++;
                }
                else
                {
                    if (count > 0)
                    {
                        Console.Write($"Your {this.Name} gained {count} level !\nHeading back to town.");
                        for (int i = 0; i < 10; i++)
                        {
                            Thread.Sleep(200);
                            Console.Write(".");
                        }
                    }
                    return;
                }
            }
        }

        /// <summary>
        /// Add 1 to the level of the Sharpmon, reset the experience and increases its stats.
        /// </summary>
        public void OnLevelUp()
        {
            this.Level += 1;
            this.MaxExperience = LevelUpSystem(this.MaxExperience * 1.3);

            this.MaxHp = LevelUpSystem(this.MaxHp * 1.3);
            this.CurrentHp = this.MaxHp;

            this.BasePower = LevelUpSystem(this.BasePower * 1.3);
            this.CurrentPower = this.BasePower;

            this.BaseDefense = LevelUpSystem(this.BaseDefense * 1.3);
            this.CurrentDefense = this.BaseDefense;

            this.BaseDodge = LevelUpSystem(this.BaseDodge * 1.3);
            this.CurrentDodge = this.BaseDodge;

            this.BaseAccucary = LevelUpSystem(this.BaseAccucary * 1.3);
            this.CurrentAccucary = this.BaseAccucary;

            this.BaseSpeed = LevelUpSystem(this.BaseSpeed * 1.3);
            this.CurrentSpeed = this.BaseSpeed;

            if (this.Level == 10)
            {
                IEnumerable<Attack> randomAttack = from attack in GameInstance.AllAttacks
                                                   where (attack.GetElementalType() == this.GetElementalType() || attack.GetElementalType() == "NORMAL") && 
                                                   attack.GetName() != this.GetAttack(0).GetName() && attack.GetName() != this.GetAttack(1).GetName()
                                                   select attack;
                this.AttacksList.Add(randomAttack.ToList()[GameInstance.Rng.Next(0, randomAttack.Count())]);
            }
            if (this.Level == 30)
            {
                IEnumerable<Attack> randomAttack = from attack in GameInstance.AllAttacks
                                                   where (attack.GetElementalType() == this.GetElementalType() || attack.GetElementalType() == "NORMAL") &&
                                                   attack.GetName() != this.GetAttack(0).GetName() && attack.GetName() != this.GetAttack(1).GetName() &&
                                                   attack.GetName() != this.GetAttack(2).GetName()
                                                   select attack;
                this.AttacksList.Add(randomAttack.ToList()[GameInstance.Rng.Next(0, randomAttack.Count())]);
            }
        }

        /// <summary>
        /// Method to check if the Stat is a float after the level up. 
        /// If it is, does a 50/50 to know if the stat gets the float ceiling
        /// or the float floor of the temporary value.
        /// </summary>
        /// <param name="tempStat"></param>
        /// <returns></returns>
        public int LevelUpSystem(double tempStat)
        {
            int tempRng;
            if (tempStat != Math.Floor(tempStat))
            {
                tempRng = GameInstance.Rng.Next(1, 3);
                if (tempRng == 1)
                    return (int)Math.Floor(tempStat);
                else
                    return (int)Math.Ceiling(tempStat);
            }
            else
                return (int)tempStat;
        }

        /// <summary>
        /// Method that returns a true if the sharpmon used to call the method is alive (current hp greater than 0).
        /// </summary>
        /// <returns></returns>
        public bool IsAlive()
        {
            bool alive = this.CurrentHp > 0;
            /*If the sharpmon is dead and his current hp is in the negative, it sets it back to 0
              to offer a better use experience and logic to the death system.*/
            if (!alive)
                this.CurrentHp = 0;
            return alive;
        }

        /// <summary>
        /// Static method to get a sharpmon based on its name and on which list
        /// it may possibly be in.
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="sharpmons"></param>
        /// <returns></returns>
        public static Sharpmon GetSharpmon(string Name, List<Sharpmon> sharpmons )
        {
            return ExtractSharpmon(from sharpmon in sharpmons
                                   where Name == sharpmon.Name
                                   select sharpmon);
        }

        /// <summary>
        /// Static method to get a random sharpmon in the list of all the sharpmon of the game.
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public static Sharpmon GetRandomSharpmon(int range)
        {
            return ExtractSharpmon(from sharpmon in GameInstance.AllSharpmons
                                   select GameInstance.AllSharpmons[range]);
        }

        /// <summary>
        /// Satic method that returns a true if the selected list contain a
        /// specific sharpmon called by its name.
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="sharpmons"></param>
        /// <returns></returns>
        public static bool ContainSharpmon(string Name, List<Sharpmon> sharpmons)
        {
            return sharpmons.Contains(GetSharpmon(Name, sharpmons));
        }

        /// <summary>
        /// Extension of the GetSharpmon method that returns the first occurence of sharpmon
        /// if there is one in the IEnumerable created by the LINQ used in the GetSharpmon method.
        /// </summary>
        /// <param name="sharpmon"></param>
        /// <returns></returns>
        public static Sharpmon ExtractSharpmon(IEnumerable<Sharpmon> sharpmon)
        {
            if (sharpmon.Count() > 0)
                return sharpmon.First();
            else
                return null;
        }

        /// <summary>
        /// Method for saving all the data of the instance of an attack type object
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", this.Name, typeof(string));
            info.AddValue("SharpmonType", this.SharpmonType, typeof(ElementalType));
            info.AddValue("Level", this.Level, typeof(int));
            info.AddValue("MaxExperience", this.MaxExperience, typeof(int));
            info.AddValue("CurrentExperience", this.CurrentExperience, typeof(int));
            info.AddValue("MaxHp", this.MaxHp, typeof(int));
            info.AddValue("CurrentHp", this.CurrentHp, typeof(int));
            info.AddValue("BasePower", this.BasePower, typeof(int));
            info.AddValue("BaseDefense", this.BaseDefense, typeof(int));
            info.AddValue("BaseDodge", this.BaseDodge, typeof(int));
            info.AddValue("BaseAccucary", this.BaseAccucary, typeof(int));
            info.AddValue("BaseSpeed", this.BaseSpeed, typeof(int));
            info.AddValue("AttacksList", this.AttacksList, typeof(List<Attack>));
        }
    }
}