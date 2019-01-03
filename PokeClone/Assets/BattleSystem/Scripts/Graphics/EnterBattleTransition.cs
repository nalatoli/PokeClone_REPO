using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBattleTransition : MonoBehaviour
{
    public PokemonData battleInfo;

    public ScreenFade screenFade;
    public int numScreenFades;

    public float trainerEnterDelay;
    public float playerLeaveDelay;
    public float opponentLeaveDelay;

    public MoveSprite player;
    public MoveSprite opponent;

    public float partyBarEnterDelay;
    public float playerPartyBarLeaveDelay;
    public float opponentPartyBarLeaveDelay;

    public MoveSprite playerPartyBar;
    public MoveSprite opponentPartyBar;

    public float playerInfoEnterDelay;
    public float opponentInfoEnterDelay;

    public MoveSprite playerInfo;
    public MoveSprite opponentInfo;

    public float ballsEnterDelay;
    public float playerBallsLeaveDelay;
    public float opponentBallsLeaveDelay;

    public float delayBetweenBalls;
    public MoveSprite[] playerBalls = new MoveSprite[6];
    public MoveSprite[] opponentBalls = new MoveSprite[6];

    // Start is called before the first frame update
    void Start()
    {
        BeginTransition();
    }

    public IEnumerator CR_EnterTrainers()
    {
        yield return new WaitForSeconds(trainerEnterDelay);
        player.Move();
        opponent.Move();
    }

    public IEnumerator CR_EnterPartyBar()
    {
        yield return new WaitForSeconds(partyBarEnterDelay);
        playerPartyBar.Move();
        opponentPartyBar.Move();
    }

    public IEnumerator CR_EnterBalls()
    {
        yield return new WaitForSeconds(ballsEnterDelay);
        for(int i=0; i<6; i++)
        {
            opponentBalls[i].Move();
            playerBalls[i].Move();
            yield return new WaitForSeconds(delayBetweenBalls);
        }
    }

    public IEnumerator CR_EnterPlayerPokemonInfo()
    {
        yield return new WaitForSeconds(playerInfoEnterDelay);
        playerInfo.Move();
    }

    public IEnumerator CR_EnterOpponentPokemonInfo()
    {
        yield return new WaitForSeconds(opponentInfoEnterDelay);
        opponentInfo.Move();
    }

    public IEnumerator CR_LeavePlayerPartyBar()
    {
        yield return new WaitForSeconds(playerPartyBarLeaveDelay);

        playerPartyBar.GetComponent<ScreenFade>().Fade();
        playerPartyBar.destination = new Vector3(-300,
                                                 playerPartyBar.transform.position.y,
                                                 playerPartyBar.transform.position.z);
        playerPartyBar.speed = 200;
        playerPartyBar.Move();
        yield return new WaitUntil(() => !playerPartyBar.GetComponent<ScreenFade>().isFading);
        Destroy(playerPartyBar.GetComponent<ScreenFade>());
    }

    public IEnumerator CR_LeaveOpponentPartyBar()
    {
        yield return new WaitForSeconds(opponentPartyBarLeaveDelay);

        opponentPartyBar.GetComponent<ScreenFade>().Fade();
        opponentPartyBar.destination = new Vector3(300,
                                                 opponentPartyBar.transform.position.y,
                                                 opponentPartyBar.transform.position.z);
        opponentPartyBar.speed = 200;
        opponentPartyBar.Move();
        yield return new WaitUntil(() => !opponentPartyBar.GetComponent<ScreenFade>().isFading);
        Destroy(opponentPartyBar.GetComponent<ScreenFade>());
    }

    public IEnumerator CR_LeavePlayerBalls()
    {
        yield return new WaitForSeconds(playerBallsLeaveDelay);
        for(int i=0; i<6; i++)
        {
            playerBalls[i].destination = new Vector3(-300,
                                                     playerBalls[i].transform.position.y,
                                                     playerBalls[i].transform.position.z);
            playerBalls[i].Move();
            playerBalls[i].GetComponent<ScreenFade>().Fade();
            yield return new WaitForSeconds(delayBetweenBalls);
        }
    }

    public IEnumerator CR_LeaveOpponentBalls()
    {
        yield return new WaitForSeconds(opponentBallsLeaveDelay);
        for (int i = 0; i < 6; i++)
        {
            opponentBalls[i].destination = new Vector3(300,
                                                     opponentBalls[i].transform.position.y,
                                                     opponentBalls[i].transform.position.z);
            opponentBalls[i].Move();
            opponentBalls[i].GetComponent<ScreenFade>().Fade();
            yield return new WaitForSeconds(delayBetweenBalls);
        }
    }

    public IEnumerator CR_LeavePlayer()
    {
        yield return new WaitForSeconds(playerLeaveDelay);
        player.destination = new Vector3(-300,
                                         player.transform.position.y,
                                         player.transform.position.z);
        player.speed = 50;
        player.Move();
        player.GetComponent<Animator>().Play("ThrowBall");
    }

    public IEnumerator CR_LeaveOpponent()
    {
        yield return new WaitForSeconds(opponentLeaveDelay);
        opponent.destination = new Vector3(300,
                                         opponent.transform.position.y,
                                         opponent.transform.position.z);
        opponent.Move();
    }

    public IEnumerator CR_RepeatFade()
    {
        for(int i=0; i<numScreenFades; i++)
        {
            yield return new WaitUntil(() => !screenFade.isFading);
            screenFade.Fade();
        }
    }

    public void BeginTransition()
    {
        //Initially set HUD icon for pokeball to be empty
        for(int i=0; i<6; i++)
        {
            playerBalls[i].GetComponent<SpriteRenderer>().sprite = battleInfo.DetermineSpriteForBall(0);
            opponentBalls[i].GetComponent<SpriteRenderer>().sprite = battleInfo.DetermineSpriteForBall(0);
        }
        //Determine status of each Pokemon and change each icon appropriately
        //Make sure we don't try to find the status of non-existent Pokemon or exceed the bounds of party array
        //First for player
        for(int i=0; i<battleInfo.player.party.Length; i++)
        {
            if (battleInfo.player.party[i] == null)
                break;
            playerBalls[i].GetComponent<SpriteRenderer>().sprite = battleInfo.DetermineSpriteForBall(battleInfo.player.party[i].status);
        }

        //Then opponent
        for (int i = 0; i < battleInfo.opponent.party.Length; i++)
        {
            if (battleInfo.opponent.party[i] == null)
                break;
            opponentBalls[i].GetComponent<SpriteRenderer>().sprite = battleInfo.DetermineSpriteForBall(battleInfo.opponent.party[i].status);
        }

        StartCoroutine("CR_RepeatFade");
        StartCoroutine("CR_EnterTrainers");
        StartCoroutine("CR_EnterPartyBar");
        StartCoroutine("CR_EnterBalls");
        StartCoroutine("CR_EnterOpponentPokemonInfo");
        StartCoroutine("CR_EnterPlayerPokemonInfo");
        StartCoroutine("CR_LeavePlayerPartyBar");
        StartCoroutine("CR_LeaveOpponentPartyBar");
        StartCoroutine("CR_LeavePlayerBalls");
        StartCoroutine("CR_LeaveOpponentBalls");
        StartCoroutine("CR_LeavePlayer");
        StartCoroutine("CR_LeaveOpponent");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
