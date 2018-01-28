using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MenuController : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject CreditsMenu;
    public GameObject PlayMenu;
    public NetworkManagerHUD ManagerHud;

    void Start()
    {
        CreditsMenu.SetActive(false);
        MainMenu.SetActive(true);
        PlayMenu.SetActive(false);
        ManagerHud.enabled = false;
    }

    public void PlayPressed()
    {
        CreditsMenu.SetActive(false);
        MainMenu.SetActive(false);
        PlayMenu.SetActive(true);
        ManagerHud.enabled = true;
    }

    public void CreditsPressed()
    {
        CreditsMenu.SetActive(true);
        MainMenu.SetActive(false);
        PlayMenu.SetActive(false);
        ManagerHud.enabled = false;
    }

    public void ExitPressed()
    {
        Application.Quit();
    }

    public void MenuPressed()
    {
        CreditsMenu.SetActive(false);
        MainMenu.SetActive(true);
        PlayMenu.SetActive(false);
        ManagerHud.enabled = false;
    }
}
