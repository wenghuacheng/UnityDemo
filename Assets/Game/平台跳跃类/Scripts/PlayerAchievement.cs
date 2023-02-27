using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAchievement : MonoBehaviour
{
    private int Score { get; set; }

    public Text ScoreText;

    public void SetScore(int score)
    {
        this.Score += score;
        this.ScoreText.text = this.Score.ToString();
    }
}
