using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Animator animator;
    public Text nameText;
    public Text dialogueText;

    public float textSpeed = 2f;

    public Dialogue[] dialogue;

    private int index;
    private bool endDialog = false;

    private void Awake()
    {
        animator.SetBool("IsOpen", false);
    }

    public void StartDialogue()
    {
        index = 0;
        endDialog = false;
        animator.SetBool("IsOpen", true);
        DisplayNextSentence();
    }


    public void DisplayNextSentence()
    {

        if (index >= dialogue.Length)
        {
            EndDialogue();
            return;
        }

        Dialogue sentence = dialogue[index];
        nameText.text = sentence.name;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence.speech));
        index++;
        //Debug.Log(index);

    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = " ";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return textSpeed;
        }
    }

    public void EndDialogue()
    {
        //index = 0;
        animator.SetBool("IsOpen", false);
        endDialog = true;
        gameObject.SetActive(false);
    }

    public bool EndScene()
    {
        return endDialog;
    }
}
