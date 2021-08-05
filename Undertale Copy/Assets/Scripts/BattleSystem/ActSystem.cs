using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActSystem : MonoBehaviour
{
    [SerializeField] private GameObject buttonCheck;
    [SerializeField] private GameObject buttonTalk;
    [SerializeField] private GameObject positionOfText = null;
    [SerializeField] private GameObject whatHappen = null;

    private bool isReading = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z) && isReading == true)
        {
            Destroy(GameObject.Find("WhatHappen(Clone)").gameObject);
            GameObject.Find("Diretor").GetComponent<Diretor>().PrepareToFight();
            isReading = false;
        }
    }

    public void DefineObjects()
    {
        buttonCheck = GameObject.Find("ButtonActCheck(Clone)");
        buttonTalk = GameObject.Find("ButtonActTalk(Clone)");
        buttonCheck.GetComponent<Button>().onClick.AddListener(() => { ClickOnCheck(); });
    }

    public void ClickOnCheck()
    {
        Destroy(buttonCheck.gameObject);
        Destroy(buttonTalk.gameObject);
    
        GameObject whatHappenCreated = Instantiate(whatHappen, positionOfText.transform.position, Quaternion.identity, positionOfText.transform.parent);
        whatHappenCreated.GetComponent<Text>().text = "Olha ae deu certo";
        isReading = true;
    }
}
