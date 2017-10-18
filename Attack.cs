using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Sharpmon_213979
{
    [Serializable]                                  //Indicate that this class thus object is serializable (used for saving the game)
    public class Attack : Effect, ISerializable     //The Attack class inherit from the Effect class and also from the ISerializable Interface (so do all the other classes).
    {
        //FIELDS
        private int FinalDamage;                    //Variable we later use to know the TRUE damage an attack will do taking many factor in count.
        private double AttackSuccess;               //Variable we later use to know the success rate of an attack against a specific sharpmon at a specific moment.
        public enum ElementalType { FIRE, BUG, ICE, GROUND, WATER, GRASS, ELECTRIC, NORMAL, POISON, FLYING, FIGHTING, PSYCHIC }     //An enum that allow use to specify the element of the attack.
        private ElementalType AttackType;           //The variable that stocks the element of the said attack. This does imply the papper-rock-cissor weakness and advantage of each type against one another.

        //PROPERTIES
        /// <summary>
        /// Method to get the string value of each type (used later for the attack system)
        /// </summary>
        /// <returns></returns>
        public string GetElementalType()
        {
            if (this.AttackType == ElementalType.BUG) return "BUG";
            else if (this.AttackType == ElementalType.FLYING) return "FLYING";
            else if (this.AttackType == ElementalType.FIRE) return "FIRE";
            else if (this.AttackType == ElementalType.WATER) return "WATER";
            else if (this.AttackType == ElementalType.GRASS) return "GRASS";
            else if (this.AttackType == ElementalType.GROUND) return "GROUND";
            else if (this.AttackType == ElementalType.ELECTRIC) return "ELECTRIC";
            else if (this.AttackType == ElementalType.POISON) return "POISON";
            else if (this.AttackType == ElementalType.ICE) return "ICE";
            else if (this.AttackType == ElementalType.FIGHTING) return "FIGHTING";
            else if (this.AttackType == ElementalType.PSYCHIC) return "PSYCHIC";
            else return "NORMAL";
        }

        //CONSTRUCTOR
        /// <summary>
        /// Constructor for attack of certain type (with type needed)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="attackType"></param>
        /// <param name="effectTarget"></param>
        /// <param name="damage"></param>
        /// <param name="power"></param>
        /// <param name="defense"></param>
        /// <param name="dodge"></param>
        /// <param name="accucary"></param>
        /// <param name="speed"></param>
        public Attack(string name, ElementalType attackType, Target effectTarget, int damage = 0, int power = 0, int defense = 0, int dodge = 0, int accucary = 0, int speed = 0)
        {
            this.Name = name;
            this.AttackType = attackType;
            this.EffectTarget = effectTarget;
            this.Damage = damage;
            this.Power = power;
            this.Defense = defense;
            this.Dodge = dodge;
            this.Accucary = accucary;
            this.Speed = speed;
            this.FinalDamage = 0;
            this.AttackSuccess = 0;
        }

        /// <summary>
        /// Contructor for Buff/Debuff or Attacks of type normal (most common type)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="effectTarget"></param>
        /// <param name="damage"></param>
        /// <param name="power"></param>
        /// <param name="defense"></param>
        /// <param name="dodge"></param>
        /// <param name="accucary"></param>
        /// <param name="speed"></param>
        public Attack(string name, Target effectTarget, int damage = 0, int power = 0, int defense = 0, int dodge = 0, int accucary = 0, int speed = 0)
        {
            this.Name = name;
            this.AttackType = ElementalType.NORMAL;
            this.EffectTarget = effectTarget;
            this.Damage = damage;
            this.Power = power;
            this.Defense = defense;
            this.Dodge = dodge;
            this.Accucary = accucary;
            this.Speed = speed;
            this.FinalDamage = 0;
            this.AttackSuccess = 0;
        }

        //SPECIAL CONSTRUCTOR FOR DESERIALIZED VALUES
        /// <summary>
        /// Constructor only used when a save of the game is loaded in order to recreate all the exact same objects.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public Attack(SerializationInfo info, StreamingContext context)
        {
            this.Name = (string)info.GetValue("Name", typeof(string));
            this.Damage = (int)info.GetValue("Damage", typeof(int));
            this.Power = (int)info.GetValue("Power", typeof(int));
            this.Defense = (int)info.GetValue("Defense", typeof(int));
            this.Dodge = (int)info.GetValue("Dodge", typeof(int));
            this.Accucary = (int)info.GetValue("Accucary", typeof(int));
            this.Speed = (int)info.GetValue("Speed", typeof(int));
            this.EffectTarget = (Target)info.GetValue("EffectTarget", typeof(Target));
            this.AttackType = (ElementalType)info.GetValue("AttackType", typeof(ElementalType));
        }

        //METHODS
        /// <summary>
        /// Allow a Sharpmon to launch an attack. The launcher is always the sharpmon attacking, the receiver is always the other one.
        /// In case of buffs, they target the launcher itself.
        /// </summary>
        /// <param name="launcher"></param>
        /// <param name="receiver"></param>
        public void LaunchAttack(Sharpmon launcher, Sharpmon receiver)
        {
            launcher.GetColorElementalType();                   //Change the color of the displayed attack name for a better user experience.
            Console.Write($"{launcher.Name} ");                 
            Console.ForegroundColor = ConsoleColor.White;       //Re-set the color back to white
            Console.WriteLine($"uses {this.Name}.");
            switch (this.EffectTarget)
            {
                /* Check the variable that stores the target of the attack used.
                   If it is SELF, then the attack know that it acts as a buff adding
                   every stat of the attack to the launcher.*/
                case Target.SELF:
                    launcher.CurrentPower += this.Power;
                    launcher.CurrentDefense += this.Defense;
                    launcher.CurrentDodge += this.Dodge;
                    launcher.CurrentAccucary += this.Accucary;
                    launcher.CurrentSpeed += this.Speed;

                    /*Those 5 lines offer a better user experience by telling which stats got buffed.*/
                    if (this.Power > 0) Console.WriteLine($"{launcher.Name} gained {this.Power} Power!");
                    if (this.Defense > 0) Console.WriteLine($"{launcher.Name} gained {this.Defense} Defense!");
                    if (this.Dodge > 0) Console.WriteLine($"{launcher.Name} gained {this.Dodge} Dodge!");
                    if (this.Accucary > 0) Console.WriteLine($"{launcher.Name} gained {this.Accucary} Accucary!");
                    if (this.Speed > 0) Console.WriteLine($"{launcher.Name} gained {this.Speed} Speed!");
                    break;
                    
                /* Check the variable that stores the target of the attack used.
                   If it is ENEMY, then the attack knows that it acts as either 
                   a damage dealing type of attack or a debuff */
                case Target.ENEMY:
                    if (this.Damage > 0)                        //Check if the attack's damage stat is greater than 0, if so it means that the attack is a damage dealing type of attack
                    {
                        if(IsTargetImmune(receiver))            //Very first check to see if the ennemy's type is immunised to this attack's type.
                            Console.WriteLine($"{receiver.Name} is immuned to this type of attack.");
                        else
                        {
                            /* Define the value of the AttackSuccess variable based on the Launcher's accucary stat 
                               and receiver's dodge stat (this formula is close to the real one used in pokemon).*/
                            this.AttackSuccess = (double)launcher.CurrentAccucary / receiver.CurrentDodge + 0.1;

                            /*Do a random between 0 and 100 and check if its equal or lower than the attack success
                              calcul made just before multiplied by 100 (cause else we'd have a number below 1.
                              If so, the attack is successfully launched on the ennemy.*/
                            if (GameInstance.Rng.Next(101) <= this.AttackSuccess * 100)
                            {
                                /*Define the value of the Final Damage variable based on the many factors 
                                  (this formula is close to the real one used in pokemon).*/
                                this.FinalDamage = AttackCalculation(((2*launcher.GetLevel()/5)*launcher.CurrentPower * this.Damage / receiver.CurrentDefense)/50 + 2);
                                /*Second check to see if the ennemy's type is resistant to this attack's type.
                                  If so, the final damage of this attack is divised by 2 (same as in the pokemon games)*/
                                if (IsTargetResitant(receiver))
                                {
                                    Console.WriteLine("It's not very effective...");
                                    this.FinalDamage /= 2;
                                }
                                /*Third check to see if the ennemy's type is resistant to this attack's type.
                                  If so, the final damage of this attack is multiplied by 2 to encourage player's
                                  to uses attacks strategicly*/
                                else if (IsTargetWeak(receiver))
                                {
                                    Console.WriteLine("It's super effective!");
                                    this.FinalDamage *= 2;
                                }
                                /*Fourth and final check to see if the attack used is False Swipe and if the damage dealt
                                   would in fact kill the target. If so, the target stays at exactly 1 hp.
                                   This attack is present to offer greater chances of capture (same as in pokemon).*/
                                if (this.Name == "False Swipe" && receiver.CurrentHp - this.FinalDamage <= 0)
                                {
                                    receiver.CurrentHp = 1;
                                    Console.WriteLine($"{receiver.Name} took {this.FinalDamage} damages !");
                                }
                                /*If not a False swipe attack, normally launch the attack 
                                  with the modified (or not) calculated damage from the previous checks.*/
                                else
                                {
                                    receiver.CurrentHp -= this.FinalDamage;
                                    Console.WriteLine($"{receiver.Name} took {this.FinalDamage} damages !");
                                }
                            }
                            /*If the random number is greater than the attack success, then the attack fail.*/
                            else
                                Console.WriteLine($"{launcher.Name} missed his {this.Name} !");
                        }
                    }
                    /*If the attack's damage stat is equal to zero then act as a debuff type of attack.
                      It acts as the exact opposite of the buff since it lowers the receiver's stats
                      by the attack's stats.*/
                    else if(this.Damage == 0)
                    {
                        /*If the receiver's stats minus the attack's stats results are lower than 1
                          then the debuff has no effect.*/
                        if (receiver.CurrentPower - this.Power < 1 || receiver.CurrentDefense - this.Defense < 1 ||
                            receiver.CurrentDodge - this.Dodge < 1 || receiver.CurrentAccucary - this.Accucary < 1 ||
                            receiver.CurrentSpeed - this.Speed < 1)
                            Console.WriteLine($"{this.Name} is inefective against {receiver.Name}!");

                        /*Such as for the buff, the actual losses of stats are displayed to the player
                          for a better use experience.*/
                        if (receiver.CurrentPower > 1 && this.Power > 0)
                        {
                            receiver.CurrentPower -= this.Power;
                            Console.WriteLine($"{receiver.Name} lost {this.Power} Power!");
                        }
                        if (receiver.CurrentDefense > 1 && this.Defense > 0)
                        {
                            receiver.CurrentDefense -= this.Defense;
                            Console.WriteLine($"{receiver.Name} lost {this.Defense} Defense!");
                        }
                        if (receiver.CurrentDodge > 1 && this.Dodge > 0)
                        {
                            receiver.CurrentDodge -= this.Dodge;
                            Console.WriteLine($"{receiver.Name} lost {this.Dodge} Dodge!");
                        }
                        if (receiver.CurrentAccucary > 1 && this.Accucary > 0)
                        {
                            receiver.CurrentAccucary -= this.Accucary;
                            Console.WriteLine($"{receiver.Name} lost {this.Accucary} Accucary!");
                        }
                        if (receiver.CurrentSpeed > 1 && this.Speed > 0)
                        {
                            receiver.CurrentSpeed -= this.Speed;
                            Console.WriteLine($"{receiver.Name} lost {this.Speed} Speed!");
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Method used in the "LaunchAttack" to, in case of the tempStat is not round
        /// choose randomly between the upper and lower floor of the decimal number.
        /// </summary>
        /// <param name="tempStat"></param>
        /// <returns></returns>
        public int AttackCalculation(double tempStat)
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
        /// Method wich returns a boolean to check if the target is immunised to a certain attack type.
        /// Returns true if he is indeed immunised, returns false otherwise.
        /// </summary>
        /// <param name="receiver"></param>
        /// <returns></returns>
        public bool IsTargetImmune(Sharpmon receiver)
        {
            if ((this.AttackType == ElementalType.ELECTRIC && receiver.SharpmonType == Sharpmon.ElementalType.GROUND) ||
                (this.AttackType == ElementalType.GROUND && receiver.SharpmonType == Sharpmon.ElementalType.FLYING))
                return true;
            return false;
        }

        /// <summary>
        /// Method wich returns a boolean to check if the target is resistant to a certain attack type.
        /// Returns true if he is indeed resistant, returns false otherwise.
        /// </summary>
        /// <param name="receiver"></param>
        /// <returns></returns>
        public bool IsTargetResitant(Sharpmon receiver)
        {
            if ((this.AttackType == ElementalType.ELECTRIC && receiver.SharpmonType == Sharpmon.ElementalType.GRASS) ||
                (this.AttackType == ElementalType.ELECTRIC && receiver.SharpmonType == Sharpmon.ElementalType.ELECTRIC) ||
                (this.AttackType == ElementalType.ICE && receiver.SharpmonType == Sharpmon.ElementalType.WATER) ||
                (this.AttackType == ElementalType.GROUND && receiver.SharpmonType == Sharpmon.ElementalType.BUG) ||
                (this.AttackType == ElementalType.GROUND && receiver.SharpmonType == Sharpmon.ElementalType.GRASS) ||
                (this.AttackType == ElementalType.FLYING && receiver.SharpmonType == Sharpmon.ElementalType.ELECTRIC) ||
                (this.AttackType == ElementalType.WATER && receiver.SharpmonType == Sharpmon.ElementalType.WATER) ||
                (this.AttackType == ElementalType.WATER && receiver.SharpmonType == Sharpmon.ElementalType.GRASS) ||
                (this.AttackType == ElementalType.GRASS && receiver.SharpmonType == Sharpmon.ElementalType.GRASS) ||
                (this.AttackType == ElementalType.GRASS && receiver.SharpmonType == Sharpmon.ElementalType.FLYING) ||
                (this.AttackType == ElementalType.GRASS && receiver.SharpmonType == Sharpmon.ElementalType.FIRE) ||
                (this.AttackType == ElementalType.FIRE && receiver.SharpmonType == Sharpmon.ElementalType.FIRE) ||
                (this.AttackType == ElementalType.FIRE && receiver.SharpmonType == Sharpmon.ElementalType.WATER) ||
                (this.AttackType == ElementalType.FIGHTING && receiver.SharpmonType == Sharpmon.ElementalType.BUG) ||
                (this.AttackType == ElementalType.FIGHTING && receiver.SharpmonType == Sharpmon.ElementalType.FLYING) ||
                (this.AttackType == ElementalType.FIGHTING && receiver.SharpmonType == Sharpmon.ElementalType.PSYCHIC) ||
                (this.AttackType == ElementalType.PSYCHIC && receiver.SharpmonType == Sharpmon.ElementalType.PSYCHIC))
                return true;

            return false;
        }

        /// <summary>
        /// Method wich returns a boolean to check if the target is weak against a certain attack type.
        /// Returns true if he is indeed weak, returns false otherwise.
        /// </summary>
        /// <param name="receiver"></param>
        /// <returns></returns>
        public bool IsTargetWeak(Sharpmon receiver)
        {
            if ((this.AttackType == ElementalType.ELECTRIC && receiver.SharpmonType == Sharpmon.ElementalType.FLYING) ||
                (this.AttackType == ElementalType.ELECTRIC && receiver.SharpmonType == Sharpmon.ElementalType.WATER) ||
                (this.AttackType == ElementalType.ICE && receiver.SharpmonType == Sharpmon.ElementalType.FLYING) ||
                (this.AttackType == ElementalType.ICE && receiver.SharpmonType == Sharpmon.ElementalType.GRASS) ||
                (this.AttackType == ElementalType.ICE && receiver.SharpmonType == Sharpmon.ElementalType.GROUND) ||
                (this.AttackType == ElementalType.GROUND && receiver.SharpmonType == Sharpmon.ElementalType.ELECTRIC) ||
                (this.AttackType == ElementalType.GROUND && receiver.SharpmonType == Sharpmon.ElementalType.FIRE) ||
                (this.AttackType == ElementalType.FLYING && receiver.SharpmonType == Sharpmon.ElementalType.BUG) ||
                (this.AttackType == ElementalType.FLYING && receiver.SharpmonType == Sharpmon.ElementalType.GRASS) ||
                (this.AttackType == ElementalType.WATER && receiver.SharpmonType == Sharpmon.ElementalType.FIRE) ||
                (this.AttackType == ElementalType.WATER && receiver.SharpmonType == Sharpmon.ElementalType.GROUND) ||
                (this.AttackType == ElementalType.GRASS && receiver.SharpmonType == Sharpmon.ElementalType.GROUND) ||
                (this.AttackType == ElementalType.GRASS && receiver.SharpmonType == Sharpmon.ElementalType.WATER) ||
                (this.AttackType == ElementalType.FIRE && receiver.SharpmonType == Sharpmon.ElementalType.BUG) ||
                (this.AttackType == ElementalType.FIRE && receiver.SharpmonType == Sharpmon.ElementalType.GRASS) ||
                (this.AttackType == ElementalType.BUG && receiver.SharpmonType == Sharpmon.ElementalType.PSYCHIC) ||
                (this.AttackType == ElementalType.FIGHTING && receiver.SharpmonType == Sharpmon.ElementalType.NORMAL) ||
                (this.AttackType == ElementalType.PSYCHIC && receiver.SharpmonType == Sharpmon.ElementalType.FIGHTING))
                return true;

            return false;
        }

        /// <summary>
        /// Static method to get an attack by its name in the list containing 
        /// all the attack of the game using LINQ.
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static Attack GetAttack(string Name)
        {
            return GameInstance.AllAttacks.FirstOrDefault(attack => attack.Name == Name);
        }

        /// <summary>
        /// Method for saving all the data of the instance of an attack type object
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", this.Name, typeof(string));
            info.AddValue("Damage", this.Damage, typeof(int));
            info.AddValue("Power", this.Power, typeof(int));
            info.AddValue("Defense", this.Defense, typeof(int));
            info.AddValue("Dodge", this.Dodge, typeof(int));
            info.AddValue("Accucary", this.Accucary, typeof(int));
            info.AddValue("Speed", this.Speed, typeof(int));
            info.AddValue("EffectTarget", this.EffectTarget, typeof(Target));
            info.AddValue("AttackType", this.AttackType, typeof(ElementalType));
        }
    }
}
