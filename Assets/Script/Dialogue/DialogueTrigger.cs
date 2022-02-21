using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogue;

    public MusicManager music;

    public GameObject player;
    public GameObject boss;
    public GameObject enemySpawner;

    private void Update()
    {
        if (dialogue.EndScene())
        {
            player.GetComponent<PlayerController>().enabled = true;
            boss.GetComponent<Enemy>().enabled = true;
            enemySpawner.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    private void InDialogue()
    {
        player.GetComponent<Animator>().Rebind();
        player.GetComponent<PlayerController>().enabled = false;
    }

    private void StartBossMusic()
    {
        music.PlaySong(music.bossSong);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogue.StartDialogue();
            InDialogue();
            StartBossMusic();
        }
    }
}
