using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OneDirection.Game;
using TMPro;

public class FinishedMenu : MonoBehaviour
{
    int displayScore = 0;
    [SerializeField]
    TextMeshProUGUI scoreText;
    [SerializeField]
    GameController gameController;
    [SerializeField]
    GameObject gameMenu;


    private void Awake()
    {
        scoreText.SetText(""+ displayScore);
    }
    
    public void SetScore(int score)
    {
        displayScore = score;
    }

    public void Restart()
    {
        gameController.StartGame();
    }

    public void ReturnToMenu()
    {
        this.gameObject.SetActive(false);
        gameMenu.SetActive(true);

    }

    public void Quit()
    {
        Application.Quit();

    }
}
