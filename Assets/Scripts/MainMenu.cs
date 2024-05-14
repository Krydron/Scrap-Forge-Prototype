using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settings;
    [SerializeField] GameObject player;

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Settings()
    {
        //Hide Main Menu and show Swttings
        mainMenu.SetActive(false);
        settings.SetActive(true);
    }

    public void ToMainMenu()
    {
        //Hide settings and show Main Menu
        mainMenu.SetActive(true);
        settings.SetActive(false);
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ReturnToGame()
    {
        player.GetComponent<PlayerMovement>().TogglePause();
    }
}
