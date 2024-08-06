using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Shop/Buyable")]
public class BuyableObject : ScriptableObject
{
    //Atributos de los objetos que se pueden comprar
    //Distintos para cada objeto
    public int id;
    public new string name; //new para que no de warning de que oculta el metodo de unity
    public Sprite sprite;
    public int initialCost;
    public float priceIncreaseIndex; // Indice de incremento del precio del objeto
    public int startLevel = 1; //1 by default, si es 0 bloqueado al inicio
    public int maxLevel;
}
