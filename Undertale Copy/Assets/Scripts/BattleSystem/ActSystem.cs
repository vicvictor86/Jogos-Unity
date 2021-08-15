using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActSystem : MonoBehaviour
{
    private GameObject buttonCheck;
    private GameObject buttonTalk;
    [SerializeField] private GameObject positionOfText = null;
    [SerializeField] private GameObject whatHappen = null;

    [SerializeField] private bool isReading = false;
    private TorielBoss toriel = null;
    
    private void Start()
    {
        toriel = GameObject.Find("Toriel").GetComponent<TorielBoss>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z) && isReading == true)
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
        buttonCheck.GetComponent<Button>().onClick.AddListener(() => { ClickOnCheck(); });
        buttonTalk.GetComponent<Button>().onClick.AddListener(() => { ClickOnTalk(); });
    }

    private void DestroyButtonsAct(string text)
    {
        Destroy(buttonCheck);
        Destroy(buttonTalk);
        GameObject whatHappenCreated = Instantiate(whatHappen, positionOfText.transform.position, Quaternion.identity, positionOfText.transform.parent);
        whatHappenCreated.GetComponent<Text>().text = text;
        isReading = true;
    }

    public void ClickOnCheck()
    {
        DestroyButtonsAct("                                     Attack: " + toriel.GetPowerOfToriel() + "                                                    Life: " + toriel.GetLife());
    }

    public void ClickOnTalk()
    {
        DestroyButtonsAct("Você tenta conversar com Toriel, sem sucesso.");
        toriel.Convince(1);
    }

    
}
