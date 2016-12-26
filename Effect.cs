using System;

namespace Sharpmon_213979
{
    /// <summary>
    /// The "mother" class of both the Attack and the Item classes.
    /// It allows us to create variables for both of them without declaring them here.
    /// </summary>
    public abstract class Effect    //The class is abstract so that no-one can ever instanciate it.
    {
        //FIELDS
        protected string Name;
        protected int Damage;       //Used by the Attack class to know how much damage an attack has.
        
        /*Used by the Attack class to know how much it buffs/debuffs the user/receiver's Power of the said attack.
          In the Item class it is used to know the permanent Power buff it gives to a selected ally Sharpmon.*/
        protected int Power;

        /*Used by the Attack class to know how much it buffs/debuffs the user/receiver's Defense of the said attack.
          In the Item class it is used to know the permanent Defense buff it gives to a selected ally Sharpmon.*/
        protected int Defense;

        /*Used by the Attack class to know how much it buffs/debuffs the user/receiver's Dodge of the said attack.
          In the Item class it is used to know the permanent Dodge buff it gives to a selected ally Sharpmon.*/
        protected int Dodge;

        /*Used by the Attack class to know how much it buffs/debuffs the user/receiver's Accucary of the said attack.
          In the Item class it is used to know the permanent Accucary buff it gives to a selected ally Sharpmon.*/
        protected int Accucary;

        /*Used by the Attack class to know how much it buffs/debuffs the user/receiver's Speed of the said attack.
          In the Item class it is used to know the permanent Speed buff it gives to a selected ally Sharpmon.*/
        protected int Speed;

        protected int Price;                //Used in the Item class know the price of an item.
        protected int SellPrice;            //Used in the Item class know the selling price of an item (always Price divided by two).
        protected string Description;       //Used in the Item class know the effect of an item. Could be used in the Attack class for the same purpose.
        public enum Target{ ENEMY, SELF }   //Used in the Attack class to determine the target of it. (SELF : act as a Buff, ENEMY: act as a Damage dealing attack or Debuff)
        protected Target EffectTarget;      //The stocker of the Target value. Can contains only ENEMY or SELF.

        //PROPERTIES
        /// <summary>
        /// A getter only for the name, accessible by everyone so that attackExemple.GetName() returns a string and so does itemExemple.GetName() since they inherit that methods.
        /// </summary>
        /// <returns></returns>
        public string GetName() 
        {
            return this.Name;
        }
    }
}
