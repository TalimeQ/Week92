using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OneDirection.Game;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameController gamecont;


    public void Resume()
    {
        gamecont.Unpause();
        this.gameObject.SetActive(false);
    }

    public void Retry()
    {
        gamecont.Retry();
        this.gameObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
