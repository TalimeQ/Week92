using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField]
    GameObject mainMenu;

    public void Quit()
    {
        this.gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }
}
