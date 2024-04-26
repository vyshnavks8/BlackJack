using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OtpFieldController : MonoBehaviour
{
    [SerializeField] private TMP_InputField.ContentType contentType = TMP_InputField.ContentType.IntegerNumber;
    [SerializeField] private TMP_InputField otpInput;
    [SerializeField] private List<OtpBox> otpBox = new();
    private string otp;
    public string Otp => otp;

    private void Awake()
    {
        otpInput.contentType = contentType;
        otpInput.characterLimit = otpBox.Count;
    }

    private void OnEnable()
    {
        otpInput.onValueChanged.AddListener(OnOtpSet);
    }


    private void OnDisable()
    {
        otpInput.onValueChanged.RemoveListener(OnOtpSet);
    }

    private void OnOtpSet(string input)
    {
        otp = input;
        for (var index = 0; index < otpBox.Count; index++)
        {
            var box = otpBox[index];
            box.SetOtpText(index < input.Length ? input[index].ToString() : string.Empty);
        }
    }
}