using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Shop/Buyable")]
public class BuyableObject : ScriptableObject
{
    //Atributos de los objetos que se pueden comprar
    //Distintos para cada objeto
    public int itemID;
    public string itemName;
    public Sprite itemSprite;
    public int initialCost;
    public int maxLevel;
}
