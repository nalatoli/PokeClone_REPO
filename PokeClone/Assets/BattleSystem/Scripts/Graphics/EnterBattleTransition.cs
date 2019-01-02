using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBattleTransition : MonoBehaviour
{
    public ScreenFade screenFade;
    public int numScreenFades;

    public float trainerEnterDelay;
    public MoveSprite playerEnter;
    public MoveSprite opponentEnter;

    public float partyBarEnterDelay;
    public MoveSprite playerPartyBarEnter;
    public MoveSprite opponentPartyBarEnter;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("CR_RepeatFade");
        StartCoroutine("CR_EnterTrainers");
        StartCoroutine("CR_EnterPartyBar");
    }

    public IEnumerator CR_EnterTrainers()
    {
        yield return new WaitForSeconds(trainerEnterDelay);
        playerEnter.Move();
        opponentEnter.Move();
    }

    public IEnumerator CR_EnterPartyBar()
    {
        yield return new WaitForSeconds(partyBarEnterDelay);
        playerPartyBarEnter.Move();
        opponentPartyBarEnter.Move();
    }

    public IEnumerator CR_RepeatFade()
    {
        for(int i=0; i<numScreenFades; i++)
        {
            yield return new WaitUntil(() => !screenFade.isFading);
            screenFade.Fade();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
