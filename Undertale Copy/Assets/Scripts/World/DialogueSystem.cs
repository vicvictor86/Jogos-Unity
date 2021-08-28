using System.Collections;
using System.Collections.Generic;
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
    
    [Header("Settings")] 
    public float typingSpeed;
    private string[] sentences;
    private int index;

    private GameObject dialogueSpace;
    private Sprite spriteActual;
    private string[] txt;

    private void DefineInformations(GameObject dialogueSpaceCopy, Sprite spriteActualCopy, string[] txtCopy)
    {
        dialogueSpace = dialogueSpaceCopy;
        spriteActual = spriteActualCopy;
        txt = txtCopy;
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
                GameObject.FindWithTag("Player").GetComponent<Player>().StartingBattle();
                
                DirectorWorld.instance.isReading = false;
            }
        }
    }
}
