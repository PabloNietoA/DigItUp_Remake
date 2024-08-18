using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refueler : MonoBehaviour, IMineable
{
    [SerializeField] private float fuelAmount;

    public void Mined()
    {
        PlayerController.instance.AddFuel(fuelAmount);
        Destroy(gameObject);
    }
}
