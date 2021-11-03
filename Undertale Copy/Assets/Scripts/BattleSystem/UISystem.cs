using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{
    [SerializeField] private PlayerHeart playerHeart = null;
    [SerializeField] private Text textOfLife = null;
    [SerializeField] private Image lifeBar = null;
    [SerializeField] private Text playerName = null;
    
    // Start is called before the first frame update
    void Start()
    {
//        playerName.text = DirectorWorld.instance.playerName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TextChange()
    {
        textOfLife.text = playerHeart.GetLife() + "/" + playerHeart.GetLifeMax();
    }

    public void LifeBarDegrease(int lifeLoose)
    {
        lifeBar.fillAmount -= (float) lifeLoose / 20;
    }
    
    public void LifeBarAddition(int lifeWin)
    {
        lifeBar.fillAmount += (float) lifeWin / 20;
    }
}
