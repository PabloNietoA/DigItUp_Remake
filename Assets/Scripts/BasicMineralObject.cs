using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Minerals/Basic Mineral")]

public class BasicMineralObject : ScriptableObject
{
    //Atributos de los minerales basicos
    //Distintos para cada mineral
    public int id;
    public new string name;
    public string description;
    public Sprite sprite;
    //Dinero otorgado al recolectar el mineral
    public int moneyReward;
    
}
