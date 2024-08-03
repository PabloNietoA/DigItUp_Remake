using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Minerals/Basic Mineral")]

public class BasicMineralObject : ScriptableObject
{
    //Atributos de los minerales basicos
    //Distintos para cada mineral
    public int itemID;
    public string itemName;
    public string itemDescription;
    public Sprite itemSprite;
    //Oro otorgado al recolectar el mineral
    public int goldReward;
    
}
