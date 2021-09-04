using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondEncountetWithFlowey : MonoBehaviour
{
    public Sprite spriteFlowey;
    public string[] speechText;

    public bool nowIsTheRealFight;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DirectorWorld.instance.playerAlreadyBattle = false;
            nowIsTheRealFight = true;

            DirectorWorld.instance.isReading = true;
            DirectorWorld.instance.playerCanMove = false;
            DialogueSystem.instance.changeSceneDialogueToBattle = false;
            
            DialogueSystem.instance.CreateBoxConversation(DirectorWorld.instance.conversationWithNpc, spriteFlowey,
                speechText);
        }
    }
}
