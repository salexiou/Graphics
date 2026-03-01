using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    private List<string> sentences;
    private List<string> sentenceHistory; 
    private int currentIndex = -1; 
    public bool dialogueEnded = false;

    void Start()
    {
        sentences = new List<string>();
        sentenceHistory = new List<string>(); 
    }

    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.name;
        sentences.Clear();
        sentenceHistory.Clear(); 

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Add(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (currentIndex < sentences.Count - 1)
        {
            currentIndex++; 
            dialogueText.text = sentences[currentIndex]; 
            sentenceHistory.Add(sentences[currentIndex]); 
        }
        else
        {
            dialogueEnded = true;
            EndDialogue();
        }
    }

    public void DisplayPreviousSentence()
    {
        if (currentIndex > 0)
        {
            currentIndex--; 
            dialogueText.text = sentenceHistory[currentIndex]; 
        }
    }

    public void EndDialogue()
    {
        gameObject.SetActive(false);
    }
    
}
