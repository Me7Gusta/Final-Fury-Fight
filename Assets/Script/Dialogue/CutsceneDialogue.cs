using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneDialogue : MonoBehaviour
{
    public SceneManagerScript sceneManager;
    public DialogueManager dialogue;
    public GameObject[] images;

    private int index = 0;
    private bool unavez = true;

    private void Awake()
    {
        if(sceneManager == null)
        {
            sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();
        }
        //dialogue = GetComponent<DialogueManager>();
    }

    private void Start()
    {
        dialogue.StartDialogue();
    }

    private void FixedUpdate()
    {
        if (dialogue.EndScene() && unavez)
        {
            sceneManager.GoToLevel1();
            unavez = false;
        }
    }

    public void ChangeImage()
    {
        index++;
        images[index - 1].SetActive(false);

        if (index < images.Length)
        {
            images[index].SetActive(true);
        }
    }
}
