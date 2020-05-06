using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public Text textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public GameObject dialogCanvas;
    public bool isDialogFinished;

    void Start()
    {
        StartCoroutine(Type());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            NextSentence();
        }

    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            dialogCanvas.SetActive(false);
            textDisplay.text = "";
            isDialogFinished = true;
        }
    }




}
