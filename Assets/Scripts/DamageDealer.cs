using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour, IMineable
{
    [SerializeField] private float dealedDamage;
    public void Mined()
    {
        PlayerController.instance.TakeDamage(dealedDamage);
        Destroy(gameObject); //En el futuro sera sustituido por un efecto de explosión o lo que toque
        //EXPLOSION
    }
}
