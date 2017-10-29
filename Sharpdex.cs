using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpmon
{
    /// <summary>
    /// Static class that stores all the information of the existing object of the game.
    /// The class itself contains all the Attacks, the Items and the Sharpmons present in the game.
    /// </summary>
    public static class Sharpdex
    {
        /*Those three lists will act as a bestiary for our game, containing all the
         posiible object that our own object will be based on in our game.*/
        public static List<Attack> AllAttacks = new List<Attack>{       //The list that will contain every existing attack of the game.
            //OFFENSIVE ATTACKS
            new Attack("Scratch", Effect.Target.ENEMY, 30),
            new Attack("Pound", Effect.Target.ENEMY, 20),
            new Attack("False Swipe", Effect.Target.ENEMY, 50),
            new Attack("Tri Attack", Effect.Target.ENEMY, 80),
            new Attack("Hyper Beam", Effect.Target.ENEMY, 150),

            new Attack("Poison Sting", Attack.ElementalType.POISON, Effect.Target.ENEMY, 30),

            new Attack("Peck", Attack.ElementalType.FLYING, Effect.Target.ENEMY, 30),
            new Attack("Wing Attack", Attack.ElementalType.FLYING, Effect.Target.ENEMY, 50),
            new Attack("Drill Peck", Attack.ElementalType.FLYING, Effect.Target.ENEMY, 60),
            new Attack("Aeroblast", Attack.ElementalType.FLYING, Effect.Target.ENEMY, 100),
            new Attack("Hurricane", Attack.ElementalType.FLYING, Effect.Target.ENEMY, 120),
            new Attack("Sky Attack", Attack.ElementalType.FLYING, Effect.Target.ENEMY, 150),

            new Attack("Thundershock", Attack.ElementalType.ELECTRIC, Effect.Target.ENEMY, 40),
            new Attack("Thunder Punch", Attack.ElementalType.ELECTRIC, Effect.Target.ENEMY, 70),
            new Attack("Thunder Fang", Attack.ElementalType.ELECTRIC, Effect.Target.ENEMY, 75),
            new Attack("Thunder", Attack.ElementalType.ELECTRIC, Effect.Target.ENEMY, 120),

            new Attack("Ember", Attack.ElementalType.FIRE, Effect.Target.ENEMY, 40),
            new Attack("Fire Punch", Attack.ElementalType.FIRE, Effect.Target.ENEMY, 60),
            new Attack("Flamethrower", Attack.ElementalType.FIRE, Effect.Target.ENEMY, 90),
            new Attack("Fire Blast", Attack.ElementalType.FIRE, Effect.Target.ENEMY, 120),

            new Attack("Vine Whip", Attack.ElementalType.GRASS, Effect.Target.ENEMY, 40),
            new Attack("Razor Leaf", Attack.ElementalType.GRASS, Effect.Target.ENEMY, 60),
            new Attack("Petal Dance", Attack.ElementalType.GRASS, Effect.Target.ENEMY, 110),
            new Attack("Solar Beam", Attack.ElementalType.GRASS, Effect.Target.ENEMY, 130),

            new Attack("Water Gun", Attack.ElementalType.WATER, Effect.Target.ENEMY, 40),
            new Attack("Dive", Attack.ElementalType.WATER, Effect.Target.ENEMY, 80),
            new Attack("Hydro Cannon", Attack.ElementalType.WATER, Effect.Target.ENEMY, 150),

            new Attack("Bonemerang", Attack.ElementalType.GROUND, Effect.Target.ENEMY, 50),
            new Attack("Rock Tomb", Attack.ElementalType.GROUND, Effect.Target.ENEMY, 60, 0, 0, 0, 0, 2),
            new Attack("Bone Club", Attack.ElementalType.GROUND, Effect.Target.ENEMY, 70),
            new Attack("Earthquake", Attack.ElementalType.GROUND, Effect.Target.ENEMY, 100),

            new Attack("Twineedle", Attack.ElementalType.BUG, Effect.Target.ENEMY, 30),
            new Attack("Fury Cutter", Attack.ElementalType.BUG, Effect.Target.ENEMY, 60),
            new Attack("Sludge", Attack.ElementalType.BUG, Effect.Target.ENEMY, 70, 0, 0, 2, 2),
            new Attack("X-Scissor", Attack.ElementalType.BUG, Effect.Target.ENEMY, 100),
            new Attack("Mega Horn", Attack.ElementalType.BUG, Effect.Target.ENEMY, 120),

            new Attack("Rock Smash", Attack.ElementalType.FIGHTING, Effect.Target.ENEMY, 40),
            new Attack("Karate Chop", Attack.ElementalType.FIGHTING, Effect.Target.ENEMY, 50),
            new Attack("Jump Kick", Attack.ElementalType.FIGHTING, Effect.Target.ENEMY, 90),
            new Attack("Focus Punch", Attack.ElementalType.FIGHTING, Effect.Target.ENEMY, 140),

            new Attack("Ice Beam", Attack.ElementalType.ICE, Effect.Target.ENEMY, 100),

            new Attack("Disable", Attack.ElementalType.PSYCHIC, Effect.Target.ENEMY, 30, 5, 5, 5, 5, 5),
            new Attack("Psychic", Attack.ElementalType.PSYCHIC, Effect.Target.ENEMY, 90, 0, 2),
            new Attack("Future Sight", Attack.ElementalType.PSYCHIC, Effect.Target.ENEMY, 110),

            //BUFF-DEBUFF ATTACKS
            new Attack("Grawl", Effect.Target.SELF, 0, 1),
            new Attack("Shell", Effect.Target.SELF, 0, 0, 1),
            new Attack("Foliage", Effect.Target.SELF, 0, 0, 0, 1),
            new Attack("Sharpen", Effect.Target.SELF, 0, 5),
            new Attack("Agility", Effect.Target.SELF, 0, 0, 0, 5, 5),
            new Attack("Haste", Effect.Target.SELF, 0, 0, 0, 0, 0, 5),
            new Attack("Harden", Effect.Target.SELF, 0, 0, 5),
            new Attack("Fire Dance", Attack.ElementalType.FIRE, Effect.Target.SELF, 0, 5, 0, 5, 5),
            new Attack("Water Dance", Attack.ElementalType.WATER, Effect.Target.SELF, 0, 0, 5, 5, 5),
            new Attack("Electric Dance", Attack.ElementalType.ELECTRIC, Effect.Target.SELF, 0, 0, 0, 5, 5, 5),
            new Attack("Cosmic Power", Attack.ElementalType.PSYCHIC, Effect.Target.SELF, 0, 20),
            new Attack("Cosmic Defense", Attack.ElementalType.PSYCHIC, Effect.Target.SELF, 0, 0, 20),

            new Attack("String Shot", Attack.ElementalType.BUG, Effect.Target.ENEMY, 0, 0, 0, 0, 0, 1),
            new Attack("Sand-Attack", Attack.ElementalType.GROUND, Effect.Target.ENEMY, 0, 0, 0, 0, 1),
            new Attack("Tail Whip", Effect.Target.ENEMY, 0, 0, 3),
            new Attack("Water Sport", Attack.ElementalType.WATER, Effect.Target.ENEMY, 0, 3)};
        
        public static List<Item> AllItems = new List<Item>{             //The list that will contain every existing item of the game.
            new Item("Potion", 100, "Adds 10HP to a Sharpmon", 10),
            new Item("Sup Potion", 300, "Adds 50HP to a Sharpmon", 50),
            new Item("Hyp Potion", 300, "Adds 500HP to a Sharpmon", 500),
            new Item("Rare candy", 1500, "The Sharpmon levels up", 0, 0, 0, 0, 0, 0, 1),
            new Item("HP up", 1500, "The Sharpmon max health increases by 1", 0, 0, 0, 0, 0, 0, 0, 1),
            new Item("Protein", 1500, "The Sharpmon base attack increases by 1", 0, 1),
            new Item("Iron", 1500, "The Sharpmon base defense increases by 1", 0, 0, 1),
            new Item("Calcium", 1500, "The Sharpmon base dodge increases by 1", 0, 0, 0, 1),
            new Item("Zinc", 1500, "The Sharpmon base accucary increases by 1", 0, 0, 0, 0, 1),
            new Item("Carbos", 2000, "The Sharpmon base speed increases by 1", 0, 0, 0, 0, 0, 1)};
        
        public static List<Sharpmon> AllSharpmons = new List<Sharpmon>{ //The list that will contain every existing sharpmon of the game.
            //STARTERS
            new Sharpmon("Sharpmender", Sharpmon.ElementalType.FIRE, 10, 10, 2, 2, 1, 2, 1, Attack.GetAttack("Ember"), Attack.GetAttack("Grawl")),
            new Sharpmon("Sharpasaur", Sharpmon.ElementalType.GRASS, 9, 9, 1, 1, 3, 2, 2, Attack.GetAttack("Vine Whip"), Attack.GetAttack("Foliage")),
            new Sharpmon("Sharpitle", Sharpmon.ElementalType.WATER, 11, 11, 1, 2, 2, 1, 2, Attack.GetAttack("Water Gun"), Attack.GetAttack("Shell")),
            new Sharpmon("Sharpichu", Sharpmon.ElementalType.ELECTRIC, 11, 11, 1, 1, 2, 2, 2, Attack.GetAttack("Thundershock"), Attack.GetAttack("Tail Whip")),

            //COMMON SHARPMON
            new Sharpmon("Sharptata", Sharpmon.ElementalType.NORMAL, 11, 11, 1, 1, 1, 2, 1, Attack.GetAttack("Scratch"), Attack.GetAttack("Tail Whip")),
            new Sharpmon("Sharpeowth", Sharpmon.ElementalType.NORMAL, 10, 10, 2, 1, 1, 1, 2, Attack.GetAttack("Scratch"), Attack.GetAttack("Grawl")),
            new Sharpmon("Sharpduo", Sharpmon.ElementalType.NORMAL, 10, 10, 2, 1, 1, 2, 2, Attack.GetAttack("Peck"), Attack.GetAttack("Scratch")),
            new Sharpmon("Sharplypuff", Sharpmon.ElementalType.NORMAL, 12, 12, 2, 2, 2, 1, 1, Attack.GetAttack("False Swipe"), Attack.GetAttack("Foliage")),
            new Sharpmon("Sharpterpie", Sharpmon.ElementalType.BUG, 8, 8, 1, 1, 1, 1, 1, Attack.GetAttack("Pound"), Attack.GetAttack("String Shot")),
            new Sharpmon("Sharpeedle", Sharpmon.ElementalType.BUG, 8, 8, 1, 1, 1, 1, 1, Attack.GetAttack("Poison Sting"), Attack.GetAttack("String Shot")),
            new Sharpmon("Sharpangela", Sharpmon.ElementalType.GRASS, 10, 10, 2, 1, 2, 1, 2, Attack.GetAttack("Vine Whip"), Attack.GetAttack("False Swipe")),
            new Sharpmon("Sharprout", Sharpmon.ElementalType.GRASS, 11, 11, 3, 1, 1, 2, 1, Attack.GetAttack("Razor Leaf"), Attack.GetAttack("Pound")),
            new Sharpmon("Sharpoddish", Sharpmon.ElementalType.GRASS, 9, 9, 1, 2, 2, 1, 1, Attack.GetAttack("Scratch"), Attack.GetAttack("Foliage")),
            new Sharpmon("Sharpidgey", Sharpmon.ElementalType.FLYING, 10, 10, 2, 1, 1, 1, 3, Attack.GetAttack("Pound"), Attack.GetAttack("Sand-Attack")),
            new Sharpmon("Sharpearow", Sharpmon.ElementalType.FLYING, 11, 11, 3, 1, 1, 2, 1, Attack.GetAttack("Peck"), Attack.GetAttack("Grawl")),
            new Sharpmon("Sharpuubat", Sharpmon.ElementalType.FLYING, 9, 9, 3, 2, 3, 2, 1, Attack.GetAttack("Poison Sting"), Attack.GetAttack("Wing Attack")),
            new Sharpmon("Sharpshrew", Sharpmon.ElementalType.GROUND, 8, 8, 3, 1, 1, 3, 3, Attack.GetAttack("Scratch"), Attack.GetAttack("Shell")),
            new Sharpmon("Sharplett", Sharpmon.ElementalType.GROUND, 11, 11, 2, 2, 1, 1, 1, Attack.GetAttack("Scratch"), Attack.GetAttack("Sand-Attack")),
            new Sharpmon("Sharpodude", Sharpmon.ElementalType.GROUND, 12, 12, 1, 2, 1, 3, 1, Attack.GetAttack("Rock Smash"), Attack.GetAttack("Shell")),
            new Sharpmon("Sharpix", Sharpmon.ElementalType.FIRE, 8, 8, 3, 2, 2, 3, 3, Attack.GetAttack("Ember"), Attack.GetAttack("Tail Whip")),
            new Sharpmon("Sharpowlithe", Sharpmon.ElementalType.FIRE, 9, 9, 2, 1, 2, 2, 1, Attack.GetAttack("Scratch"), Attack.GetAttack("Ember")),
            new Sharpmon("Sharponyta", Sharpmon.ElementalType.FIRE, 11, 11, 2, 1, 2, 2, 1, Attack.GetAttack("Ember"), Attack.GetAttack("Haste")),
            new Sharpmon("Sharpbby", Sharpmon.ElementalType.WATER, 11, 11, 2, 3, 1, 2, 1, Attack.GetAttack("Scratch"), Attack.GetAttack("Shell")),
            new Sharpmon("Sharpyduck", Sharpmon.ElementalType.WATER, 11, 11, 1, 2, 1, 4, 1, Attack.GetAttack("Water Sport"), Attack.GetAttack("Scratch")),
            new Sharpmon("Sharporsea", Sharpmon.ElementalType.WATER, 10, 10, 2, 1, 1, 2, 2, Attack.GetAttack("Water Gun"), Attack.GetAttack("Pound")),
            new Sharpmon("Sharpankey", Sharpmon.ElementalType.FIGHTING, 11, 11, 3, 1, 1, 2, 1, Attack.GetAttack("Rock Smash"), Attack.GetAttack("Foliage")),
            new Sharpmon("Sharpchop", Sharpmon.ElementalType.FIGHTING, 11, 11, 3, 2, 1, 3, 2, Attack.GetAttack("Karate Chop"), Attack.GetAttack("Grawl")),
            new Sharpmon("Sharpnemite", Sharpmon.ElementalType.ELECTRIC, 9, 9, 2, 1, 1, 2, 2, Attack.GetAttack("Thundershock"), Attack.GetAttack("Pound")),
            new Sharpmon("Sharptrode", Sharpmon.ElementalType.ELECTRIC, 11, 11, 1, 3, 1, 1, 1, Attack.GetAttack("Pound"), Attack.GetAttack("Shell")),

            //ADVANCED SHARPMONS
            new Sharpmon("Sharpaskhan", Sharpmon.ElementalType.NORMAL, 14, 14, 3, 1, 3, 2, 2, Attack.GetAttack("Tri Attack"), Attack.GetAttack("Rock Smash")),
            new Sharpmon("Sharptong", Sharpmon.ElementalType.NORMAL, 15, 15, 2, 2, 2, 5, 2, Attack.GetAttack("Tri Attack"), Attack.GetAttack("Tail Whip")),
            new Sharpmon("Sharpbone", Sharpmon.ElementalType.GROUND, 10, 10, 2, 2, 2, 3, 2, Attack.GetAttack("Bonemerang"), Attack.GetAttack("Grawl")),
            new Sharpmon("Sharponix", Sharpmon.ElementalType.GROUND, 13, 13, 2, 4, 1, 1, 2, Attack.GetAttack("Rock Tomb"), Attack.GetAttack("Harden")),
            new Sharpmon("Sharprydhon", Sharpmon.ElementalType.GROUND, 18, 18, 5, 5, 2, 2, 2, Attack.GetAttack("Mega Horn"), Attack.GetAttack("Earthquake")),
            new Sharpmon("Sharpcyther", Sharpmon.ElementalType.BUG, 13, 13, 3, 2, 2, 2, 2, Attack.GetAttack("Fury Cutter"), Attack.GetAttack("Foliage")),
            new Sharpmon("Sharpinsir", Sharpmon.ElementalType.BUG, 14, 14, 2, 3, 2, 2, 1, Attack.GetAttack("Fury Cutter"), Attack.GetAttack("Shell")),
            new Sharpmon("Sharpzing", Sharpmon.ElementalType.BUG, 16, 16, 2, 3, 3, 2, 3, Attack.GetAttack("Sludge"), Attack.GetAttack("Pound")),
            new Sharpmon("Sharplume", Sharpmon.ElementalType.GRASS, 15, 15, 4, 2, 2, 1, 2, Attack.GetAttack("Petal Dance"), Attack.GetAttack("Foliage")),
            new Sharpmon("Sharpmonchan", Sharpmon.ElementalType.FIGHTING, 13, 13, 2, 2, 3, 3, 2, Attack.GetAttack("Jump Kick"), Attack.GetAttack("Grawl")),
            new Sharpmon("Sharpmonlee", Sharpmon.ElementalType.FIGHTING, 13, 13, 4, 1, 2, 4, 3, Attack.GetAttack("Focus Punch"), Attack.GetAttack("Foliage")),
            new Sharpmon("Sharpcanine", Sharpmon.ElementalType.FIRE, 15, 15, 4, 1, 2, 3, 4, Attack.GetAttack("Flamethrower"), Attack.GetAttack("Grawl")),
            new Sharpmon("Sharplareon", Sharpmon.ElementalType.FIRE, 15, 15, 4, 3, 3, 3, 2, Attack.GetAttack("Fire Blast"), Attack.GetAttack("Fire Dance")),
            new Sharpmon("Sharponite", Sharpmon.ElementalType.FLYING, 20, 20, 5, 3, 4, 3, 4, Attack.GetAttack("Hurricane"), Attack.GetAttack("Hyper Beam")),
            new Sharpmon("Sharpras", Sharpmon.ElementalType.WATER, 20, 20, 3, 5, 2, 2, 2, Attack.GetAttack("Ice Beam"), Attack.GetAttack("Water Gun")),
            new Sharpmon("Sharpmie", Sharpmon.ElementalType.WATER, 16, 16, 4, 5, 1, 1, 1, Attack.GetAttack("Ice Beam"), Attack.GetAttack("Shell")),
            new Sharpmon("Sharparados", Sharpmon.ElementalType.WATER, 17, 17, 5, 4, 2, 4, 1, Attack.GetAttack("Dive"), Attack.GetAttack("Shell")),
            new Sharpmon("Sharporeon", Sharpmon.ElementalType.WATER, 15, 15, 4, 3, 3, 3, 2, Attack.GetAttack("Hydro Cannon"), Attack.GetAttack("Water Dance")),
            new Sharpmon("Sharpolteon", Sharpmon.ElementalType.ELECTRIC, 15, 15, 4, 3, 3, 3, 2, Attack.GetAttack("Thunder"), Attack.GetAttack("Electric Dance")),
            new Sharpmon("Sharpraichu", Sharpmon.ElementalType.ELECTRIC, 13, 13, 3, 3, 3, 3, 4, Attack.GetAttack("Thunder Punch"), Attack.GetAttack("Electric Dance")),
            new Sharpmon("Sharplakazam", Sharpmon.ElementalType.PSYCHIC, 14, 14, 5, 2, 2, 5, 4, Attack.GetAttack("Psychic"), Attack.GetAttack("Disable")),

            //LEGENDARY SHARPMON
            new Sharpmon("Sharpdos", Sharpmon.ElementalType.ELECTRIC, 30, 30, 4, 4, 4, 6, 6, Attack.GetAttack("Thunder"), Attack.GetAttack("Aeroblast")),
            new Sharpmon("Sharpoltres", Sharpmon.ElementalType.FIRE, 30, 30, 6, 4, 6, 4, 4, Attack.GetAttack("Fire Blast"), Attack.GetAttack("Sky Attack")),
            new Sharpmon("Sharpcanine", Sharpmon.ElementalType.FIRE, 20, 20, 6, 3, 4, 5, 6, new Attack("Flare Blitz", Attack.ElementalType.FIRE, Effect.Target.ENEMY, 120), Attack.GetAttack("Thunder Fang")),
            new Sharpmon("Sharpticuno", Sharpmon.ElementalType.WATER, 30, 30, 4, 6, 6, 4, 4, Attack.GetAttack("Ice Beam"), Attack.GetAttack("Hydro Cannon")),
            new Sharpmon("Sharpew", Sharpmon.ElementalType.PSYCHIC, 35, 35, 6, 6, 6, 7, 6, Attack.GetAttack("Future Sight"), Attack.GetAttack("Cosmic Defense")),
            new Sharpmon("Sharpewtwo", Sharpmon.ElementalType.PSYCHIC, 35, 35, 7, 6, 6, 6, 6, Attack.GetAttack("Future Sight"), Attack.GetAttack("Cosmic Power"))};
        
        public static List<string> Badges = new List<string>{           //The list that will contain every existing badges of the game.
            "Boulder",
            "Cascade",
            "Thunder",
            "Rainbow",
            "Marsh",
            "Soul",
            "Volcano",
            "Earth",
            "The Elite Four"};
        
        public static List<string> Towns = new List<string>{            //The list that will contain every existing towns of the game.
            "Pewter City",
            "Cerulean City",
            "Vermillon City",
            "Celadon City",
            "Saffron City",
            "Fuschia City",
            "Cinnabar Island",
            "Viridian City",
            "the Indigo Plateau"
        };
        public static Dictionary<Sharpmon, int> ArenaSharpmons = new Dictionary<Sharpmon, int>{            //The list that will contain each pair of sharpmon/level per arena.
            {Sharpmon.CopySharpmon(Sharpmon.GetSharpmon("Sharponix", Sharpdex.AllSharpmons)), 14},
            {Sharpmon.CopySharpmon(Sharpmon.GetSharpmon("Sharpmie", Sharpdex.AllSharpmons)), 21},
            {Sharpmon.CopySharpmon(Sharpmon.GetSharpmon("Sharpraichu", Sharpdex.AllSharpmons)), 24},
            {Sharpmon.CopySharpmon(Sharpmon.GetSharpmon("Sharplume", Sharpdex.AllSharpmons)), 29},
            {Sharpmon.CopySharpmon(Sharpmon.GetSharpmon("Sharpzing", Sharpdex.AllSharpmons)), 43},
            {Sharpmon.CopySharpmon(Sharpmon.GetSharpmon("Sharplakazam", Sharpdex.AllSharpmons)), 43},
            {Sharpmon.CopySharpmon(Sharpmon.GetSharpmon("Sharpcanine", Sharpdex.AllSharpmons)), 47},
            {Sharpmon.CopySharpmon(Sharpmon.GetSharpmon("Sharprydhon", Sharpdex.AllSharpmons)), 50},
            {Sharpmon.CopySharpmon(Sharpmon.GetSharpmon("Sharponite", Sharpdex.AllSharpmons)), 62}
        };
        
    }
}
