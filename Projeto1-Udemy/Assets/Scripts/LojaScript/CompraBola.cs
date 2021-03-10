using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompraBola : MonoBehaviour
{
    public int bolasID;
    public Text btnText;
    private Animator falido;


    public void CompraBolasBtn()
    {
        for(int i = 0; i < BolasShop.instance.bolasList.Count; i++)
        {
            if (BolasShop.instance.bolasList[i].bolasID == bolasID && !BolasShop.instance.bolasList[i].bolasComprou && PlayerPrefs.GetInt("moedasSave") >= BolasShop.instance.bolasList[i].bolasPreco)
            {
                BolasShop.instance.bolasList[i].bolasComprou = true;
                UpdateCompraBtn();
                ScoreManager.instance.PerdeMoedas(BolasShop.instance.bolasList[i].bolasPreco);
                GameObject.Find("Numero_Moedas").GetComponent<Text>().text = PlayerPrefs.GetInt("moedasSave").ToString();
            }
            else if (BolasShop.instance.bolasList[i].bolasID == bolasID && !BolasShop.instance.bolasList[i].bolasComprou && PlayerPrefs.GetInt("moedasSave") <= BolasShop.instance.bolasList[i].bolasPreco)
            {
                falido = GameObject.FindGameObjectWithTag("Falido").GetComponent<Animator>();
                falido.Play("FalidoAnim");
            }
            else if (BolasShop.instance.bolasList[i].bolasID == bolasID && BolasShop.instance.bolasList[i].bolasComprou)
            {
                UpdateCompraBtn();
            }
        }

        BolasShop.instance.UpdateSprite(bolasID);
    }

    void UpdateCompraBtn()
    {
        btnText.text = "Usando";

        for(int i = 0; i < BolasShop.instance.compraBtnList.Count; i++)
        {
            CompraBola compraBolaScript = BolasShop.instance.compraBtnList[i].GetComponent<CompraBola>();

            for(int j = 0; j < BolasShop.instance.bolasList.Count; j++)
            {

                if(BolasShop.instance.bolasList[j].bolasID == compraBolaScript.bolasID)
                {
                    BolasShop.instance.SalvaBolasLojaText(compraBolaScript.bolasID, "Usando");
                    if(BolasShop.instance.bolasList[j].bolasID == compraBolaScript.bolasID && BolasShop.instance.bolasList[j].bolasComprou && BolasShop.instance.bolasList[j].bolasID == bolasID)
                    {
                        OndeEstou.instance.bolaEmUso = compraBolaScript.bolasID;
                        PlayerPrefs.SetInt("BolaUse", compraBolaScript.bolasID);
                    }
                }

                if(BolasShop.instance.bolasList[j].bolasID == compraBolaScript.bolasID && BolasShop.instance.bolasList[j].bolasComprou && BolasShop.instance.bolasList[j].bolasID != bolasID)
                {
                    compraBolaScript.btnText.text = "Use";
                    BolasShop.instance.SalvaBolasLojaText(compraBolaScript.bolasID, "Use");
                }
            }
        }
    }

    public void FalidoInvers()
    {
        falido = GameObject.FindGameObjectWithTag("Falido").GetComponent<Animator>();
        falido.Play("FalidoAnimInvers");
    }
}
