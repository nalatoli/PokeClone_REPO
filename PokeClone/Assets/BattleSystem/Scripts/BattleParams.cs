using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleParams : MonoBehaviour
{
    public enum PokemonType
    {
        Null,
        Normal,
        Fire,
        Fighting,
        Water,
        Flying,
        Grass,
        Poison,
        Electric,
        Ground,
        Psychic,
        Rock,
        Ice,
        Bug,
        Dragon,
        Ghost,
        Dark,
        Steel,
        Fairy
    };

    public enum MoveType
    {
        Physical, Special, Status
    };

    public class Move
    {
        public int defaultPP;
        public static PokemonType pkmnType;
        public static MoveType moveType;
        public static void MoveEffect()
        {
            //This is static because individual Pokemon shouldn't have their own implementation of a move, but they should
            //have their own PP for it
        }
    }

    public class Tackle : Move
    {
        new public int maxPP, currentPP;
        new public static PokemonType pkmnType = PokemonType.Normal;
        new public static MoveType moveType = MoveType.Physical;
        new public static void MoveEffect()
        {
            //
        }
    }

    public class Pokemon
    {
        public string name;
        public Sprite pokemonSprite;

        //Battle relevant stats
        public int level;
        public int maxHP, currentHP;
        public int attack, attackStage;
        public int defense, defenseStage;
        public int specAttack, specAttackStage;
        public int specDefense, specDefenseStage;
        public int speed, speedStage;
        public int accuracyStage;
        public int evasionStage;

        public List<Move> moveset;
    }

    public class Trainer
    {
        public Sprite trainerSprite;
        public List<Pokemon> party;
    }

    public float[,] typeMatchups;


    // Start is called before the first frame update
    void Start()
    {
        Pokemon Eevee = new Pokemon();
        Pokemon Pikachu = new Pokemon();

        Eevee.name = "Eevee";
        Eevee.pokemonSprite = null;
        Eevee.level = 5;
        Eevee.maxHP = Eevee.currentHP = 20;
        Eevee.attack = 10;
        Eevee.attackStage = 0;
        Eevee.defense = 10;
        Eevee.defenseStage = 0;
        Eevee.specDefense = 10;
        Eevee.specDefenseStage = 0;
        Eevee.specAttack = 10;
        Eevee.specAttackStage = 0;
        Eevee.speed = 10;
        Eevee.speedStage = 0;
        Eevee.accuracyStage = 0;
        Eevee.evasionStage = 0;

        Pikachu.name = "Pikachu";
        Pikachu.pokemonSprite = null;
        Pikachu.level = 5;
        Pikachu.maxHP = Pikachu.currentHP = 20;
        Pikachu.attack = 11;
        Pikachu.attackStage = 0;
        Pikachu.defense = 10;
        Pikachu.defenseStage = 0;
        Pikachu.specDefense = 10;
        Pikachu.specDefenseStage = 0;
        Pikachu.specAttack = 10;
        Pikachu.specAttackStage = 0;
        Pikachu.speed = 9;
        Pikachu.speedStage = 0;
        Pikachu.accuracyStage = 0;
        Pikachu.evasionStage = 0;

        Trainer Red = new Trainer();
        Trainer Blue = new Trainer();

        Red.party.Add(Pikachu);
        Blue.party.Add(Eevee);


        //There are technically 17 types but Null is included for things that don't take type effectiveness into account
        //The first dimension of the matrix represents the attacker's type and the second the defender's
        typeMatchups = new float[18,18];
        //Matchups against Null
        typeMatchups[(int)PokemonType.Null, (int)PokemonType.Null] = 1;
        typeMatchups[(int)PokemonType.Null, (int)PokemonType.Normal] = 1;
        typeMatchups[(int)PokemonType.Null, (int)PokemonType.Fire] = 1;
        typeMatchups[(int)PokemonType.Null, (int)PokemonType.Fighting] = 1;
        typeMatchups[(int)PokemonType.Null, (int)PokemonType.Water] = 1;
        typeMatchups[(int)PokemonType.Null, (int)PokemonType.Flying] = 1;
        typeMatchups[(int)PokemonType.Null, (int)PokemonType.Grass] = 1;
        typeMatchups[(int)PokemonType.Null, (int)PokemonType.Poison] = 1;
        typeMatchups[(int)PokemonType.Null, (int)PokemonType.Electric] = 1;
        typeMatchups[(int)PokemonType.Null, (int)PokemonType.Ground] = 1;
        typeMatchups[(int)PokemonType.Null, (int)PokemonType.Psychic] = 1;
        typeMatchups[(int)PokemonType.Null, (int)PokemonType.Rock] = 1;
        typeMatchups[(int)PokemonType.Null, (int)PokemonType.Ice] = 1;
        typeMatchups[(int)PokemonType.Null, (int)PokemonType.Bug] = 1;
        typeMatchups[(int)PokemonType.Null, (int)PokemonType.Dragon] = 1;
        typeMatchups[(int)PokemonType.Null, (int)PokemonType.Ghost] = 1;
        typeMatchups[(int)PokemonType.Null, (int)PokemonType.Dark] = 1;
        typeMatchups[(int)PokemonType.Null, (int)PokemonType.Steel] = 1;
        typeMatchups[(int)PokemonType.Null, (int)PokemonType.Fairy] = 1;

        //Matchups against Normal
        typeMatchups[(int)PokemonType.Normal, (int)PokemonType.Null] = 1;
        typeMatchups[(int)PokemonType.Normal, (int)PokemonType.Normal] = 1;
        typeMatchups[(int)PokemonType.Normal, (int)PokemonType.Fire] = 1;
        typeMatchups[(int)PokemonType.Normal, (int)PokemonType.Fighting] = 1;
        typeMatchups[(int)PokemonType.Normal, (int)PokemonType.Water] = 1;
        typeMatchups[(int)PokemonType.Normal, (int)PokemonType.Flying] = 1;
        typeMatchups[(int)PokemonType.Normal, (int)PokemonType.Grass] = 1;
        typeMatchups[(int)PokemonType.Normal, (int)PokemonType.Poison] = 1;
        typeMatchups[(int)PokemonType.Normal, (int)PokemonType.Electric] = 1;
        typeMatchups[(int)PokemonType.Normal, (int)PokemonType.Ground] = 1;
        typeMatchups[(int)PokemonType.Normal, (int)PokemonType.Psychic] = 1;
        typeMatchups[(int)PokemonType.Normal, (int)PokemonType.Rock] = 0.5f;
        typeMatchups[(int)PokemonType.Normal, (int)PokemonType.Ice] = 1;
        typeMatchups[(int)PokemonType.Normal, (int)PokemonType.Bug] = 1;
        typeMatchups[(int)PokemonType.Normal, (int)PokemonType.Dragon] = 1;
        typeMatchups[(int)PokemonType.Normal, (int)PokemonType.Ghost] = 0;
        typeMatchups[(int)PokemonType.Normal, (int)PokemonType.Dark] = 1;
        typeMatchups[(int)PokemonType.Normal, (int)PokemonType.Steel] = 0.5f;
        typeMatchups[(int)PokemonType.Normal, (int)PokemonType.Fairy] = 1;

        //Matchups against Fire
        typeMatchups[(int)PokemonType.Fire, (int)PokemonType.Null] = 1;
        typeMatchups[(int)PokemonType.Fire, (int)PokemonType.Normal] = 1;
        typeMatchups[(int)PokemonType.Fire, (int)PokemonType.Fire] = 0.5f;
        typeMatchups[(int)PokemonType.Fire, (int)PokemonType.Fighting] = 1;
        typeMatchups[(int)PokemonType.Fire, (int)PokemonType.Water] = 0.5f;
        typeMatchups[(int)PokemonType.Fire, (int)PokemonType.Flying] = 1;
        typeMatchups[(int)PokemonType.Fire, (int)PokemonType.Grass] = 2;
        typeMatchups[(int)PokemonType.Fire, (int)PokemonType.Poison] = 1;
        typeMatchups[(int)PokemonType.Fire, (int)PokemonType.Electric] = 1;
        typeMatchups[(int)PokemonType.Fire, (int)PokemonType.Ground] = 1;
        typeMatchups[(int)PokemonType.Fire, (int)PokemonType.Psychic] = 1;
        typeMatchups[(int)PokemonType.Fire, (int)PokemonType.Rock] = 0.5f;
        typeMatchups[(int)PokemonType.Fire, (int)PokemonType.Ice] = 2;
        typeMatchups[(int)PokemonType.Fire, (int)PokemonType.Bug] = 2;
        typeMatchups[(int)PokemonType.Fire, (int)PokemonType.Dragon] = 0.5f;
        typeMatchups[(int)PokemonType.Fire, (int)PokemonType.Ghost] = 1;
        typeMatchups[(int)PokemonType.Fire, (int)PokemonType.Dark] = 1;
        typeMatchups[(int)PokemonType.Fire, (int)PokemonType.Steel] = 2;
        typeMatchups[(int)PokemonType.Fire, (int)PokemonType.Fairy] = 1;

        //Matchups against fighting
        typeMatchups[(int)PokemonType.Fighting, (int)PokemonType.Null] = 1;
        typeMatchups[(int)PokemonType.Fighting, (int)PokemonType.Normal] = 2;
        typeMatchups[(int)PokemonType.Fighting, (int)PokemonType.Fire] = 1;
        typeMatchups[(int)PokemonType.Fighting, (int)PokemonType.Fighting] = 1;
        typeMatchups[(int)PokemonType.Fighting, (int)PokemonType.Water] = 1;
        typeMatchups[(int)PokemonType.Fighting, (int)PokemonType.Flying] = 0.5f;
        typeMatchups[(int)PokemonType.Fighting, (int)PokemonType.Grass] = 1;
        typeMatchups[(int)PokemonType.Fighting, (int)PokemonType.Poison] = 0.5f;
        typeMatchups[(int)PokemonType.Fighting, (int)PokemonType.Electric] = 1;
        typeMatchups[(int)PokemonType.Fighting, (int)PokemonType.Ground] = 1;
        typeMatchups[(int)PokemonType.Fighting, (int)PokemonType.Psychic] = 0.5f;
        typeMatchups[(int)PokemonType.Fighting, (int)PokemonType.Rock] = 2;
        typeMatchups[(int)PokemonType.Fighting, (int)PokemonType.Ice] = 2;
        typeMatchups[(int)PokemonType.Fighting, (int)PokemonType.Bug] = 0.5f;
        typeMatchups[(int)PokemonType.Fighting, (int)PokemonType.Dragon] = 1;
        typeMatchups[(int)PokemonType.Fighting, (int)PokemonType.Ghost] = 0;
        typeMatchups[(int)PokemonType.Fighting, (int)PokemonType.Dark] = 2;
        typeMatchups[(int)PokemonType.Fighting, (int)PokemonType.Steel] = 2;
        typeMatchups[(int)PokemonType.Fighting, (int)PokemonType.Fairy] = 0.5f;

        //Matchups against Water
        typeMatchups[(int)PokemonType.Water, (int)PokemonType.Null] = 1;
        typeMatchups[(int)PokemonType.Water, (int)PokemonType.Normal] = 1;
        typeMatchups[(int)PokemonType.Water, (int)PokemonType.Fire] = 2;
        typeMatchups[(int)PokemonType.Water, (int)PokemonType.Fighting] = 1;
        typeMatchups[(int)PokemonType.Water, (int)PokemonType.Water] = 0.5f;
        typeMatchups[(int)PokemonType.Water, (int)PokemonType.Flying] = 1;
        typeMatchups[(int)PokemonType.Water, (int)PokemonType.Grass] = 0.5f;
        typeMatchups[(int)PokemonType.Water, (int)PokemonType.Poison] = 1;
        typeMatchups[(int)PokemonType.Water, (int)PokemonType.Electric] = 1;
        typeMatchups[(int)PokemonType.Water, (int)PokemonType.Ground] = 2;
        typeMatchups[(int)PokemonType.Water, (int)PokemonType.Psychic] = 1;
        typeMatchups[(int)PokemonType.Water, (int)PokemonType.Rock] = 2;
        typeMatchups[(int)PokemonType.Water, (int)PokemonType.Ice] = 1;
        typeMatchups[(int)PokemonType.Water, (int)PokemonType.Bug] = 1;
        typeMatchups[(int)PokemonType.Water, (int)PokemonType.Dragon] = 0.5f;
        typeMatchups[(int)PokemonType.Water, (int)PokemonType.Ghost] = 1;
        typeMatchups[(int)PokemonType.Water, (int)PokemonType.Dark] = 1;
        typeMatchups[(int)PokemonType.Water, (int)PokemonType.Steel] = 1;
        typeMatchups[(int)PokemonType.Water, (int)PokemonType.Fairy] = 1;

        //Matchups against Flying
        typeMatchups[(int)PokemonType.Flying, (int)PokemonType.Null] = 1;
        typeMatchups[(int)PokemonType.Flying, (int)PokemonType.Normal] = 1;
        typeMatchups[(int)PokemonType.Flying, (int)PokemonType.Fire] = 1;
        typeMatchups[(int)PokemonType.Flying, (int)PokemonType.Fighting] = 2;
        typeMatchups[(int)PokemonType.Flying, (int)PokemonType.Water] = 1;
        typeMatchups[(int)PokemonType.Flying, (int)PokemonType.Flying] = 1;
        typeMatchups[(int)PokemonType.Flying, (int)PokemonType.Grass] = 2;
        typeMatchups[(int)PokemonType.Flying, (int)PokemonType.Poison] = 1;
        typeMatchups[(int)PokemonType.Flying, (int)PokemonType.Electric] = 0.5f;
        typeMatchups[(int)PokemonType.Flying, (int)PokemonType.Ground] = 1;
        typeMatchups[(int)PokemonType.Flying, (int)PokemonType.Psychic] = 1;
        typeMatchups[(int)PokemonType.Flying, (int)PokemonType.Rock] = 0.5f;
        typeMatchups[(int)PokemonType.Flying, (int)PokemonType.Ice] = 1;
        typeMatchups[(int)PokemonType.Flying, (int)PokemonType.Bug] = 2;
        typeMatchups[(int)PokemonType.Flying, (int)PokemonType.Dragon] = 1;
        typeMatchups[(int)PokemonType.Flying, (int)PokemonType.Ghost] = 1;
        typeMatchups[(int)PokemonType.Flying, (int)PokemonType.Dark] = 1;
        typeMatchups[(int)PokemonType.Flying, (int)PokemonType.Steel] = 0.5f;
        typeMatchups[(int)PokemonType.Flying, (int)PokemonType.Fairy] = 1;

        //Matchups against Grass
        typeMatchups[(int)PokemonType.Grass, (int)PokemonType.Null] = 1;
        typeMatchups[(int)PokemonType.Grass, (int)PokemonType.Normal] = 1;
        typeMatchups[(int)PokemonType.Grass, (int)PokemonType.Fire] = 0.5f;
        typeMatchups[(int)PokemonType.Grass, (int)PokemonType.Fighting] = 1;
        typeMatchups[(int)PokemonType.Grass, (int)PokemonType.Water] = 2;
        typeMatchups[(int)PokemonType.Grass, (int)PokemonType.Flying] = 0.5f;
        typeMatchups[(int)PokemonType.Grass, (int)PokemonType.Grass] = 1;
        typeMatchups[(int)PokemonType.Grass, (int)PokemonType.Poison] = 0.5f;
        typeMatchups[(int)PokemonType.Grass, (int)PokemonType.Electric] = 1;
        typeMatchups[(int)PokemonType.Grass, (int)PokemonType.Ground] = 2;
        typeMatchups[(int)PokemonType.Grass, (int)PokemonType.Psychic] = 1;
        typeMatchups[(int)PokemonType.Grass, (int)PokemonType.Rock] = 2;
        typeMatchups[(int)PokemonType.Grass, (int)PokemonType.Ice] = 1;
        typeMatchups[(int)PokemonType.Grass, (int)PokemonType.Bug] = 0.5f;
        typeMatchups[(int)PokemonType.Grass, (int)PokemonType.Dragon] = 1;
        typeMatchups[(int)PokemonType.Grass, (int)PokemonType.Ghost] = 1;
        typeMatchups[(int)PokemonType.Grass, (int)PokemonType.Dark] = 1;
        typeMatchups[(int)PokemonType.Grass, (int)PokemonType.Steel] = 0.5f;
        typeMatchups[(int)PokemonType.Grass, (int)PokemonType.Fairy] = 1;

        //Matchups against Poison
        typeMatchups[(int)PokemonType.Poison, (int)PokemonType.Null] = 1;
        typeMatchups[(int)PokemonType.Poison, (int)PokemonType.Normal] = 1;
        typeMatchups[(int)PokemonType.Poison, (int)PokemonType.Fire] = 1;
        typeMatchups[(int)PokemonType.Poison, (int)PokemonType.Fighting] = 1;
        typeMatchups[(int)PokemonType.Poison, (int)PokemonType.Water] = 1;
        typeMatchups[(int)PokemonType.Poison, (int)PokemonType.Flying] = 1;
        typeMatchups[(int)PokemonType.Poison, (int)PokemonType.Grass] = 0.5f;
        typeMatchups[(int)PokemonType.Poison, (int)PokemonType.Poison] = 0.5f;
        typeMatchups[(int)PokemonType.Poison, (int)PokemonType.Electric] = 1;
        typeMatchups[(int)PokemonType.Poison, (int)PokemonType.Ground] = 0.5f;
        typeMatchups[(int)PokemonType.Poison, (int)PokemonType.Psychic] = 1;
        typeMatchups[(int)PokemonType.Poison, (int)PokemonType.Rock] = 0.5f;
        typeMatchups[(int)PokemonType.Poison, (int)PokemonType.Ice] = 1;
        typeMatchups[(int)PokemonType.Poison, (int)PokemonType.Bug] = 1;
        typeMatchups[(int)PokemonType.Poison, (int)PokemonType.Dragon] = 1;
        typeMatchups[(int)PokemonType.Poison, (int)PokemonType.Ghost] = 0.5f;
        typeMatchups[(int)PokemonType.Poison, (int)PokemonType.Dark] = 1;
        typeMatchups[(int)PokemonType.Poison, (int)PokemonType.Steel] = 0;
        typeMatchups[(int)PokemonType.Poison, (int)PokemonType.Fairy] = 2;

        //Matchups against Electric
        typeMatchups[(int)PokemonType.Electric, (int)PokemonType.Null] = 1;
        typeMatchups[(int)PokemonType.Electric, (int)PokemonType.Normal] = 1;
        typeMatchups[(int)PokemonType.Electric, (int)PokemonType.Fire] = 1;
        typeMatchups[(int)PokemonType.Electric, (int)PokemonType.Fighting] = 1;
        typeMatchups[(int)PokemonType.Electric, (int)PokemonType.Water] = 2;
        typeMatchups[(int)PokemonType.Electric, (int)PokemonType.Flying] = 2;
        typeMatchups[(int)PokemonType.Electric, (int)PokemonType.Grass] = 0.5f;
        typeMatchups[(int)PokemonType.Electric, (int)PokemonType.Poison] = 1;
        typeMatchups[(int)PokemonType.Electric, (int)PokemonType.Electric] = 0.5f;
        typeMatchups[(int)PokemonType.Electric, (int)PokemonType.Ground] = 0;
        typeMatchups[(int)PokemonType.Electric, (int)PokemonType.Psychic] = 1;
        typeMatchups[(int)PokemonType.Electric, (int)PokemonType.Rock] = 1;
        typeMatchups[(int)PokemonType.Electric, (int)PokemonType.Ice] = 1;
        typeMatchups[(int)PokemonType.Electric, (int)PokemonType.Bug] = 1;
        typeMatchups[(int)PokemonType.Electric, (int)PokemonType.Dragon] = 1;
        typeMatchups[(int)PokemonType.Electric, (int)PokemonType.Ghost] = 0.5f;
        typeMatchups[(int)PokemonType.Electric, (int)PokemonType.Dark] = 1;
        typeMatchups[(int)PokemonType.Electric, (int)PokemonType.Steel] = 1;
        typeMatchups[(int)PokemonType.Electric, (int)PokemonType.Fairy] = 1;

        //Matchups against Ground
        typeMatchups[(int)PokemonType.Ground, (int)PokemonType.Null] = 1;
        typeMatchups[(int)PokemonType.Ground, (int)PokemonType.Normal] = 1;
        typeMatchups[(int)PokemonType.Ground, (int)PokemonType.Fire] = 2;
        typeMatchups[(int)PokemonType.Ground, (int)PokemonType.Fighting] = 1;
        typeMatchups[(int)PokemonType.Ground, (int)PokemonType.Water] = 1;
        typeMatchups[(int)PokemonType.Ground, (int)PokemonType.Flying] = 0;
        typeMatchups[(int)PokemonType.Ground, (int)PokemonType.Grass] = 0.5f;
        typeMatchups[(int)PokemonType.Ground, (int)PokemonType.Poison] = 2;
        typeMatchups[(int)PokemonType.Ground, (int)PokemonType.Electric] = 2;
        typeMatchups[(int)PokemonType.Ground, (int)PokemonType.Ground] = 1;
        typeMatchups[(int)PokemonType.Ground, (int)PokemonType.Psychic] = 1;
        typeMatchups[(int)PokemonType.Ground, (int)PokemonType.Rock] = 2;
        typeMatchups[(int)PokemonType.Ground, (int)PokemonType.Ice] = 1;
        typeMatchups[(int)PokemonType.Ground, (int)PokemonType.Bug] = 0.5f;
        typeMatchups[(int)PokemonType.Ground, (int)PokemonType.Dragon] = 1;
        typeMatchups[(int)PokemonType.Ground, (int)PokemonType.Ghost] = 1;
        typeMatchups[(int)PokemonType.Ground, (int)PokemonType.Dark] = 1;
        typeMatchups[(int)PokemonType.Ground, (int)PokemonType.Steel] = 2;
        typeMatchups[(int)PokemonType.Ground, (int)PokemonType.Fairy] = 1;

        //Matchups against Psychic
        typeMatchups[(int)PokemonType.Psychic, (int)PokemonType.Null] = 1;
        typeMatchups[(int)PokemonType.Psychic, (int)PokemonType.Normal] = 1;
        typeMatchups[(int)PokemonType.Psychic, (int)PokemonType.Fire] = 1;
        typeMatchups[(int)PokemonType.Psychic, (int)PokemonType.Fighting] = 2;
        typeMatchups[(int)PokemonType.Psychic, (int)PokemonType.Water] = 1;
        typeMatchups[(int)PokemonType.Psychic, (int)PokemonType.Flying] = 1;
        typeMatchups[(int)PokemonType.Psychic, (int)PokemonType.Grass] = 1;
        typeMatchups[(int)PokemonType.Psychic, (int)PokemonType.Poison] = 2;
        typeMatchups[(int)PokemonType.Psychic, (int)PokemonType.Electric] = 1;
        typeMatchups[(int)PokemonType.Psychic, (int)PokemonType.Ground] = 1;
        typeMatchups[(int)PokemonType.Psychic, (int)PokemonType.Psychic] = 0.5f;
        typeMatchups[(int)PokemonType.Psychic, (int)PokemonType.Rock] = 1;
        typeMatchups[(int)PokemonType.Psychic, (int)PokemonType.Ice] = 1;
        typeMatchups[(int)PokemonType.Psychic, (int)PokemonType.Bug] = 1;
        typeMatchups[(int)PokemonType.Psychic, (int)PokemonType.Dragon] = 1;
        typeMatchups[(int)PokemonType.Psychic, (int)PokemonType.Ghost] = 1;
        typeMatchups[(int)PokemonType.Psychic, (int)PokemonType.Dark] = 0;
        typeMatchups[(int)PokemonType.Psychic, (int)PokemonType.Steel] = 0.5f;
        typeMatchups[(int)PokemonType.Psychic, (int)PokemonType.Fairy] = 1;

        //Matchups against Rock
        typeMatchups[(int)PokemonType.Rock, (int)PokemonType.Null] = 1;
        typeMatchups[(int)PokemonType.Rock, (int)PokemonType.Normal] = 1;
        typeMatchups[(int)PokemonType.Rock, (int)PokemonType.Fire] = 2;
        typeMatchups[(int)PokemonType.Rock, (int)PokemonType.Fighting] = 0.5f;
        typeMatchups[(int)PokemonType.Rock, (int)PokemonType.Water] = 1;
        typeMatchups[(int)PokemonType.Rock, (int)PokemonType.Flying] = 2;
        typeMatchups[(int)PokemonType.Rock, (int)PokemonType.Grass] = 1;
        typeMatchups[(int)PokemonType.Rock, (int)PokemonType.Poison] = 1;
        typeMatchups[(int)PokemonType.Rock, (int)PokemonType.Electric] = 1;
        typeMatchups[(int)PokemonType.Rock, (int)PokemonType.Ground] = 0.5f;
        typeMatchups[(int)PokemonType.Rock, (int)PokemonType.Psychic] = 1;
        typeMatchups[(int)PokemonType.Rock, (int)PokemonType.Rock] = 1;
        typeMatchups[(int)PokemonType.Rock, (int)PokemonType.Ice] = 2;
        typeMatchups[(int)PokemonType.Rock, (int)PokemonType.Bug] = 2;
        typeMatchups[(int)PokemonType.Rock, (int)PokemonType.Dragon] = 1;
        typeMatchups[(int)PokemonType.Rock, (int)PokemonType.Ghost] = 1;
        typeMatchups[(int)PokemonType.Rock, (int)PokemonType.Dark] = 1;
        typeMatchups[(int)PokemonType.Rock, (int)PokemonType.Steel] = 0.5f;
        typeMatchups[(int)PokemonType.Rock, (int)PokemonType.Fairy] = 1;

        //Matchups against Ice
        typeMatchups[(int)PokemonType.Ice, (int)PokemonType.Null] = 1;
        typeMatchups[(int)PokemonType.Ice, (int)PokemonType.Normal] = 1;
        typeMatchups[(int)PokemonType.Ice, (int)PokemonType.Fire] = 0.5f;
        typeMatchups[(int)PokemonType.Ice, (int)PokemonType.Fighting] = 1;
        typeMatchups[(int)PokemonType.Ice, (int)PokemonType.Water] = 0.5f;
        typeMatchups[(int)PokemonType.Ice, (int)PokemonType.Flying] = 2;
        typeMatchups[(int)PokemonType.Ice, (int)PokemonType.Grass] = 2;
        typeMatchups[(int)PokemonType.Ice, (int)PokemonType.Poison] = 1;
        typeMatchups[(int)PokemonType.Ice, (int)PokemonType.Electric] = 1;
        typeMatchups[(int)PokemonType.Ice, (int)PokemonType.Ground] = 2;
        typeMatchups[(int)PokemonType.Ice, (int)PokemonType.Psychic] = 1;
        typeMatchups[(int)PokemonType.Ice, (int)PokemonType.Rock] = 1;
        typeMatchups[(int)PokemonType.Ice, (int)PokemonType.Ice] = 0.5f;
        typeMatchups[(int)PokemonType.Ice, (int)PokemonType.Bug] = 1;
        typeMatchups[(int)PokemonType.Ice, (int)PokemonType.Dragon] = 2;
        typeMatchups[(int)PokemonType.Ice, (int)PokemonType.Ghost] = 1;
        typeMatchups[(int)PokemonType.Ice, (int)PokemonType.Dark] = 1;
        typeMatchups[(int)PokemonType.Ice, (int)PokemonType.Steel] = 0.5f;
        typeMatchups[(int)PokemonType.Ice, (int)PokemonType.Fairy] = 1;

        //Matchups against Bug
        typeMatchups[(int)PokemonType.Bug, (int)PokemonType.Null] = 1;
        typeMatchups[(int)PokemonType.Bug, (int)PokemonType.Normal] = 1;
        typeMatchups[(int)PokemonType.Bug, (int)PokemonType.Fire] = 0.5f;
        typeMatchups[(int)PokemonType.Bug, (int)PokemonType.Fighting] = 0.5f;
        typeMatchups[(int)PokemonType.Bug, (int)PokemonType.Water] = 1;
        typeMatchups[(int)PokemonType.Bug, (int)PokemonType.Flying] = 0.5f;
        typeMatchups[(int)PokemonType.Bug, (int)PokemonType.Grass] = 2;
        typeMatchups[(int)PokemonType.Bug, (int)PokemonType.Poison] = 0.5f;
        typeMatchups[(int)PokemonType.Bug, (int)PokemonType.Electric] = 1;
        typeMatchups[(int)PokemonType.Bug, (int)PokemonType.Ground] = 1;
        typeMatchups[(int)PokemonType.Bug, (int)PokemonType.Psychic] = 2;
        typeMatchups[(int)PokemonType.Bug, (int)PokemonType.Rock] = 1;
        typeMatchups[(int)PokemonType.Bug, (int)PokemonType.Ice] = 1;
        typeMatchups[(int)PokemonType.Bug, (int)PokemonType.Bug] = 1;
        typeMatchups[(int)PokemonType.Bug, (int)PokemonType.Dragon] = 1;
        typeMatchups[(int)PokemonType.Bug, (int)PokemonType.Ghost] = 0.5f;
        typeMatchups[(int)PokemonType.Bug, (int)PokemonType.Dark] = 2;
        typeMatchups[(int)PokemonType.Bug, (int)PokemonType.Steel] = 0.5f;
        typeMatchups[(int)PokemonType.Bug, (int)PokemonType.Fairy] = 0.5f;

        //Matchups against Dragon
        typeMatchups[(int)PokemonType.Dragon, (int)PokemonType.Null] = 1;
        typeMatchups[(int)PokemonType.Dragon, (int)PokemonType.Normal] = 1;
        typeMatchups[(int)PokemonType.Dragon, (int)PokemonType.Fire] = 1;
        typeMatchups[(int)PokemonType.Dragon, (int)PokemonType.Fighting] = 1;
        typeMatchups[(int)PokemonType.Dragon, (int)PokemonType.Water] = 1;
        typeMatchups[(int)PokemonType.Dragon, (int)PokemonType.Flying] = 1;
        typeMatchups[(int)PokemonType.Dragon, (int)PokemonType.Grass] = 1;
        typeMatchups[(int)PokemonType.Dragon, (int)PokemonType.Poison] = 1;
        typeMatchups[(int)PokemonType.Dragon, (int)PokemonType.Electric] = 1;
        typeMatchups[(int)PokemonType.Dragon, (int)PokemonType.Ground] = 1;
        typeMatchups[(int)PokemonType.Dragon, (int)PokemonType.Psychic] = 1;
        typeMatchups[(int)PokemonType.Dragon, (int)PokemonType.Rock] = 1;
        typeMatchups[(int)PokemonType.Dragon, (int)PokemonType.Ice] = 1;
        typeMatchups[(int)PokemonType.Dragon, (int)PokemonType.Bug] = 1;
        typeMatchups[(int)PokemonType.Dragon, (int)PokemonType.Dragon] = 2;
        typeMatchups[(int)PokemonType.Dragon, (int)PokemonType.Ghost] = 1;
        typeMatchups[(int)PokemonType.Dragon, (int)PokemonType.Dark] = 1;
        typeMatchups[(int)PokemonType.Dragon, (int)PokemonType.Steel] = 0.5f;
        typeMatchups[(int)PokemonType.Dragon, (int)PokemonType.Fairy] = 0;

        //Matchups against Ghost
        typeMatchups[(int)PokemonType.Ghost, (int)PokemonType.Null] = 1;
        typeMatchups[(int)PokemonType.Ghost, (int)PokemonType.Normal] = 0;
        typeMatchups[(int)PokemonType.Ghost, (int)PokemonType.Fire] = 1;
        typeMatchups[(int)PokemonType.Ghost, (int)PokemonType.Fighting] = 1;
        typeMatchups[(int)PokemonType.Ghost, (int)PokemonType.Water] = 1;
        typeMatchups[(int)PokemonType.Ghost, (int)PokemonType.Flying] = 1;
        typeMatchups[(int)PokemonType.Ghost, (int)PokemonType.Grass] = 1;
        typeMatchups[(int)PokemonType.Ghost, (int)PokemonType.Poison] = 1;
        typeMatchups[(int)PokemonType.Ghost, (int)PokemonType.Electric] = 1;
        typeMatchups[(int)PokemonType.Ghost, (int)PokemonType.Ground] = 1;
        typeMatchups[(int)PokemonType.Ghost, (int)PokemonType.Psychic] = 2;
        typeMatchups[(int)PokemonType.Ghost, (int)PokemonType.Rock] = 1;
        typeMatchups[(int)PokemonType.Ghost, (int)PokemonType.Ice] = 1;
        typeMatchups[(int)PokemonType.Ghost, (int)PokemonType.Bug] = 1;
        typeMatchups[(int)PokemonType.Ghost, (int)PokemonType.Dragon] = 2;
        typeMatchups[(int)PokemonType.Ghost, (int)PokemonType.Ghost] = 2;
        typeMatchups[(int)PokemonType.Ghost, (int)PokemonType.Dark] = 0.5f;
        typeMatchups[(int)PokemonType.Ghost, (int)PokemonType.Steel] = 1;
        typeMatchups[(int)PokemonType.Ghost, (int)PokemonType.Fairy] = 1;

        //Matchups against Dark
        typeMatchups[(int)PokemonType.Dark, (int)PokemonType.Null] = 1;
        typeMatchups[(int)PokemonType.Dark, (int)PokemonType.Normal] = 1;
        typeMatchups[(int)PokemonType.Dark, (int)PokemonType.Fire] = 1;
        typeMatchups[(int)PokemonType.Dark, (int)PokemonType.Fighting] = 0.5f;
        typeMatchups[(int)PokemonType.Dark, (int)PokemonType.Water] = 1;
        typeMatchups[(int)PokemonType.Dark, (int)PokemonType.Flying] = 1;
        typeMatchups[(int)PokemonType.Dark, (int)PokemonType.Grass] = 1;
        typeMatchups[(int)PokemonType.Dark, (int)PokemonType.Poison] = 1;
        typeMatchups[(int)PokemonType.Dark, (int)PokemonType.Electric] = 1;
        typeMatchups[(int)PokemonType.Dark, (int)PokemonType.Ground] = 1;
        typeMatchups[(int)PokemonType.Dark, (int)PokemonType.Psychic] = 2;
        typeMatchups[(int)PokemonType.Dark, (int)PokemonType.Rock] = 1;
        typeMatchups[(int)PokemonType.Dark, (int)PokemonType.Ice] = 1;
        typeMatchups[(int)PokemonType.Dark, (int)PokemonType.Bug] = 1;
        typeMatchups[(int)PokemonType.Dark, (int)PokemonType.Dragon] = 1;
        typeMatchups[(int)PokemonType.Dark, (int)PokemonType.Ghost] = 2;
        typeMatchups[(int)PokemonType.Dark, (int)PokemonType.Dark] = 0.5f;
        typeMatchups[(int)PokemonType.Dark, (int)PokemonType.Steel] = 1;
        typeMatchups[(int)PokemonType.Dark, (int)PokemonType.Fairy] = 0.5f;

        //Matchups against Steel
        typeMatchups[(int)PokemonType.Steel, (int)PokemonType.Null] = 1;
        typeMatchups[(int)PokemonType.Steel, (int)PokemonType.Normal] = 1;
        typeMatchups[(int)PokemonType.Steel, (int)PokemonType.Fire] = 0.5f;
        typeMatchups[(int)PokemonType.Steel, (int)PokemonType.Fighting] = 1;
        typeMatchups[(int)PokemonType.Steel, (int)PokemonType.Water] = 0.5f;
        typeMatchups[(int)PokemonType.Steel, (int)PokemonType.Flying] = 1;
        typeMatchups[(int)PokemonType.Steel, (int)PokemonType.Grass] = 1;
        typeMatchups[(int)PokemonType.Steel, (int)PokemonType.Poison] = 1;
        typeMatchups[(int)PokemonType.Steel, (int)PokemonType.Electric] = 0.5f;
        typeMatchups[(int)PokemonType.Steel, (int)PokemonType.Ground] = 1;
        typeMatchups[(int)PokemonType.Steel, (int)PokemonType.Psychic] = 1;
        typeMatchups[(int)PokemonType.Steel, (int)PokemonType.Rock] = 2;
        typeMatchups[(int)PokemonType.Steel, (int)PokemonType.Ice] = 2;
        typeMatchups[(int)PokemonType.Steel, (int)PokemonType.Bug] = 1;
        typeMatchups[(int)PokemonType.Steel, (int)PokemonType.Dragon] = 1;
        typeMatchups[(int)PokemonType.Steel, (int)PokemonType.Ghost] = 1;
        typeMatchups[(int)PokemonType.Steel, (int)PokemonType.Dark] = 1;
        typeMatchups[(int)PokemonType.Steel, (int)PokemonType.Steel] = 0.5f;
        typeMatchups[(int)PokemonType.Steel, (int)PokemonType.Fairy] = 2;

        //Matchups against Fairy
        typeMatchups[(int)PokemonType.Fairy, (int)PokemonType.Null] = 1;
        typeMatchups[(int)PokemonType.Fairy, (int)PokemonType.Normal] = 1;
        typeMatchups[(int)PokemonType.Fairy, (int)PokemonType.Fire] = 0.5f;
        typeMatchups[(int)PokemonType.Fairy, (int)PokemonType.Fighting] = 2;
        typeMatchups[(int)PokemonType.Fairy, (int)PokemonType.Water] = 1;
        typeMatchups[(int)PokemonType.Fairy, (int)PokemonType.Flying] = 1;
        typeMatchups[(int)PokemonType.Fairy, (int)PokemonType.Grass] = 1;
        typeMatchups[(int)PokemonType.Fairy, (int)PokemonType.Poison] = 0.5f;
        typeMatchups[(int)PokemonType.Fairy, (int)PokemonType.Electric] = 0.5f;
        typeMatchups[(int)PokemonType.Fairy, (int)PokemonType.Ground] = 1;
        typeMatchups[(int)PokemonType.Fairy, (int)PokemonType.Psychic] = 1;
        typeMatchups[(int)PokemonType.Fairy, (int)PokemonType.Rock] = 1;
        typeMatchups[(int)PokemonType.Fairy, (int)PokemonType.Ice] = 1;
        typeMatchups[(int)PokemonType.Fairy, (int)PokemonType.Bug] = 1;
        typeMatchups[(int)PokemonType.Fairy, (int)PokemonType.Dragon] = 2;
        typeMatchups[(int)PokemonType.Fairy, (int)PokemonType.Ghost] = 1;
        typeMatchups[(int)PokemonType.Fairy, (int)PokemonType.Dark] = 2;
        typeMatchups[(int)PokemonType.Fairy, (int)PokemonType.Steel] = 0.5f;
        typeMatchups[(int)PokemonType.Fairy, (int)PokemonType.Fairy] = 1;

    }

    public void BattleLoop()
    {
        Trainer player, opponent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
