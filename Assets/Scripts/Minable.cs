using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minable : MonoBehaviour
{
    [SerializeField] int value;

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            Manager.instance.addMoney(value);
            Destroy(gameObject);
        }
    }
}
