using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiDialogues : MonoBehaviour
{
    public GameObject[] dialogue;

    public void NextSentence()
    {
        for (int i = 0; i < dialogue.Length; i++)
        {
            if (dialogue[i].activeInHierarchy)
            {
                dialogue[i].GetComponent<DialogueManager>().DisplayNextSentence();
            }
        }
    }
}
