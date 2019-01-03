using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonData : MonoBehaviour
{
    public Pokeball pokemonBallDisplay;
    public Trainer player;
    public Trainer opponent;

    public Sprite DetermineSpriteForBall(int pokemonStatus)
    {
        switch (pokemonStatus)
        {
            case 0:
                return pokemonBallDisplay.empty;
            case 1:
                return pokemonBallDisplay.healthy;
            case 2:
                return pokemonBallDisplay.fainted;
            default:
                return pokemonBallDisplay.status;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
