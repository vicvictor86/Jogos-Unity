using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Diretor : MonoBehaviour
{

    //[SerializeField]
    //private GameObject MenuEsc = null;
    
    private bool isPaused;

    [Header("Battle")]
    public GameObject battleFieldSprite;
    public GameObject playerHeart;
    public List<Button> battleButtons;

    [Header("Attack Actions")]
    public GameObject attackLevel;
    public GameObject pointerOfAttack;

    [Header("Check Actions")]
    [SerializeField] private GameObject buttonCheck = null;
    [SerializeField] private GameObject buttonTalk = null;
    [SerializeField] private GameObject positionOfCheck = null;
    [SerializeField] private GameObject positionOfTalk = null;

    public GameObject positionOfPointer;

    [Header("Buttons")]
    public bool clickFight = true;
    public bool clickAct = false;
    public bool clickItem = false;
    public bool clickMercy = false;
    public bool isFighting = false;

    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            //MenuEsc.SetActive(estaPausado);    
        }
    }

    public void PrepareToFight()
    {
        foreach (Button button in battleButtons)
        {
            button.interactable = false;
        }
        playerHeart.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        battleFieldSprite.gameObject.transform.localScale = new Vector3(65, 75, 1);
        this.isFighting = true;
    }

    public void ChangeBattleField()
    {
        playerHeart.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        battleFieldSprite.gameObject.transform.localScale = new Vector3(260, 75, 1);
    }

    public void IsFighting()
    {
        clickFight = !clickFight;
        //Animação pra atacar
        ChangeBattleField();
        Instantiate(attackLevel, battleFieldSprite.transform.position, Quaternion.identity);
        Instantiate(pointerOfAttack, positionOfPointer.transform.position, Quaternion.identity);
    }
    public void IsAct()
    {
        clickAct = !clickAct;
        //Animação de diálogo
        ChangeBattleField();

        Instantiate(buttonCheck, positionOfCheck.transform.position, Quaternion.identity, positionOfCheck.transform.parent);
        Instantiate(buttonTalk, positionOfTalk.transform.position, Quaternion.identity, positionOfTalk.transform.parent);
        
        GameObject.Find("ActSystem").GetComponent<ActSystem>().DefineObjects();
    }
    public void IsItem()
    {
        clickItem = !clickItem;
        //Escolha de item

        PrepareToFight();
    }
    public void IsMercy()
    {
        clickMercy = !clickMercy;
        //Perdoar o personagem e acabar a batalha
    }

}
