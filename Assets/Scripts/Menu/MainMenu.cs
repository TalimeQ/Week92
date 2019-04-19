using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OneDirection.Game;

namespace OneDirection.UI
{ 
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        GameController gameController;
        [SerializeField]
        GameObject howTo;
        [SerializeField]
        GameObject credits;

        public void StartGame()
        {
            gameController.StartGame();
        }

        public void Credits()
        {
            this.gameObject.SetActive(false);
            credits.SetActive(true);
        }

        public void HowTo()
        {
            this.gameObject.SetActive(false);
            howTo.SetActive(true);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }

}