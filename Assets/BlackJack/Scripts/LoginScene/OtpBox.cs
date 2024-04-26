using TMPro;
using UnityEngine;

public class OtpBox : MonoBehaviour
{
    [SerializeField] private TMP_Text otpText;

    public void SetOtpText(string text)
    {
        otpText.text = text;
    }
}