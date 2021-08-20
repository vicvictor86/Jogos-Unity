using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloweyBoss : Enemy
{

    // Start is called before the first frame update
    private void Awake()
    {
        this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Flowey");
    }

    void Start()
    {
               
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
