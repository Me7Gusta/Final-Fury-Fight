using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoUI : MonoBehaviour
{
    public GameObject goUI;

    private void Start()
    {
        goUI.SetActive(false);
    }

    public void ActivateGoUI()
    {
        goUI.GetComponent<Animator>().Rebind();
        goUI.SetActive(true);
    }

    public void DisableGoUI()
    {
        goUI.SetActive(false);
    }
}
