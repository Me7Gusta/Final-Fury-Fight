﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public void ReloadScene()
    {
        Time.timeScale = 1f;
        GameManager.gm.SetHealth(GameManager.gm.GetMaxHealth());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
