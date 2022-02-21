using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public Slider playerSlider;

    public GameObject enemyUI;
    public Slider enemySlider;
    public Image enemyImage;
    public Text enemyName;

    private float enemyUITimer = 3f;
    private float timer;
    private bool enemyHit = false;

    private void Start()
    {
        enemyUI.SetActive(false);
    }

    private void Update()
    {
        if (enemyHit)
        {
            timer += Time.deltaTime;

            if (timer > enemyUITimer)
            {
                enemyHit = false;
                enemyUI.SetActive(false);
            }
        }
    }

    public void SetMaxHealth(int health)
    {
        playerSlider.maxValue = health;
        playerSlider.value = health;
    }

    public void SetHealth(int health)
    {
        playerSlider.value = health;
    }

    public void UpdateEnemyUI(string name, int maxHealth, int currentHealth, Sprite image)
    {
        enemySlider.maxValue = maxHealth;
        enemySlider.value = currentHealth;
        enemyName.text = name;
        enemyImage.sprite = image;
        enemyHit = true;
        timer = 0;

        enemyUI.SetActive(true);
    }

}
