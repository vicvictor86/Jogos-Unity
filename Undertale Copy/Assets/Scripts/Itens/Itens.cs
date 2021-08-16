using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itens : MonoBehaviour
{
    [SerializeField] private string nameOfItem = "";
    [SerializeField] private int power = 0;
    [SerializeField] private string description = "";
    private PlayerHeart player = null;

    private TypeOfItem type;
    public enum TypeOfItem
    {
        Healing,
        Attack
    }

    private void Start()
    {
        player = GameObject.Find("PlayerHeart").GetComponent<PlayerHeart>();
    }

    public bool Use()
    {
        bool itemUse = true;
        switch (type)
        {
            case TypeOfItem.Attack:
                player.SetPower(player.GetPower() + power);
                break;
            case TypeOfItem.Healing:
                if (player.GetLife() != player.GetLifeMax())
                {
                    player.Heal(power);
                }
                else
                {
                    itemUse = false;
                }
                break;
        }

        if (itemUse)
        {
            //Não fazer dessa forma pois caso o jogo pare de ser executado n se deve perder os itens
            //Modificar em uma list que devo fazer dps
            int quantityOfItem = PlayerPrefs.GetInt(nameOfItem + "Quantity");
            if (quantityOfItem > 0)
            {
                PlayerPrefs.SetInt(nameOfItem + "Quantity", quantityOfItem - 1);
            }
        }

        return itemUse;
    }

    public string GetName()
    {
        return this.nameOfItem;
    }

    public int GetPower()
    {
        return this.power;
    }

    public string GetDescription()
    {
        return this.description;
    }

    public void SetName(string nameOfItem)
    {
        this.nameOfItem = nameOfItem;
    }

    public void SetPower(int power)
    {
        this.power = power;
    }

    public void SetDescription(string description)
    {
        this.description = description;
    }

    public void SetType(TypeOfItem type)
    {
        this.type = type;
    }
}
