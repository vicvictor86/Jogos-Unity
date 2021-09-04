using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloweyBoss : Enemy
{
    public Sprite spriteFlowey;
    public string[] speechText;

    // Start is called before the first frame update
    private void Awake()
    {
        if (!isEnemyOpenWorld)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Flowey");
        }
    }
    
    public override string GetName()
    {
        return "Flowey";
    }
    
    public override string TextBeggin()
    {
        return "Flowey quer ser seu amigo";
    }

    public override string TextConviced()
    {
        return "Flowey gostou de voce, que tal sermos amigos?";
    }

    public override string TextNoConviced()
    {
        return "Flowey ainda nao te acha um bom amigo, tente mais uma vez";
    }

    public override string TextTalking()
    {
        return "Flowey gostou do seu papo, vcs possuem muita coisa em comum";
    }

    public override GameObject ContactWithPlayer()
    {
        if (GameObject.Find("SecondEncounter").GetComponent<SecondEncountetWithFlowey>().nowIsTheRealFight)
        {
            GameObject.FindWithTag("Player").GetComponent<Player>().StartingBattle();
            return null;
        }
        GameObject conversationInScene = DialogueSystem.instance.CreateBoxConversation(DirectorWorld.instance.conversationWithNpc, spriteFlowey, speechText);
        
        DirectorWorld.instance.isReading = true;
        DirectorWorld.instance.soundSystem.PlayAudio("YourBestFriend", true);
        
        return conversationInScene;
    }
}
