using TMPro;
using UnityEngine;

public class Chat : MonoBehaviour
{
    [SerializeField] private TMP_Text chatText;
    public void SetText(string input)
    {
        chatText.text = input;
    }
}