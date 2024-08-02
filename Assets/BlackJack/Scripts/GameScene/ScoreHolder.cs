using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHolder : MonoBehaviour
{
    [SerializeField] private TMP_Text rankText;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Image bgImage;
    [SerializeField] private Color evenColour;
    [SerializeField] private Color oddColour;

    private void OnValidate()
    {
        if (bgImage == null)
        {
            bgImage = GetComponent<Image>();
        }
    }

    public void SetData(int rank, string username, string score)
    {
        bgImage.color = rank % 2 == 0 ? evenColour : oddColour;
        rankText.text = rank.ToString();
        nameText.text = username;
        scoreText.text = score;
    }
}