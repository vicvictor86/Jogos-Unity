using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Diretor : MonoBehaviour
{

    //[SerializeField]
    //private GameObject MenuEsc = null;
    
    private bool isPaused;
    private bool isReading;

    [Header("Battle")]
    [SerializeField] private GameObject battleFieldSprite = null;
    [SerializeField] private GameObject playerHeart = null;
    [SerializeField] private float timeOfBattle = 0;
    [SerializeField] private bool isConvinced = false;
    [SerializeField] private GameObject openWorldScreen = null;
    [SerializeField] private List<Button> battleButtons = null;
    
    [Header("Attack Actions")]
    [SerializeField] private GameObject attackLevel = null;
    [SerializeField] private GameObject pointerOfAttack = null;
    [SerializeField] private GameObject positionOfPointer = null;

    [Header("Check Actions")]
    [SerializeField] private GameObject buttonCheck = null;
    [SerializeField] private GameObject buttonTalk = null;
    [SerializeField] private GameObject positionOfCheck = null;
    [SerializeField] private GameObject positionOfTalk = null;

    [Header("Buttons")]
    [SerializeField] private bool clickFight = true;
    [SerializeField] private bool clickAct = false;
    [SerializeField] private bool clickItem = false;
    [SerializeField] private bool clickMercy = false;
    [SerializeField] private bool isFighting = false;

    [Header("Texts of Monster")] 
    [SerializeField] private GameObject textOfCharacter = null;
    [SerializeField] private GameObject positionOfText = null;
    [SerializeField] private GameObject playerTarget = null;

    [Header("Itens")]
    [SerializeField] private ItemSystem itemSystem = null;
    [SerializeField] private GameObject itemButton = null;
    [SerializeField] private List<Itens> itensList = null;

    private TorielBoss toriel = null;

    public void Start()
    {
        Instantiate(textOfCharacter, positionOfText.transform.position, Quaternion.identity, positionOfText.transform.parent);
        toriel = GameObject.Find("Toriel").GetComponent<TorielBoss>();
        itemSystem = GameObject.Find("ItemSystem").GetComponent<ItemSystem>();
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Life PotionQuantity", 2);
        PlayerPrefs.SetInt("MeatQuantity", 1);
    }

    private void Update()
    {   
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            //MenuEsc.SetActive(estaPausado);    
        }

        if(Input.GetKeyUp(KeyCode.Z) && isReading == true)
        {
            Destroy(GameObject.Find("TextCharacter(Clone)"));
            isReading = false;
            if (!isConvinced)
            {
                PrepareToFight();
            }
            else
            {
                openWorldScreen.SetActive(true);
                this.transform.parent.gameObject.SetActive(false);
            }
        }
    }

    private void InstantiateTextOfCharacter(string text)
    {
        GameObject textConvincig = Instantiate(textOfCharacter, positionOfText.transform.position, Quaternion.identity, positionOfText.transform.parent);
        textConvincig.GetComponent<Text>().text = text;
    }

    public void StartEndBattle()
    {
        StartCoroutine(nameof(EndBattle));
    }
    
    private IEnumerator EndBattle()
    {
        yield return new WaitForSeconds(timeOfBattle);

        playerHeart.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        
        ChangeBattleFieldToLarge();
        yield return new WaitForSeconds(1f);
        
        InstantiateTextOfCharacter("Toriel ta tiltada contigo");
        SetInterectableButtons(true);
        
        isFighting = false;
        DisablePlayer();
        
        StopCoroutine(nameof(EndBattle));
    }

    private void SetInterectableButtons(bool boolean)
    {
        foreach (Button button in battleButtons)
        {
            button.interactable = boolean;
        }
    }

    private void DisablePlayer()
    {
        playerHeart.GetComponent<SpriteRenderer>().enabled = false;
        playerHeart.GetComponent<BoxCollider2D>().enabled = false;
    }
    
    private void EnablePlayer()
    {
        playerHeart.GetComponent<SpriteRenderer>().enabled = true;
        playerHeart.GetComponent<BoxCollider2D>().enabled = true;
    }
    
    public void PrepareToFight()
    {
        EnablePlayer();
        playerHeart.transform.position = battleFieldSprite.transform.position;

        ChangeBattleFieldToSmall();
        this.isFighting = true;
    }

    private void ChangeBattleFieldToSmall()
    {
        battleFieldSprite.GetComponent<Animator>().Play("DecreaseOfBattleField");
    }
    
    private void ChangeBattleFieldToLarge()
    {
        DisablePlayer();
        battleFieldSprite.GetComponent<Animator>().Play("DecreaseOfBattleFieldReversed");
    }

    private GameObject TransitionTextToTargetSelect()
    {
        Destroy(GameObject.Find("TextCharacter(Clone)"));
        return Instantiate(playerTarget, positionOfCheck.transform.position, Quaternion.identity, positionOfCheck.transform.parent);
    }

    public void ClickFightButton()
    {
        clickFight = !clickFight;
        SetInterectableButtons(false);

        GameObject buttonPlayerTarget = TransitionTextToTargetSelect();
        buttonPlayerTarget.GetComponent<Button>().onClick.AddListener(WannaFightButton);
    }

    private void WannaFightButton()
    {
        //Animação pra atacar
        Destroy(GameObject.Find("PlayerTarget(Clone)"));

        Instantiate(attackLevel, battleFieldSprite.transform.position, Quaternion.identity);
        Instantiate(pointerOfAttack, positionOfPointer.transform.position, Quaternion.identity);
    }

    public void ClickActButton()
    {
        clickAct = !clickAct;
        SetInterectableButtons(false);

        GameObject buttonPlayerTarget = TransitionTextToTargetSelect();
        buttonPlayerTarget.GetComponent<Button>().onClick.AddListener(WannaActButton);
    }

    private void WannaActButton()
    {
        //Animação de diálogo
        Destroy(GameObject.Find("PlayerTarget(Clone)"));
        ChangeBattleFieldToLarge();

        Instantiate(buttonCheck, positionOfCheck.transform.position, Quaternion.identity, positionOfCheck.transform.parent);
        Instantiate(buttonTalk, positionOfTalk.transform.position, Quaternion.identity, positionOfTalk.transform.parent);
        
        GameObject.Find("ActSystem").GetComponent<ActSystem>().DefineObjects();
    }
    public void ClickItemButton()
    {
        clickItem = !clickItem;
        Destroy(GameObject.Find("TextCharacter(Clone)"));
        SetInterectableButtons(false);

        //Escolha de item
        itensList = itemSystem.ListOfItems();
        if(itensList.Capacity == 0)
        {
            InstantiateTextOfCharacter("Voce nao tem nenhum item");
            isReading = true;
        }

        GameObject layoutButtons = GameObject.Find("LayoutButtons");
        foreach (Itens itemActual in itensList)
        {
            GameObject itemInstantiate = Instantiate(itemButton, layoutButtons.transform);
            itemInstantiate.GetComponentInChildren<Text>().text = itemActual.GetName();
            itemInstantiate.GetComponent<Button>().onClick.AddListener(() => { itemSystem.UseItem(itemActual); });
        }
    }
    public void IsMercy()
    {
        clickMercy = !clickMercy;
        SetInterectableButtons(false);

        GameObject buttonPlayerTarget = TransitionTextToTargetSelect();
        buttonPlayerTarget.GetComponent<Button>().onClick.AddListener(WannaMercyButton);
    }

    private void WannaMercyButton()
    {
        Destroy(GameObject.Find("PlayerTarget(Clone)"));
        if (toriel.GetConvincing() <= 0)
        {
            InstantiateTextOfCharacter("Toriel nao ta mais tiltada, ces fizeram as pazes");
            isConvinced = true;
            isReading = true;
        }
        else
        {
            InstantiateTextOfCharacter("Toriel ainda ta tiltada contigo, hora da porrada");
            isReading = true;
        }
    }

    public bool IsFighting()
    {
        return this.isFighting;
    }

    private void ChangingScaleBattlefield(float x, float y, float z)
    {
        battleFieldSprite.transform.localScale = new Vector3(x, y, z);
    }
}
