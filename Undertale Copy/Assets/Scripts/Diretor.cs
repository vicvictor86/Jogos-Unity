using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diretor : MonoBehaviour
{

    //[SerializeField]
    //private GameObject MenuEsc = null;
    public GameObject BattleButtons;
    private bool estaPausado;

    //Logica dos cliques
    public bool clickFight = true;
    public bool clickAct = false;
    public bool clickItem = false;
    public bool clickMercy = false;
    public bool isFighting = false;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            estaPausado = !estaPausado;
            //MenuEsc.SetActive(estaPausado);    
        }
    }

    public void IsFighting()
    {
        clickFight = !clickFight;
    }
    public void IsAct()
    {
        clickAct = !clickAct;
    }
    public void IsItem()
    {
        clickItem = !clickItem;
    }
    public void IsMercy()
    {
        clickMercy = !clickMercy;
    }

}
