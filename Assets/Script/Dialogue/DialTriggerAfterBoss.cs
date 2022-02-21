using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialTriggerAfterBoss : MonoBehaviour
{
    public GameObject player;
    public DialogueManager dialogue;

    private void Update()
    {
        if (dialogue.EndScene())
        {
            player.GetComponent<PlayerController>().enabled = true;
            this.gameObject.SetActive(false);
        }
    }

    public void StartDialogue()
    {
        player.GetComponent<Animator>().Rebind();
        player.GetComponent<PlayerController>().enabled = false;
        dialogue.StartDialogue();
    }
}
