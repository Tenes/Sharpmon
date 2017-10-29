using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Sharpmon
{
    [Serializable]                             //Indicate that this class thus object is serializable (used for saving the game)
    public class Sharpmon                       //The Item class inherit from the Effect class and also from the ISerializable Interface (so do all the other classes).
    {
        //FIELDS
        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        private int Level;
        [JsonProperty]
        public int MaxExperience { get; set; }
        [JsonProperty]
        public int CurrentExperience { get; set; }
        [JsonProperty]
        public int MaxHp { get; set; }
        [JsonProperty]
        public int CurrentHp { get; set; }
        [JsonProperty]
        public int BasePower { get; set; }
        [JsonProperty]
        public int CurrentPower { get; set; }
        [JsonProperty]
        public int BaseDefense { get; set; }
        [JsonProperty]
        public int CurrentDefense { get; set; }
        [JsonProperty]
        public int BaseDodge { get; set; }
        [JsonProperty]
        public int CurrentDodge { get; set; }
        [JsonProperty]
        public int BaseAccucary { get; set; }
        [JsonProperty]
        public int CurrentAccucary { get; set; }
        [JsonProperty]
        public int BaseSpeed { get; set; }
        [JsonProperty]
        public int CurrentSpeed { get; set; }
        public enum ElementalType { FIRE, GROUND, WATER, GRASS, ELECTRIC, NORMAL, FLYING, BUG, FIGHTING, PSYCHIC }
        [JsonProperty]
        public ElementalType SharpmonType { get; set; }
        [JsonProperty]
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

        public void SetExp(int x)
        {
            this.CurrentExperience += x;
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
            this.MaxExperience = (int)(4 * Math.Pow(this.Level, 3) / 5);
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
            this.AttacksList = new List<Attack>(){primaryAttack, secondaryAttack};
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
        [JsonConstructor]
        public Sharpmon(string name, ElementalType sharpmonType, int maxHP, int currentHp, int power, int defense, int dodge, int accucary, int speed,
            List<Attack> attackList , int level, int maxExperience, int CurrentExperience = 0)
        {
            this.Name = name;
            this.SharpmonType = sharpmonType;
            this.Level = level;
            this.MaxExperience = maxExperience;
            this.CurrentExperience = CurrentExperience;
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
                if (this.Level == 100)
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
            this.MaxExperience = (int)(4 * Math.Pow(this.Level, 3) / 5);

            Sharpmon baseSharpmon = GetSharpmon(this.Name, Sharpdex.AllSharpmons);

            this.MaxHp = LevelUpSystem((((baseSharpmon.MaxHp * 2) * this.Level)/ 100) + 10 + this.Level);
            this.CurrentHp = this.MaxHp;

            this.BasePower = LevelUpSystem(baseSharpmon.BasePower * (this.Level / 2) + baseSharpmon.BasePower);
            this.CurrentPower = this.BasePower;

            this.BaseDefense = LevelUpSystem(baseSharpmon.BaseDefense * (this.Level / 2) + baseSharpmon.BaseDefense);
            this.CurrentDefense = this.BaseDefense;

            this.BaseDodge = LevelUpSystem(baseSharpmon.BaseDodge * (this.Level / 2) + baseSharpmon.BaseDodge);
            this.CurrentDodge = this.BaseDodge;

            this.BaseAccucary = LevelUpSystem(baseSharpmon.BaseAccucary * (this.Level / 2) + baseSharpmon.BaseAccucary);
            this.CurrentAccucary = this.BaseAccucary;

            this.BaseSpeed = LevelUpSystem(baseSharpmon.BaseSpeed * (this.Level / 2) + baseSharpmon.BaseSpeed);
            this.CurrentSpeed = this.BaseSpeed;

            if (this.Level == 10)
            {
                List<Attack> randomAttack =
                    Sharpdex.AllAttacks
                    .Where(attack => (attack.GetElementalType() == this.GetElementalType() ||
                                   attack.GetElementalType() == "NORMAL")
                                  && attack.GetName() != this.GetAttack(0).GetName() &&
                                  attack.GetName() != this.GetAttack(1).GetName()).ToList();
                this.AttacksList.Add(randomAttack[GameInstance.Rng.Next(0, randomAttack.Count())]);
            }
            if (this.Level == 30)
            {
                List<Attack> randomAttack = Sharpdex.AllAttacks
                    .Where(attack => (attack.GetElementalType() == this.GetElementalType() ||
                                      attack.GetElementalType() == "NORMAL") &&
                                     attack.GetName() != this.GetAttack(0).GetName() &&
                                     attack.GetName() != this.GetAttack(1).GetName() &&
                                     attack.GetName() != this.GetAttack(2).GetName()).ToList();
                this.AttacksList.Add(randomAttack[GameInstance.Rng.Next(0, randomAttack.Count())]);
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
            int finalStat = (GameInstance.Rng.Next(1,3) == 1) ? (int)Math.Floor(tempStat) : (int)Math.Ceiling(tempStat);;
            switch(GameInstance.Rng.Next(1,4))
            {
                case 1:
                    return --finalStat;
                case 2:
                    return finalStat;
                case 3:
                default:
                    return ++finalStat;
            }
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
            return sharpmons.FirstOrDefault(sharpmon => sharpmon.Name == Name);
        }

        /// <summary>
        /// Static method to get a random sharpmon in the list of all the sharpmon of the game.
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public static Sharpmon GetRandomSharpmon(int range)
        {
            return Sharpdex.AllSharpmons[range];
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

        public new void ToString()
        {
            this.GetColorElementalType();
            Console.Write($" {this.Name} ");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}