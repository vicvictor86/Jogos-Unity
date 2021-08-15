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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TextChange()
    {
        textOfLife.text = playerHeart.GetLife() + "/" + playerHeart.GetLifeMax();
    }

    public void LifeBarChange(int lifeLoose)
    {
        lifeBar.fillAmount -= (float) lifeLoose / 20;
    }
}
