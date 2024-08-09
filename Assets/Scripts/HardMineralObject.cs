using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Minerals/Hard Mineral")]

public class HardMineralObject : BasicMineralObject //Hereda de BasicMineralObject con los mismos atributos basicos
{
    public int durability; //"Vida" del mineral Cuanto mejor el taladro más rapido se rompe
    public int minimumDrill; //Taladro minimo necesario para romper el mineral
}
