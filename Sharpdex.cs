using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sharpmon_213979
{
    /// <summary>
    /// Static class that stores all the information of the existing object of the game.
    /// The class itself contains all the Attacks, the Items and the Sharpmons present in the game.
    /// </summary>
    public static class Sharpdex
    {
        //METHODS
        public static void CreateAttacks(List<Attack> AllAttacks)
        {
            //OFFENSIVE ATTACKS
            AllAttacks.Add(new Attack("Scratch", Effect.Target.ENEMY, 30));
            AllAttacks.Add(new Attack("Pound", Effect.Target.ENEMY, 20));
            AllAttacks.Add(new Attack("False Swipe", Effect.Target.ENEMY, 50));
            AllAttacks.Add(new Attack("Tri Attack", Effect.Target.ENEMY, 80));

            AllAttacks.Add(new Attack("Poison Sting", Attack.ElementalType.POISON, Effect.Target.ENEMY, 30));

            AllAttacks.Add(new Attack("Peck", Attack.ElementalType.FLYING, Effect.Target.ENEMY, 30));
            AllAttacks.Add(new Attack("Wing Attack", Attack.ElementalType.FLYING, Effect.Target.ENEMY, 50));
            AllAttacks.Add(new Attack("Drill Peck", Attack.ElementalType.FLYING, Effect.Target.ENEMY, 60));
            AllAttacks.Add(new Attack("Aeroblast", Attack.ElementalType.FLYING, Effect.Target.ENEMY, 100));
            AllAttacks.Add(new Attack("Sky Attack", Attack.ElementalType.FLYING, Effect.Target.ENEMY, 150));

            AllAttacks.Add(new Attack("Thundershock", Attack.ElementalType.ELECTRIC, Effect.Target.ENEMY, 40));
            AllAttacks.Add(new Attack("Thunder Punch", Attack.ElementalType.ELECTRIC, Effect.Target.ENEMY, 70));
            AllAttacks.Add(new Attack("Thunder", Attack.ElementalType.ELECTRIC, Effect.Target.ENEMY, 120));

            AllAttacks.Add(new Attack("Ember", Attack.ElementalType.FIRE, Effect.Target.ENEMY, 40));
            AllAttacks.Add(new Attack("Fire Punch", Attack.ElementalType.FIRE, Effect.Target.ENEMY, 60));
            AllAttacks.Add(new Attack("Flamethrower", Attack.ElementalType.FIRE, Effect.Target.ENEMY, 90));
            AllAttacks.Add(new Attack("Fire Blast", Attack.ElementalType.FIRE, Effect.Target.ENEMY, 120));

            AllAttacks.Add(new Attack("Vine Whip", Attack.ElementalType.GRASS, Effect.Target.ENEMY, 40));
            AllAttacks.Add(new Attack("Razor Leaf", Attack.ElementalType.GRASS, Effect.Target.ENEMY, 60));
            AllAttacks.Add(new Attack("Solar Beam", Attack.ElementalType.GRASS, Effect.Target.ENEMY, 120));

            AllAttacks.Add(new Attack("Water Gun", Attack.ElementalType.WATER, Effect.Target.ENEMY, 40));
            AllAttacks.Add(new Attack("Dive", Attack.ElementalType.WATER, Effect.Target.ENEMY, 80));
            AllAttacks.Add(new Attack("Hydro Cannon", Attack.ElementalType.WATER, Effect.Target.ENEMY, 150));

            AllAttacks.Add(new Attack("Bonemerang", Attack.ElementalType.GROUND, Effect.Target.ENEMY, 50));
            AllAttacks.Add(new Attack("Bone Club", Attack.ElementalType.GROUND, Effect.Target.ENEMY, 70));
            AllAttacks.Add(new Attack("Earthquake", Attack.ElementalType.GROUND, Effect.Target.ENEMY, 100));

            AllAttacks.Add(new Attack("Twineedle", Attack.ElementalType.BUG, Effect.Target.ENEMY, 30));
            AllAttacks.Add(new Attack("Fury Cutter", Attack.ElementalType.BUG, Effect.Target.ENEMY, 60));
            AllAttacks.Add(new Attack("X-Scissor", Attack.ElementalType.BUG, Effect.Target.ENEMY, 90));

            AllAttacks.Add(new Attack("Rock Smash", Attack.ElementalType.FIGHTING, Effect.Target.ENEMY, 40));
            AllAttacks.Add(new Attack("Karate Chop", Attack.ElementalType.FIGHTING, Effect.Target.ENEMY, 50));
            AllAttacks.Add(new Attack("Jump Kick", Attack.ElementalType.FIGHTING, Effect.Target.ENEMY, 90));
            AllAttacks.Add(new Attack("Focus Punch", Attack.ElementalType.FIGHTING, Effect.Target.ENEMY, 140));

            AllAttacks.Add(new Attack("Ice Beam", Attack.ElementalType.ICE, Effect.Target.ENEMY, 100));

            AllAttacks.Add(new Attack("Future Sight", Attack.ElementalType.PSYCHIC, Effect.Target.ENEMY, 110));

            //BUFF-DEBUFF ATTACKS
            AllAttacks.Add(new Attack("Grawl", Effect.Target.SELF, 0, 1));
            AllAttacks.Add(new Attack("Shell", Effect.Target.SELF, 0, 0, 1));
            AllAttacks.Add(new Attack("Foliage", Effect.Target.SELF, 0, 0, 0, 1));
            AllAttacks.Add(new Attack("Sharpen", Effect.Target.SELF, 0, 5));
            AllAttacks.Add(new Attack("Agility", Effect.Target.SELF, 0, 0, 0, 5, 5));
            AllAttacks.Add(new Attack("Haste", Effect.Target.SELF, 0, 0, 0, 0, 0, 5));
            AllAttacks.Add(new Attack("Harden", Effect.Target.SELF, 0, 0, 5));
            AllAttacks.Add(new Attack("Fire Dance", Attack.ElementalType.FIRE, Effect.Target.SELF, 0, 5, 0, 5, 5));
            AllAttacks.Add(new Attack("Water Dance", Attack.ElementalType.WATER, Effect.Target.SELF, 0, 0, 5, 5, 5));
            AllAttacks.Add(new Attack("Electric Dance", Attack.ElementalType.ELECTRIC, Effect.Target.SELF, 0, 0, 0, 5, 5, 5));
            AllAttacks.Add(new Attack("Cosmic Power", Attack.ElementalType.PSYCHIC, Effect.Target.SELF, 0, 20));
            AllAttacks.Add(new Attack("Cosmic Defense", Attack.ElementalType.PSYCHIC, Effect.Target.SELF, 0, 0, 20));

            AllAttacks.Add(new Attack("String Shot", Attack.ElementalType.BUG, Effect.Target.ENEMY, 0, 0, 0, 0, 0, 1));
            AllAttacks.Add(new Attack("Sand-Attack", Attack.ElementalType.GROUND, Effect.Target.ENEMY, 0, 0, 0, 0, 1));
            AllAttacks.Add(new Attack("Tail Whip", Effect.Target.ENEMY, 0, 0, 1));
            AllAttacks.Add(new Attack("Water Sport", Attack.ElementalType.WATER, Effect.Target.ENEMY, 0, 1));
        }

        public static void CreateItems(List<Item> AllItems)
        {
            AllItems.Add(new Item("Potion", 100, "Adds 10HP to a Sharpmon", 10));
            AllItems.Add(new Item("Sup Potion", 300, "Adds 50HP to a Sharpmon", 50));
            AllItems.Add(new Item("Hyp Potion", 300, "Adds 500HP to a Sharpmon", 500));
            AllItems.Add(new Item("Rare candy", 1500, "The Sharpmon levels up", 0, 0, 0, 0, 0, 0, 1));
            AllItems.Add(new Item("HP up", 1500, "The Sharpmon max health increases by 1", 0, 0, 0, 0, 0, 0, 0, 1));
            AllItems.Add(new Item("Protein", 1500, "The Sharpmon base attack increases by 1", 0, 1));
            AllItems.Add(new Item("Iron", 1500, "The Sharpmon base defense increases by 1", 0, 0, 1));
            AllItems.Add(new Item("Calcium", 1500, "The Sharpmon base dodge increases by 1", 0, 0, 0, 1));
            AllItems.Add(new Item("Zinc", 1500, "The Sharpmon base accucary increases by 1", 0, 0, 0, 0, 1));
            AllItems.Add(new Item("Carbos", 2000, "The Sharpmon base speed increases by 1", 0, 0, 0, 0, 0, 1));
        }

        public static void CreateBadges(List<string> Badges)
        {
            Badges.Add("Boulder");
            Badges.Add("Cascade");
            Badges.Add("Thunder");
            Badges.Add("Rainbow");
            Badges.Add("Marsh");
            Badges.Add("Soul");
            Badges.Add("Volcano");
            Badges.Add("Earth");
            Badges.Add("The Elite Four");
        }

        public static void CreateTowns(List<string> Towns)
        {

            Towns.Add("Pewter City");
            Towns.Add("Cerulean City");
            Towns.Add("Vermillon City");
            Towns.Add("Celadon City");
            Towns.Add("Saffron City");
            Towns.Add("Fuschia City");
            Towns.Add("Cinnabar Island");
            Towns.Add("Viridian City");
            Towns.Add("the Indigo Plateau");
        }

        public static void CreateSharpmons(List<Sharpmon> AllSharpmons)
        {
            //STARTERS
            AllSharpmons.Add(new Sharpmon("Sharpmender", Sharpmon.ElementalType.FIRE, 10, 10, 1, 1, 1, 2, 1, Attack.GetAttack("Ember"), Attack.GetAttack("Grawl")));
            AllSharpmons.Add(new Sharpmon("Sharpasaur", Sharpmon.ElementalType.GRASS, 9, 9, 1, 1, 3, 2, 2, Attack.GetAttack("Vine Whip"), Attack.GetAttack("Foliage")));
            AllSharpmons.Add(new Sharpmon("Sharpitle", Sharpmon.ElementalType.WATER, 11, 11, 1, 2, 2, 1, 2, Attack.GetAttack("Water Gun"), Attack.GetAttack("Shell")));
            AllSharpmons.Add(new Sharpmon("Sharpichu", Sharpmon.ElementalType.ELECTRIC, 11, 11, 1, 1, 2, 2, 2, Attack.GetAttack("Thundershock"), Attack.GetAttack("Tail Whip")));

            //COMMON SHARPMON
            AllSharpmons.Add(new Sharpmon("Sharptata", Sharpmon.ElementalType.NORMAL, 11, 11, 1, 1, 1, 2, 1, Attack.GetAttack("Scratch"), Attack.GetAttack("Tail Whip")));
            AllSharpmons.Add(new Sharpmon("Sharpeowth", Sharpmon.ElementalType.NORMAL, 10, 10, 2, 1, 1, 1, 2, Attack.GetAttack("Scratch"), Attack.GetAttack("Grawl")));
            AllSharpmons.Add(new Sharpmon("Sharpduo", Sharpmon.ElementalType.NORMAL, 10, 10, 2, 1, 1, 2, 2, Attack.GetAttack("Peck"), Attack.GetAttack("Scratch")));
            AllSharpmons.Add(new Sharpmon("Sharplypuff", Sharpmon.ElementalType.NORMAL, 12, 12, 2, 2, 2, 1, 1, Attack.GetAttack("False Swipe"), Attack.GetAttack("Foliage")));
            AllSharpmons.Add(new Sharpmon("Sharpterpie", Sharpmon.ElementalType.BUG, 8, 8, 1, 1, 1, 1, 1, Attack.GetAttack("Pound"), Attack.GetAttack("String Shot")));
            AllSharpmons.Add(new Sharpmon("Sharpeedle", Sharpmon.ElementalType.BUG, 8, 8, 1, 1, 1, 1, 1, Attack.GetAttack("Poison Sting"), Attack.GetAttack("String Shot")));
            AllSharpmons.Add(new Sharpmon("Sharpangela", Sharpmon.ElementalType.GRASS, 10, 10, 2, 1, 2, 1, 2, Attack.GetAttack("Vine Whip"), Attack.GetAttack("False Swipe")));
            AllSharpmons.Add(new Sharpmon("Sharprout", Sharpmon.ElementalType.GRASS, 11, 11, 3, 1, 1, 2, 1, Attack.GetAttack("Razor Leaf"), Attack.GetAttack("Pound")));
            AllSharpmons.Add(new Sharpmon("Sharpoddish", Sharpmon.ElementalType.GRASS, 9, 9, 1, 2, 2, 1, 1, Attack.GetAttack("Scratch"), Attack.GetAttack("Foliage")));
            AllSharpmons.Add(new Sharpmon("Sharpidgey", Sharpmon.ElementalType.FLYING, 10, 10, 2, 1, 1, 1, 3, Attack.GetAttack("Pound"), Attack.GetAttack("Sand-Attack")));
            AllSharpmons.Add(new Sharpmon("Sharpearow", Sharpmon.ElementalType.FLYING, 11, 11, 3, 1, 1, 2, 1, Attack.GetAttack("Peck"), Attack.GetAttack("Grawl")));
            AllSharpmons.Add(new Sharpmon("Sharpuubat", Sharpmon.ElementalType.FLYING, 9, 9, 3, 2, 3, 2, 1, Attack.GetAttack("Poison Sting"), Attack.GetAttack("Wing Attack")));
            AllSharpmons.Add(new Sharpmon("Sharpshrew", Sharpmon.ElementalType.GROUND, 8, 8, 3, 1, 1, 3, 3, Attack.GetAttack("Scratch"), Attack.GetAttack("Shell")));
            AllSharpmons.Add(new Sharpmon("Sharplett", Sharpmon.ElementalType.GROUND, 11, 11, 2, 2, 1, 1, 1, Attack.GetAttack("Scratch"), Attack.GetAttack("Sand-Attack")));
            AllSharpmons.Add(new Sharpmon("Sharpodude", Sharpmon.ElementalType.GROUND, 12, 12, 1, 2, 1, 3, 1, Attack.GetAttack("Rock Smash"), Attack.GetAttack("Shell")));
            AllSharpmons.Add(new Sharpmon("Sharpix", Sharpmon.ElementalType.FIRE, 8, 8, 3, 2, 2, 3, 3, Attack.GetAttack("Ember"), Attack.GetAttack("Tail Whip")));
            AllSharpmons.Add(new Sharpmon("Sharponix", Sharpmon.ElementalType.GROUND, 13, 13, 2, 2, 1, 1, 1, Attack.GetAttack("Pound"), Attack.GetAttack("Shell")));
            AllSharpmons.Add(new Sharpmon("Sharpowlithe", Sharpmon.ElementalType.FIRE, 9, 9, 2, 1, 2, 2, 1, Attack.GetAttack("Scratch"), Attack.GetAttack("Ember")));
            AllSharpmons.Add(new Sharpmon("Sharponyta", Sharpmon.ElementalType.FIRE, 11, 11, 2, 1, 2, 2, 1, Attack.GetAttack("Ember"), Attack.GetAttack("Haste")));
            AllSharpmons.Add(new Sharpmon("Sharpbby", Sharpmon.ElementalType.WATER, 11, 11, 2, 3, 1, 2, 1, Attack.GetAttack("Scratch"), Attack.GetAttack("Shell")));
            AllSharpmons.Add(new Sharpmon("Sharpyduck", Sharpmon.ElementalType.WATER, 11, 11, 1, 2, 1, 4, 1, Attack.GetAttack("Water Sport"), Attack.GetAttack("Scratch")));
            AllSharpmons.Add(new Sharpmon("Sharporsea", Sharpmon.ElementalType.WATER, 10, 10, 2, 1, 1, 2, 2, Attack.GetAttack("Water Gun"), Attack.GetAttack("Pound")));
            AllSharpmons.Add(new Sharpmon("Sharpankey", Sharpmon.ElementalType.FIGHTING, 11, 11, 3, 1, 1, 2, 1, Attack.GetAttack("Rock Smash"), Attack.GetAttack("Foliage")));
            AllSharpmons.Add(new Sharpmon("Sharpchop", Sharpmon.ElementalType.FIGHTING, 11, 11, 3, 2, 1, 3, 2, Attack.GetAttack("Karate Chop"), Attack.GetAttack("Grawl")));
            AllSharpmons.Add(new Sharpmon("Sharpnemite", Sharpmon.ElementalType.ELECTRIC, 9, 9, 2, 1, 1, 2, 2, Attack.GetAttack("Thundershock"), Attack.GetAttack("Pound")));
            AllSharpmons.Add(new Sharpmon("Sharptrode", Sharpmon.ElementalType.ELECTRIC, 11, 11, 1, 3, 1, 1, 1, Attack.GetAttack("Pound"), Attack.GetAttack("Shell")));

            //ADVANCED SHARPMONS
            AllSharpmons.Add(new Sharpmon("Sharpaskhan", Sharpmon.ElementalType.NORMAL, 14, 14, 3, 1, 3, 2, 2, Attack.GetAttack("Tri Attack"), Attack.GetAttack("Rock Smash")));
            AllSharpmons.Add(new Sharpmon("Sharptong", Sharpmon.ElementalType.NORMAL, 15, 15, 2, 2, 2, 5, 2, Attack.GetAttack("Tri Attack"), Attack.GetAttack("Tail Whip")));
            AllSharpmons.Add(new Sharpmon("Sharpbone", Sharpmon.ElementalType.GROUND, 10, 10, 2, 2, 2, 3, 2, Attack.GetAttack("Bonemerang"), Attack.GetAttack("Grawl")));
            AllSharpmons.Add(new Sharpmon("Sharpcyther", Sharpmon.ElementalType.BUG, 13, 13, 3, 2, 2, 2, 2, Attack.GetAttack("Fury Cutter"), Attack.GetAttack("Foliage")));
            AllSharpmons.Add(new Sharpmon("Sharpinsir", Sharpmon.ElementalType.BUG, 14, 14, 2, 3, 2, 2, 1, Attack.GetAttack("Fury Cutter"), Attack.GetAttack("Shell")));
            AllSharpmons.Add(new Sharpmon("Sharpmonchan", Sharpmon.ElementalType.FIGHTING, 13, 13, 2, 2, 3, 3, 2, Attack.GetAttack("Jump Kick"), Attack.GetAttack("Grawl")));
            AllSharpmons.Add(new Sharpmon("Sharpmonlee", Sharpmon.ElementalType.FIGHTING, 13, 13, 4, 1, 2, 4, 3, Attack.GetAttack("Focus Punch"), Attack.GetAttack("Foliage")));
            AllSharpmons.Add(new Sharpmon("Sharpcanine", Sharpmon.ElementalType.FIRE, 15, 15, 4, 1, 2, 3, 4, Attack.GetAttack("Flamethrower"), Attack.GetAttack("Grawl")));
            AllSharpmons.Add(new Sharpmon("Sharplareon", Sharpmon.ElementalType.FIRE, 15, 15, 4, 3, 3, 3, 2, Attack.GetAttack("Fire Blast"), Attack.GetAttack("Fire Dance")));
            AllSharpmons.Add(new Sharpmon("Sharpras", Sharpmon.ElementalType.WATER, 20, 20, 3, 5, 2, 2, 2, Attack.GetAttack("Ice Beam"), Attack.GetAttack("Water Gun")));
            AllSharpmons.Add(new Sharpmon("Sharparados", Sharpmon.ElementalType.WATER, 17, 17, 5, 4, 2, 4, 1, Attack.GetAttack("Dive"), Attack.GetAttack("Shell")));
            AllSharpmons.Add(new Sharpmon("Sharporeon", Sharpmon.ElementalType.WATER, 15, 15, 4, 3, 3, 3, 2, Attack.GetAttack("Hydro Cannon"), Attack.GetAttack("Water Dance")));
            AllSharpmons.Add(new Sharpmon("Sharpolteon", Sharpmon.ElementalType.ELECTRIC, 15, 15, 4, 3, 3, 3, 2, Attack.GetAttack("Thunder"), Attack.GetAttack("Electric Dance")));

            //LEGENDARY SHARPMON
            AllSharpmons.Add(new Sharpmon("Sharpdos", Sharpmon.ElementalType.ELECTRIC, 30, 30, 4, 4, 4, 6, 6, Attack.GetAttack("Thunder"), Attack.GetAttack("Aeroblast")));
            AllSharpmons.Add(new Sharpmon("Sharpoltres", Sharpmon.ElementalType.FIRE, 30, 30, 6, 4, 6, 4, 4, Attack.GetAttack("Fire Blast"), Attack.GetAttack("Sky Attack")));
            AllSharpmons.Add(new Sharpmon("Sharpticuno", Sharpmon.ElementalType.WATER, 30, 30, 4, 6, 6, 4, 4, Attack.GetAttack("Ice Beam"), Attack.GetAttack("Hydro Cannon")));
            AllSharpmons.Add(new Sharpmon("Sharpew", Sharpmon.ElementalType.PSYCHIC, 35, 35, 6, 6, 6, 7, 6, Attack.GetAttack("Future Sight"), Attack.GetAttack("Cosmic Defense")));
            AllSharpmons.Add(new Sharpmon("Sharpewtwo", Sharpmon.ElementalType.PSYCHIC, 35, 35, 7, 6, 6, 6, 6, Attack.GetAttack("Future Sight"), Attack.GetAttack("Cosmic Power")));
        }
    }
}
