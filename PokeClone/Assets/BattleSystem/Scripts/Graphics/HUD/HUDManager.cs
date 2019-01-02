using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    //The pokemon which are in play
    public Pokemon activePlayerPokemon;
    public Pokemon activeOpponentPokemon;

    //A Text component which will display each active mon's name
    public Text playerPokemonName;
    public Text opponentPokemonName;

    public Text playerPokemonCurrentHealth;

    public Text playerPokemonMaxHealth;

    public Text playerPokemonLevel;
    public Text opponentPokemonLevel;

    public HealthBar playerPokemonHealthBar;
    public HealthBar opponentPokemonHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //HP values are integers but HP bar only accepts fractional values
        float playerHealthRatio = (float)activePlayerPokemon.currentHP / (float)activePlayerPokemon.maxHP;
        float enemyHealthRatio = (float)activeOpponentPokemon.currentHP / (float)activeOpponentPokemon.maxHP;

        playerPokemonHealthBar.SetFullness(playerHealthRatio);
        opponentPokemonHealthBar.SetFullness(enemyHealthRatio);

        playerPokemonName.text = activePlayerPokemon.nickname;
        opponentPokemonName.text = activeOpponentPokemon.nickname;

        playerPokemonCurrentHealth.text = activePlayerPokemon.currentHP.ToString();

        playerPokemonMaxHealth.text = activePlayerPokemon.maxHP.ToString();

        playerPokemonLevel.text = activePlayerPokemon.level.ToString();
        opponentPokemonLevel.text = activeOpponentPokemon.level.ToString();

    }
}
