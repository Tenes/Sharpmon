using System;
using Newtonsoft.Json;

namespace Sharpmon
{
    /// <summary>
    /// The "mother" class of both the Attack and the Item classes.
    /// It allows us to create variables for both of them without declaring them here.
    /// </summary>
    [Serializable]  
    public abstract class Effect    //The class is abstract so that no-one can ever instanciate it.
    {
        //FIELDS
        [JsonProperty]
        protected string Name;
        [JsonProperty]
        protected int Damage;       //Used by the Attack class to know how much damage an attack has.
        
        /*Used by the Attack class to know how much it buffs/debuffs the user/receiver's Power of the said attack.
          In the Item class it is used to know the permanent Power buff it gives to a selected ally Sharpmon.*/
        [JsonProperty]
        protected int Power;

        /*Used by the Attack class to know how much it buffs/debuffs the user/receiver's Defense of the said attack.
          In the Item class it is used to know the permanent Defense buff it gives to a selected ally Sharpmon.*/
        [JsonProperty]
        protected int Defense;

        /*Used by the Attack class to know how much it buffs/debuffs the user/receiver's Dodge of the said attack.
          In the Item class it is used to know the permanent Dodge buff it gives to a selected ally Sharpmon.*/
        [JsonProperty]
        protected int Dodge;

        /*Used by the Attack class to know how much it buffs/debuffs the user/receiver's Accucary of the said attack.
          In the Item class it is used to know the permanent Accucary buff it gives to a selected ally Sharpmon.*/
        [JsonProperty]
        protected int Accucary;

        /*Used by the Attack class to know how much it buffs/debuffs the user/receiver's Speed of the said attack.
          In the Item class it is used to know the permanent Speed buff it gives to a selected ally Sharpmon.*/
        [JsonProperty]
        protected int Speed;
        [JsonProperty]
        protected int Price;                //Used in the Item class know the price of an item.
        [JsonProperty]
        protected int SellPrice;            //Used in the Item class know the selling price of an item (always Price divided by two).
        [JsonProperty]
        protected string Description;       //Used in the Item class know the effect of an item. Could be used in the Attack class for the same purpose.
        public enum Target{ ENEMY, SELF }   //Used in the Attack class to determine the target of it. (SELF : act as a Buff, ENEMY: act as a Damage dealing attack or Debuff)
        [JsonProperty]
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
