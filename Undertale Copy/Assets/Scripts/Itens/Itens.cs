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

    public void Use()
    {
        switch (type)
        {
            case TypeOfItem.Attack:
                player.SetPower(player.GetPower() + power);
                break;
            case TypeOfItem.Healing:
                player.Heal(power);
                break;
        }
        
        int quantityOfItem = PlayerPrefs.GetInt(nameOfItem + "Quantity");
        //Trabalhar para sumir da lisat quando chegar a zero
        if (quantityOfItem > 0)
        {
            PlayerPrefs.SetInt(nameOfItem + "Quantity", quantityOfItem - 1);
        }
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

    public void SetName(string name)
    {
        this.nameOfItem = name;
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
