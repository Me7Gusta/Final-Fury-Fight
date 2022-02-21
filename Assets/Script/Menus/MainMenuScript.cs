using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    
    public GameObject menu;
    public GameObject controls;
    public GameObject credits;
    public GameObject settings;
    public Slider volSlider;
    private bool creditsActive = false;
    private bool contrlsActive = false;

    private void Update()
    {
        if ((creditsActive||contrlsActive)&&Input.GetMouseButtonDown(0))
        {
            GoBackMenu();
        }
    }

    public void StartGame()
    {
        GameManager.gm.SetMaxHealth();
        SceneManager.LoadScene("Cutscene");
    }

    public void GameControls()
    {
        menu.SetActive(false);
        controls.SetActive(true);
        contrlsActive = true;
    }

    public void GameCredits()
    {
        menu.SetActive(false);
        credits.SetActive(true);
        creditsActive = true;
    }

    public void GameSettings()
    {
        volSlider.value = SoundManager.soundM.GetVolume();
        menu.SetActive(false);
        settings.SetActive(true);
    }
    
    public void GoBackMenu()
    {
        menu.SetActive(true);
        controls.SetActive(false);
        credits.SetActive(false);
        settings.SetActive(false);
        contrlsActive = false;
        creditsActive = false;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }
}
