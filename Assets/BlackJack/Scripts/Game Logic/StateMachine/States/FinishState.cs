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
        if (AppData.gameType == GameType.AI)
        {
            var popContent = new PopContent("", "Do you want\nto <size=150><b>Replay</size></b> Game ?");
            var buttonContentA = new ButtonContent("No", OnExitGame);
            var buttonContentB = new ButtonContent("Yes", OnRestartGame);
            PopUpController.ShowPopUp(popContent, buttonContentA, buttonContentB);
        }
        else
        {
            var popContent = new PopContent("", "Go to\n <size=150><b>Main Menu</size></b>");
            //  var buttonContentA = new ButtonContent("No", OnExitGame);
            var buttonContentB = new ButtonContent("Yes", OnExitGame);
            PopUpController.ShowPopUp(popContent, buttonContentB,ButtonType.ButtonB);
        }
    }

    private void OnRestartGame()
    {
        PopUpController.ClosePopUp();
        blackJackManager.RestartGame();
    }

    private void OnExitGame()
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