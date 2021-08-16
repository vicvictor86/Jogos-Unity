using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem : MonoBehaviour
{
    private Diretor director = null;

    // Start is called before the first frame update
    void Start()
    {
        director = GameObject.Find("Diretor").GetComponent<Diretor>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Itens DefineItem(string namePlayerPrefs, int quantityLifePotion, string name, int power, string description, Itens.TypeOfItem type)
    {
        GameObject gameObjectActual = new GameObject(namePlayerPrefs + "(" + quantityLifePotion + ")");
        Itens itemActual = gameObjectActual.AddComponent<Itens>();
        itemActual.transform.SetParent(GameObject.Find("ParentItensGameObject").transform);
        itemActual.SetName(name);
        itemActual.SetPower(power);
        itemActual.SetDescription(description);
        itemActual.SetType(type);
        return itemActual;
    }

    //When the item name have twp or more words, use space between them when saving to playerPrefs and 
    //Quantity must be with no space 
    public List<Itens> ListOfItems()
    {
        List<Itens> itemsSave = new List<Itens>();

        int quantityLifePotion = PlayerPrefs.GetInt("Life PotionQuantity");
        while (quantityLifePotion > 0)
        {
            Itens itemActual = DefineItem("Life PotionQuantity", quantityLifePotion, "Life Potion", 2, "A potion indeed", Itens.TypeOfItem.Healing);

            itemsSave.Add(itemActual);
            quantityLifePotion--;
        }

        int quantityMeatQuantity = PlayerPrefs.GetInt("MeatQuantity");
        while(quantityMeatQuantity > 0)
        {
            Itens itemActual = DefineItem("MeatQuantity", quantityMeatQuantity, "Meat", 1, "A meat delicious", Itens.TypeOfItem.Attack);

            itemsSave.Add(itemActual);
            quantityMeatQuantity--;
        }
        return itemsSave;
    }

    public void UseItem(Itens itemActual)
    {
        itemActual.Use();

        director.DisableBack();
        DestroyChidrenItems();

        director.PrepareToFight();
    }

    public void DestroyChidrenItems()
    {
        DestroyEveryChildren("LayoutButtons");
        DestroyEveryChildren("ParentItensGameObject");
    }

    private void DestroyEveryChildren(string nameOfParent)
    {
        GameObject parent = GameObject.Find(nameOfParent);
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
