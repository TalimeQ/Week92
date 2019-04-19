using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameUi : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreText;
    [SerializeField]
    TextMeshProUGUI livesText;

    public void UpdateScore(int newScore)
    {
        scoreText.text = "Score: " + newScore;
    }

    public void UpdateLives(int newLives)
    {
        livesText.text = "Lives: " + newLives;
    }
}
