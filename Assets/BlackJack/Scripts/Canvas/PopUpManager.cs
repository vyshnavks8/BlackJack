using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] private PopUpCanvas popUpPanel;
    private static PopUpManager popUpManager;
    private void Awake()
    {
        if (popUpManager == null)
        {
            popUpManager = this;
            DontDestroyOnLoad(this);
        }
        else if (popUpManager != null)
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        PopUpController.OnShowPopUpDual += ShowPopUpDual;
        PopUpController.OnShowPopUpDualTimed += ShowPopUpDualTimed;
        PopUpController.OnShowPopUpSingle += ShowPopUpSingle;
        PopUpController.OnShowPopUpNoButton += OnShowPopUpNoButton;
        PopUpController.OnClosePopUp += OnClosePopUp;
    }


    private void OnDisable()
    {
        PopUpController.OnShowPopUpDual -= ShowPopUpDual;
        PopUpController.OnShowPopUpDualTimed -= ShowPopUpDualTimed;
        PopUpController.OnShowPopUpSingle -= ShowPopUpSingle;
        PopUpController.OnShowPopUpNoButton -= OnShowPopUpNoButton;
        PopUpController.OnClosePopUp -= OnClosePopUp;
    }

       


    private void OnClosePopUp()
    {
        popUpPanel.ClosePop();
    }
        

    private void OnShowPopUpNoButton(PopContent popContent)
    {
        var heading = popContent.Heading;
        var data = popContent.Data;
        popUpPanel.ShowPop(heading, data);
    }

    private void ShowPopUpSingle(PopContent popContent, ButtonContent buttonContent)
    {
        var heading = popContent.Heading;
        var data = popContent.Data;
        var b1 = buttonContent.ButtonText;
        var c1 = buttonContent.ButtonCallback;
        popUpPanel.ShowPop(heading, data, b1, c1);
    }



 
    private void ShowPopUpDual(PopContent popContent, ButtonContent buttonContentA, ButtonContent buttonContentB)
    {
        var heading = popContent.Heading;
        var data = popContent.Data;
        var b1 = buttonContentA.ButtonText;
        var c1 = buttonContentA.ButtonCallback;
        var b2 = buttonContentB.ButtonText;
        var c2 = buttonContentB.ButtonCallback;
        popUpPanel.ShowPop(heading, data, b1, c1, b2, c2);
    }

    private void ShowPopUpDualTimed(PopContent popContent, ButtonContent buttonContentA, ButtonContent buttonContentB, TimedContent timedContent)
    {
        var heading = popContent.Heading;
        var data = popContent.Data;
        var b1 = buttonContentA.ButtonText;
        var c1 = buttonContentA.ButtonCallback;
        var b2 = buttonContentB.ButtonText;
        var c2 = buttonContentB.ButtonCallback;
        var t1 = timedContent.Time;
        var c3 = timedContent.TimedCallback;
        popUpPanel.ShowPop(heading, data, b1, c1, b2, c2, t1, c3);
    }
}