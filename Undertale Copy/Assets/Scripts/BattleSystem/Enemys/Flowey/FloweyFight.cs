using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloweyFight : MonoBehaviour
{
    [SerializeField] private GameObject bubbleText = null; 
    public string[] speechText;
    
    private void Start()
    {
        SpeechExplain();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && DirectorWorld.instance.isReading)
        {
            DialogueSystem.instance.NextSentence();
            DialogueSystem.instance.changeSceneDialogueToBattle = true;
        }
    }

    public void SpeechExplain()
    {
        DialogueSystem.instance.Speech(bubbleText, speechText);
    }
}
