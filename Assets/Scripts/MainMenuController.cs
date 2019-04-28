using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    private GameObject mainMenu;
    private GameObject creditMenu;

    public void Start()
    {
        mainMenu = gameObject.transform.Find("MainMenu").gameObject;
        creditMenu = gameObject.transform.Find("CreditMenu").gameObject;
    }

    /// <summary>
    /// Enabled the credit menu and hide the main menu.
    /// </summary>
    public void CreditEnabled()
    {
        creditMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    /// <summary>
    /// Disabled the credit menu and display the main menu.
    /// </summary>
    public void CreditDisabled()
    {
        mainMenu.SetActive(true);
        creditMenu.SetActive(false);
    }

    /// <summary>
    /// Quit function.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
