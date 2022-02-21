using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NextLevelScript : MonoBehaviour
{
    public GameObject endGame;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0;
            endGame.SetActive(true);
        }
    }

    /*
    [Header("Events")]
    [Space]

    public UnityEvent NextLevel;

    private void Awake()
    {
        if (NextLevel == null)
            NextLevel = new UnityEvent();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            NextLevel.Invoke();
        }
    }*/
}
