using System.Collections;
using System.Collections.Generic;
using Asyncoroutine;
using UnityEngine;
using UnityEngine.EventSystems;
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
    [SerializeField] private Enemy enemyActual = null;
    
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
    [SerializeField] private bool firstClick = false;
    [SerializeField] private bool secondClick = false;
    [SerializeField] private bool isFighting = false;
    [SerializeField] private bool clickItem = false;
    [SerializeField] private EventSystem eventSystem = null;

    [Header("Texts of Monster")] 
    [SerializeField] private GameObject textOfCharacter = null;
    [SerializeField] private GameObject positionOfText = null;
    [SerializeField] private GameObject playerTarget = null;

    [Header("Itens")]
    [SerializeField] private ItemSystem itemSystem = null;
    [SerializeField] private GameObject itemButton = null;
    [SerializeField] private List<Itens> itensList = null;
    
    private GameObject lastButtonSelected = null;

    public void CatchWorldScreen()
    {
        openWorldScreen = GameObject.FindWithTag("World");
    }
    
    public void Start()
    {
        enemyActual = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
        itemSystem = GameObject.Find("ItemSystem").GetComponent<ItemSystem>();
        InstantiateTextOfCharacter(enemyActual.TextBeggin());
        
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Life PotionQuantity", 3);
        PlayerPrefs.SetInt("MeatQuantity", 1);
    }

    private void Update()
    {   
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            //MenuEsc.SetActive(estaPausado);    
        }

        if(Input.GetKeyDown(KeyCode.Z) && isReading)
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
                transform.parent.gameObject.SetActive(false);
            }
        }

        if (Input.GetKeyUp(KeyCode.X) && firstClick)
        {
            switch (secondClick)
            {
                case false:
                {
                    ReturnToTextFromTargetSelect();
                    
                    if (clickItem)
                    {
                        itemSystem.DestroyChidrenItems();
                        clickItem = false;
                    }
                    
                    SelectFirstButton(lastButtonSelected);
                    firstClick = false;
                    break;
                }
                case true:
                    GameObject.Find("ActSystem").GetComponent<ActSystem>().DestroyButtonsAct();
                    GameObject firstButton = DefineActButton();
                    secondClick = false;
                    SelectFirstButton(firstButton);
                    break;
            }
        }
    }
    
    private void FirstClick()
    {
        Destroy(GameObject.Find("TextCharacter(Clone)"));
        firstClick = true;
        SetInterectableButtons(false);
    }

    public void ClickFightButton()
    {
        FirstClick();
        lastButtonSelected = battleButtons[0].gameObject;
        GameObject buttonPlayerTarget = DefineTarget();
        buttonPlayerTarget.GetComponent<Button>().onClick.AddListener(WannaFightButton);
        SelectFirstButton(buttonPlayerTarget);
    }

    private void WannaFightButton()
    {
        Destroy(GameObject.Find("PlayerTarget(Clone)"));
        DisableBack();
        Instantiate(attackLevel, battleFieldSprite.transform.position, Quaternion.identity);
        Instantiate(pointerOfAttack, positionOfPointer.transform.position, Quaternion.identity);
    }

    private GameObject DefineActButton()
    {
        GameObject buttonPlayerTarget = DefineTarget();
        buttonPlayerTarget.GetComponent<Button>().onClick.AddListener(WannaActButton);

        return buttonPlayerTarget;
    }
    
    public void ClickActButton()
    {
        FirstClick();
        lastButtonSelected = battleButtons[1].gameObject;
        SelectFirstButton(DefineActButton());
    }

    private void WannaActButton()
    {
        Destroy(GameObject.Find("PlayerTarget(Clone)"));

        secondClick = true;
        GameObject firstButton = Instantiate(buttonCheck, positionOfCheck.transform.position, Quaternion.identity, positionOfCheck.transform.parent);
        Instantiate(buttonTalk, positionOfTalk.transform.position, Quaternion.identity, positionOfTalk.transform.parent);
        
        SelectFirstButton(firstButton);
        GameObject.Find("ActSystem").GetComponent<ActSystem>().DefineObjects();
    }
    public void ClickItemButton()
    {
        FirstClick();
        clickItem = true;
        lastButtonSelected = battleButtons[2].gameObject;
        
        itensList = itemSystem.ListOfItems();
        if(itensList.Capacity == 0)
        {
            InstantiateTextOfCharacter("Voce nao tem nenhum item");
            isReading = true;
        }
        
        GameObject layoutButtons = GameObject.Find("LayoutButtons");
        int step = 0;
        foreach (Itens itemActual in itensList)
        {
            
            GameObject itemInstantiate = Instantiate(itemButton, layoutButtons.transform);
            
            if (step == 0)
            {
                SelectFirstButton(itemInstantiate);
                step++;
            }
            
            itemInstantiate.GetComponentInChildren<Text>().text = itemActual.GetName();
            itemInstantiate.GetComponent<Button>().onClick.AddListener(() => { itemSystem.UseItem(itemActual); });
        }
    }
    public void IsMercy()
    {
        FirstClick();
        
        GameObject buttonPlayerTarget = DefineTarget();
        lastButtonSelected = battleButtons[3].gameObject;
        SelectFirstButton(buttonPlayerTarget);
        
        buttonPlayerTarget.GetComponent<Button>().onClick.AddListener(WannaMercyButton);
    }

    private async void WannaMercyButton()
    {
        Destroy(GameObject.Find("PlayerTarget(Clone)"));
        DisableBack();
        if (enemyActual.GetConvincing() <= 0)
        {
            InstantiateTextOfCharacter(enemyActual.TextConviced());
            
            await new WaitForSeconds(0.5f);
            
            isConvinced = true;
            isReading = true;
        }
        else
        {
            InstantiateTextOfCharacter(enemyActual.TextNoConviced());
            
            await new WaitForSeconds(0.5f);
            
            isReading = true;
        }
    }
    
    private void SelectFirstButton(GameObject firstButton)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton);
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

        isFighting = false;
        playerHeart.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        
        ChangeBattleFieldToLarge();
        yield return new WaitForSeconds(1f);
        
        InstantiateTextOfCharacter(enemyActual.TextBeggin());
        SetInterectableButtons(true);
        
        DisableBack();
        DisablePlayer();
        SelectFirstButton(battleButtons[0].gameObject);
        
        StopCoroutine(nameof(EndBattle));
    }

    public void DisableBack()
    {
        firstClick = false;
        secondClick = false;
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

    private GameObject DefineTarget()
    {
        GameObject target = Instantiate(playerTarget, positionOfCheck.transform.position, Quaternion.identity, positionOfCheck.transform.parent);
        target.GetComponentInChildren<Text>().text = enemyActual.GetName();
        return target;
    }

    private void ReturnToTextFromTargetSelect()
    {
        Destroy(GameObject.Find("PlayerTarget(Clone)"));
        InstantiateTextOfCharacter(enemyActual.TextBeggin());
        SetInterectableButtons(true);
    }

    public bool IsFighting()
    {
        return this.isFighting;
    }

    private void ChangingScaleBattlefield(float x, float y, float z)
    {
        battleFieldSprite.transform.localScale = new Vector3(x, y, z);
    }

    public void SetSecondClick(bool boolean)
    {
        this.secondClick = boolean;
    }
}
