using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolasShop : MonoBehaviour
{

    public static BolasShop instance;

    public List<Bolas> bolasList = new List<Bolas>();
    public List<GameObject> bolaSuporteList = new List<GameObject>();
    public List<GameObject> compraBtnList = new List<GameObject>();

    public GameObject baseBolaItem;
    public Transform conteudo;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        FillList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FillList()
    {
        foreach(Bolas b in bolasList)
        {
            GameObject itensBola = Instantiate(baseBolaItem) as GameObject;
            itensBola.transform.SetParent(conteudo, false);
            BolasSuporte item = itensBola.GetComponent<BolasSuporte>();

            item.bolaID = b.bolasID;
            item.bolaPreco.text = b.bolasPreco.ToString();
            item.btnCompra.GetComponent<CompraBola>().bolasID = b.bolasID;

            //Lista CompraBtn
            compraBtnList.Add(item.btnCompra);

            //Lista bolaSuporteList
            bolaSuporteList.Add(itensBola);

            if(PlayerPrefs.GetInt("BTN"+item.bolaID) == 1)
            {
                b.bolasComprou = true;
            }

            if(PlayerPrefs.HasKey("BTNS"+item.bolaID) && b.bolasComprou)
            {
                item.btnCompra.GetComponent<CompraBola>().btnText.text = PlayerPrefs.GetString("BTNS" + item.bolaID);
            }

            if (b.bolasComprou)
            {
                item.bolaSprite.sprite = Resources.Load<Sprite>("SpritesBola/" + b.bolasNomeSprite);
                item.bolaPreco.text = "Comprado!";

                if(PlayerPrefs.HasKey("BTNS"+item.bolaID) == false)
                {
                    item.btnCompra.GetComponent<CompraBola>().btnText.text = "Usando";
                }
            }
            else
            {
                item.bolaSprite.sprite = Resources.Load<Sprite>("SpritesBola/" + b.bolasNomeSprite + "_cinza"); ;
            }
        }
    }

    public void UpdateSprite(int bola_ID)
    {
        for(int i = 0; i < bolaSuporteList.Count; i++)
        {
            BolasSuporte bolasSuporteScript = bolaSuporteList[i].GetComponent<BolasSuporte>();

            if(bolasSuporteScript.bolaID == bola_ID)
            {
                for(int j = 0; j < bolasList.Count; j++)
                {
                    if(bolasList[j].bolasID == bola_ID)
                    {
                        if (bolasList[j].bolasComprou)
                        {
                            bolasSuporteScript.bolaSprite.sprite = Resources.Load<Sprite>("SpritesBola/" + bolasList[j].bolasNomeSprite);
                            bolasSuporteScript.bolaPreco.text = "Comprado!";
                            SalvaBolasLojaInfo(bolasSuporteScript.bolaID);
                        }
                        else
                        {
                            bolasSuporteScript.bolaSprite.sprite = Resources.Load<Sprite>("SpritesBola/" + bolasList[j].bolasNomeSprite + "_cinza");
                        }
                    }
                }
            }
        }
    }

    void SalvaBolasLojaInfo(int idBola)
    {
        for(int i = 0; i < bolasList.Count; i++)
        {
            BolasSuporte bolasSup = bolaSuporteList[i].GetComponent<BolasSuporte>();

            if (bolasSup.bolaID == idBola)
            {
                PlayerPrefs.SetInt("BTN" + bolasSup.bolaID, bolasSup.btnCompra ? 1 : 0);
            }
        }
    }

    public void SalvaBolasLojaText(int idBola, string s)
    {
        for(int i = 0; i < bolasList.Count; i++)
        {
            BolasSuporte bolasSup = bolaSuporteList[i].GetComponent<BolasSuporte>();

            if(bolasSup.bolaID == idBola)
            {
                PlayerPrefs.SetString("BTNS" + bolasSup.bolaID, s);
            }
        }
    }
}
