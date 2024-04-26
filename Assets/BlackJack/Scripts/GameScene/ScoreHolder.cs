using TMPro;
using UnityEngine;

public class ScoreHolder : MonoBehaviour
{
    [SerializeField] private TMP_Text rankText;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text scoreText;

    public void SetData(string rank, string username, string score)
    {
        rankText.text = rank;
        nameText.text = username;
        scoreText.text = score;
    }
}