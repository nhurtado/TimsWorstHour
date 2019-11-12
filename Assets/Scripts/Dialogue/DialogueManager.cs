using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text sentenceText;
    public Animator boxAnimator;
    public Animator charAnimator;

    Queue<string> sentences;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        
    }

    public void StartDialogue(Dialogue dialogue) {
        boxAnimator.SetBool("IsOpen", true);
        charAnimator.SetBool("IsOpen", true);

        sentences.Clear();

        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        nameText.text = dialogue.name;
        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        sentenceText.text = sentence;
    }

    void EndDialogue() {
        boxAnimator.SetBool("IsOpen", false);
        charAnimator.SetBool("IsOpen", false);
    }
}
