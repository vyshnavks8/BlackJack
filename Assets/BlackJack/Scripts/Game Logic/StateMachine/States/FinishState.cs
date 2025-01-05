using System.Collections;
using UnityEngine;

public class FinishState : BlackJackState
{
    [SerializeField] private BlackJackManager blackJackManager;
    private IEnumerator waitToFinish;
    private const int waitTime = 5;

    public override void EnterState()
    {
        waitToFinish = Wait();
        StartCoroutine(waitToFinish);

    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        var popContent = new PopContent("", "Do you want\nto <size=90><b>Replay</size></b> Game ?");
        var buttonContentA = new ButtonContent("No", OnClickNo);
        var buttonContentB = new ButtonContent("Yes", OnClickYes);
        PopUpController.ShowPopUp(popContent, buttonContentA, buttonContentB);
        
    }

    private void OnClickYes()
    {
        PopUpController.ClosePopUp();
        blackJackManager.RestartGame();
    }

    private void OnClickNo()
    {
        PopUpController.ClosePopUp();
        blackJackManager.ExitGame();
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
    }
}