using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NetworkPlayerUI : MonoBehaviour
{
    [SerializeField] private Image playerIconImage;
    [SerializeField] private TMP_Text playerNameText;

    public void SetData(Sprite icon,string playerName)
    {
        if (icon != null)
        {
            playerIconImage.sprite = icon;
        }
        playerNameText.text = playerName;
    }
}