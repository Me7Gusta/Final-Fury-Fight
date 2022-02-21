using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject instructions;
    public GameObject settings;

    public Slider volSlider;

    private bool inMenu = false;

    // Update is called once per frame
    private void Update()
    {
        //Debug.Log("Entrando");
        if (Input.GetButtonDown("Cancel"))
        {
            //Debug.Log("pausando");
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        inMenu = true;
    }

    public void Resume()
    {
        if (inMenu) {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
            inMenu = false;
        }
    }

    public void GameInstructions()
    {
        inMenu = false;
        pauseMenuUI.SetActive(false);
        instructions.SetActive(true);
    }

    public void GameSettings()
    {
        inMenu = false;
        volSlider.value = SoundManager.soundM.GetVolume();
        pauseMenuUI.SetActive(false);
        settings.SetActive(true);
    }

    public void GoBackMenu()
    {
        inMenu = true;
        pauseMenuUI.SetActive(true);
        instructions.SetActive(false);
        settings.SetActive(false);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        //Debug.Log("Quitting game");
        Application.Quit();
    }
}
