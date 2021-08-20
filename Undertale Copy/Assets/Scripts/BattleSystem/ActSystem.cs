using System.Collections;
using System.Collections.Generic;
using Asyncoroutine;
using UnityEngine;
using UnityEngine.UI;

public class ActSystem : MonoBehaviour
{
    private GameObject buttonCheck;
    private GameObject buttonTalk;
    [SerializeField] private GameObject positionOfText = null;
    [SerializeField] private GameObject whatHappen = null;

    [SerializeField] private bool isReading = false;
    private Enemy enemyActual = null;
    
    private void Start()
    {
        enemyActual = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z) && isReading)
        {
            Destroy(GameObject.Find("WhatHappen(Clone)"));
            GameObject.Find("Diretor").GetComponent<Diretor>().PrepareToFight();
            isReading = false;
        }
    }

    public void DefineObjects()
    {
        buttonCheck = GameObject.Find("ButtonActCheck(Clone)");
        buttonTalk = GameObject.Find("ButtonActTalk(Clone)");
        buttonCheck.GetComponent<Button>().onClick.AddListener(ClickOnCheck);
        buttonTalk.GetComponent<Button>().onClick.AddListener(ClickOnTalk);
    }

    public void DestroyButtonsAct()
    {
        Destroy(buttonCheck);
        Destroy(buttonTalk);
    }
    
    private async void WhatHappen(string text)
    {
        DestroyButtonsAct();
        GameObject whatHappenCreated = Instantiate(whatHappen, positionOfText.transform.position, Quaternion.identity, positionOfText.transform.parent);
        whatHappenCreated.GetComponent<Text>().text = text;

        await new WaitForSeconds(0.5f);
        isReading = true;
    }

    private void ClickOnCheck()
    {
        GameObject.Find("Diretor").GetComponent<Diretor>().DisableBack();
        WhatHappen("                                     Attack: " + enemyActual.GetPowerEnemy() + "                                                    Life: " + enemyActual.GetLife());
    }

    private void ClickOnTalk()
    {
        GameObject.Find("Diretor").GetComponent<Diretor>().DisableBack();
        WhatHappen(enemyActual.TextTalking());
        enemyActual.Convince(1);
    }
}
