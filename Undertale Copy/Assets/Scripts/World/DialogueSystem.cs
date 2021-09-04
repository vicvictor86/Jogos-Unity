using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [Header("Settings")] public float typingSpeed;
    private float typingSpeedDefault;
    private string[] sentences;
    private int index;

    public GameObject dialogueSpace;
    private Sprite spriteActual;
    private string[] txt;

    public GameObject screenActual;
    public bool changeSceneDialogueToBattle;
    private void Start()
    {
        typingSpeedDefault = typingSpeed;
        changeSceneDialogueToBattle = true;
    }

    private void SearchKeyWords()
    {
        for (int i = 0; i < txt.Length - 1; i++)
        {
            string pattern = @"\b_NamePlayer_\b";
            string replace = DirectorWorld.instance.playerName;
            txt[i] = Regex.Replace(txt[i], pattern, replace);
        }
    }
    
    private void DefineInformations(GameObject dialogueSpaceCopy, string[] txtCopy)
    {
        dialogueSpace = dialogueSpaceCopy;
        txt = txtCopy;
        
        SearchKeyWords();
    }

    private void DefineInformations(GameObject dialogueSpaceCopy, Sprite spriteActualCopy, string[] txtCopy)
    {
        dialogueSpace = dialogueSpaceCopy;
        spriteActual = spriteActualCopy;
        txt = txtCopy;
        
        SearchKeyWords();
    }

    public void Speech(GameObject dialogueSpaceCopy, string[] txtCopy)
    {
        DefineInformations(dialogueSpaceCopy, txtCopy);

        dialogueSpace.SetActive(true);
        sentences = txt;
        DirectorWorld.instance.isReading = true;

        StartCoroutine(TypeSentence());
    }

    public void Speech(GameObject dialogueSpaceCopy, Sprite spriteActualCopy, string[] txtCopy)
    {
        DefineInformations(dialogueSpaceCopy, spriteActualCopy, txtCopy);

        dialogueSpace.SetActive(true);
        dialogueSpace.transform.Find("ImageOfNpc").GetComponent<Image>().sprite = spriteActual;
        sentences = txt;

        StartCoroutine(TypeSentence());
    }

    IEnumerator TypeSentence()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            dialogueSpace.GetComponentInChildren<Text>().text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        if (dialogueSpace.GetComponentInChildren<Text>().text == sentences[index])
        {
            typingSpeed = typingSpeedDefault;
            if (index < sentences.Length - 1)
            {
                index++;
                dialogueSpace.GetComponentInChildren<Text>().text = "";
                StartCoroutine(TypeSentence());
            }
            else
            {
                dialogueSpace.GetComponentInChildren<Text>().text = "";
                index = 0;

                Destroy(dialogueSpace);
                if (changeSceneDialogueToBattle)
                {
                    SelectScreenToChange();
                }
                    
                DirectorWorld.instance.isReading = false;
                DirectorWorld.instance.playerCanMove = true;
            }
        }
        else
        {
            typingSpeed = 0;
        }
    }

    private void SelectScreenToChange()
    {
        if (screenActual.name == "FirstBattleFlowey")
        {
            DirectorWorld.instance.ChangeScreen("FirstBattleFlowey", "OpenWorldScreen");
            screenActual = GameObject.Find("World");
        }
        else if (screenActual.name == "World")
        {
            DirectorWorld.instance.ChangeScreen("World", "FirstBattle");
            screenActual = GameObject.Find("FirstBattleFlowey");
        }
    }

    public GameObject CreateBoxConversation(GameObject painelConversation, Sprite sprite, string[] speechText)
    {
        GameObject conversationInScene = Instantiate(painelConversation, GameObject.Find("Canvas").transform);
        instance.Speech(conversationInScene, sprite, speechText);

        return conversationInScene;
    }
}