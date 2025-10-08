using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{
    [SerializeField] private Image chatIcon;
    [SerializeField] private TMP_Text chatText;
    public void SetText(Sprite icon,string input)
    {
        chatIcon.sprite = icon;
        chatText.text = input;
    }
}